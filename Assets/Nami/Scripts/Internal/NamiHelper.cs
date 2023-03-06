
namespace NamiSDK
{
	using System;
	using System.Collections.Generic;
	using UnityEngine;

	class NamiHelper : MonoBehaviour
	{
		static NamiHelper _instance;
		static readonly object InitLock = new object();
		readonly object _queueLock = new object();
		readonly List<Action> _queuedActions = new List<Action>();
		readonly List<Action> _executingActions = new List<Action>();

		public static NamiHelper Instance
		{
			get
			{
				if (_instance == null)
				{
					Init();
				}

				return _instance;
			}
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		internal static void Init()
		{
			lock (InitLock)
			{
				if (ReferenceEquals(_instance, null))
				{
					var instances = FindObjectsOfType<NamiHelper>();

					if (instances.Length > 1)
					{
						Debug.LogError(typeof(NamiHelper) + " Something went really wrong " +
						               " - there should never be more than 1 " + typeof(NamiHelper) +
						               " Reopening the scene might fix it.");
					}
					else if (instances.Length == 0)
					{
						var singleton = new GameObject {hideFlags = HideFlags.HideAndDontSave};
						_instance = singleton.AddComponent<NamiHelper>();
						singleton.name = typeof(NamiHelper).ToString();

						DontDestroyOnLoad(singleton);

						Debug.Log("[Singleton] An _instance of " + typeof(NamiHelper) +
						          " is needed in the scene, so '" + singleton.name +
						          "' was created with DontDestroyOnLoad.");
					}
					else
					{
						Debug.Log("[Singleton] Using _instance already created: " + _instance.gameObject.name);
					}
				}
			}
		}

		NamiHelper()
		{
		}

		internal static void Queue(Action action)
		{
			if (action == null)
			{
				return;
			}

			lock (_instance._queueLock)
			{
				_instance._queuedActions.Add(action);
			}
		}

		void Update()
		{
			MoveQueuedActionsToExecuting();

			while (_executingActions.Count > 0)
			{
				Action action = _executingActions[0];
				_executingActions.RemoveAt(0);
				action();
			}
		}

		void MoveQueuedActionsToExecuting()
		{
			lock (_queueLock)
			{
				while (_queuedActions.Count > 0)
				{
					Action action = _queuedActions[0];
					_executingActions.Add(action);
					_queuedActions.RemoveAt(0);
				}
			}
		}
	}
}