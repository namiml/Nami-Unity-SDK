using UnityEngine;
using UnityEngine.UI;

namespace NamiExample
{
    [RequireComponent(typeof(ScrollRect))]
    public class DebugScrollRect : MonoBehaviour
    {
        private ScrollRect scrollRect;

        [SerializeField] private Text logText;

        private void Awake()
        {
            scrollRect = GetComponent<ScrollRect>();
            logText.text = "";

            Application.logMessageReceived += OnLogMessageReceived;
        }

        private void OnEnable()
        {
            ScrollDown();
        }

        private void OnLogMessageReceived(string logMessage, string stackTrace, LogType type)
        {
            var log = "[" + type + "] : " + logMessage;
            if (type == LogType.Exception || type == LogType.Error)
            {
                log += "\n" + stackTrace;
            }

            logText.text += log + "\n\n";

            ScrollDown();
        }

        private void ScrollDown()
        {
            scrollRect.normalizedPosition = Vector2.zero;
        }
    }
}
