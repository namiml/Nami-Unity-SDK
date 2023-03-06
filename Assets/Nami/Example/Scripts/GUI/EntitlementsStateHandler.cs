using System.Collections.Generic;
using NamiSDK;
using UnityEngine;
using UnityEngine.UI;

namespace NamiExample
{
    public class EntitlementsStateHandler : MonoBehaviour
    {
        [SerializeField] private Text titleText;
        private const string disactiveText = "No Active Entitlements for User";
        private const string activeText = "Active Entitlements";

        [Space] [SerializeField] private Transform contentRoot;
        [SerializeField] private EntitlementCard cardInstance;
        private readonly List<EntitlementCard> cardPool = new List<EntitlementCard>();

        private void Start()
        {
            NamiEntitlementManager.RegisterActiveEntitlementsHandler(UpdateEntitlements);
            UpdateEntitlements(NamiEntitlementManager.Active());
        }

        public void UpdateEntitlements(List<NamiEntitlement> entitlements)
        {
            foreach (var card in cardPool)
            {
                card.SetActive(false);
            }

            if (entitlements == null || entitlements.Count == 0)
            {
                titleText.text = disactiveText;
            }
            else
            {
                titleText.text = activeText;

                for (var i = 0; i < entitlements.Count; i++)
                {
                    if (cardPool.Count <= i)
                    {
                        var card = Instantiate(cardInstance, contentRoot);
                        cardPool.Add(card);
                    }

                    cardPool[i].UpdateInfo(entitlements[i]);
                    cardPool[i].SetActive(true);
                }
            }
        }
    }
}