using System;
using UnityEngine;
using UnityEngine.UI;

namespace NamiExample
{
    [RequireComponent(typeof(Button))]
    public class CommandButton : MonoBehaviour
    {
        private Button button;
        [SerializeField] private Text text;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
        }

        public string Name
        {
            get => text.text;
            set => text.text = value;
        }

        public Action Action { get; set; }

        private void OnClick()
        {
            Action?.Invoke();
        }
    }
}