using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NamiExample
{
    [Serializable]
    public class Tab
    {
        [SerializeField] private GameObject targetObject;

        [SerializeField] private Button button;
        private CanvasGroup buttonCanvasGroup;

        private const float openStateAlpha = 1.0f;
        private const float closeStateAlpha = 0.4f;

        public void Init(Action onClick)
        {
            if (onClick == null || targetObject == null || button == null)
            {
                return;
            }

            buttonCanvasGroup = button.GetComponent<CanvasGroup>();
            button.onClick.AddListener(onClick.Invoke);
        }

        public void Open()
        {
            if (buttonCanvasGroup == null || targetObject == null)
            {
                return;
            }

            buttonCanvasGroup.alpha = openStateAlpha; 
            targetObject.SetActive(true);
        }

        public void Close()
        {
            if (buttonCanvasGroup == null || targetObject == null)
            {
                return;
            }

            buttonCanvasGroup.alpha = closeStateAlpha; 
            targetObject.SetActive(false);
        }
    }

    public class TabsHandler : MonoBehaviour
    {
        [SerializeField] private List<Tab> tabs = new List<Tab>();

        private void Start()
        {
            foreach (var tab in tabs)
            {
                tab.Init(() => SwitchTab(tab));
            }

            SwitchTab(tabs[0]);
        }

        private void SwitchTab(Tab tabToOpen)
        {
            foreach (var tab in tabs)
            {
                tab.Close();
            }

            tabToOpen.Open();
        }
    }
}