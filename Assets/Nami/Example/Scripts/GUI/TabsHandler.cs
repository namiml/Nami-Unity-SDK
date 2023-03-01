using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NamiExample
{
    [Serializable]
    public class Tab
    {
        public GameObject targetObject;
        public Button button;
        private CanvasGroup buttonCanvasGroup;

        private const float openStateAlpha = 1.0f;
        private const float closeStateAlpha = 0.4f;

        public void Init(Action<Tab> switchAction)
        {
            if (button == null) return;
            buttonCanvasGroup = button.GetComponent<CanvasGroup>();

            if (switchAction == null) return;
            button.onClick.AddListener(() => switchAction.Invoke(this));
        }

        public void Open()
        {
            if (buttonCanvasGroup != null) buttonCanvasGroup.alpha = openStateAlpha;
            if (targetObject != null) targetObject.SetActive(true);
        }

        public void Close()
        {
            if (buttonCanvasGroup != null) buttonCanvasGroup.alpha = closeStateAlpha;
            if (targetObject != null) targetObject.SetActive(false);
        }
    }

    public class TabsHandler : MonoBehaviour
    {
        [SerializeField] private List<Tab> tabs = new List<Tab>();

        private void Start()
        {
            foreach (var tab in tabs) tab.Init(SwitchTabs);
        }

        private void SwitchTabs(Tab tabToOpen)
        {
            foreach (var tab in tabs) tab.Close();
            tabToOpen.Open();
        }
    }
}