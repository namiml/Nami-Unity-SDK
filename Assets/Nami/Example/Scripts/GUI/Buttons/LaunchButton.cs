using NamiSDK;
using UnityEngine;
using UnityEngine.UI;

namespace NamiExample
{
    [RequireComponent(typeof(Button))]
    public class LaunchButton : MonoBehaviour
    {
        private Button button;
        [SerializeField] private string label;

        private void Start()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            NamiCampaignManager.Launch(label);
        }
    }
}
