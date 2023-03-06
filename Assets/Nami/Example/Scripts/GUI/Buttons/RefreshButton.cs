using System.Collections.Generic;
using NamiSDK;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NamiExample
{
    [RequireComponent(typeof(Button))]
    public class RefreshButton : MonoBehaviour
    {
        private Button button;

        public UnityEvent<List<NamiEntitlement>> onEntitlementRefreshed = new UnityEvent<List<NamiEntitlement>>();

        private void Start()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            NamiEntitlementManager.Refresh(OnEntitlementRefreshed);
            button.interactable = false;
        }

        private void OnEntitlementRefreshed(List<NamiEntitlement> namiEntitlements)
        {
            onEntitlementRefreshed.Invoke(namiEntitlements);
            button.interactable = true;
        }
    }
}