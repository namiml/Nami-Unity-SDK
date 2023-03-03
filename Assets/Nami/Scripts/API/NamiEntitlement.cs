using System.Collections.Generic;
using System.Linq;
using NamiSDK.MiniJSON;
using NamiSDK.Utils;
using UnityEngine;

namespace NamiSDK
{
    public class NamiEntitlement
    {
        public NamiEntitlement(AndroidJavaObject ajo)
        {
            ActivePurchases = ajo.CallAJO("getActivePurchases").FromJavaList<AndroidJavaObject>().Select(o => new NamiPurchase(o)).ToList();
            Desc = ajo.CallStr("getDesc");
            Name = ajo.CallStr("getName");
            NamiId = ajo.CallStr("getNamiId");
            PurchasedSKUs = ajo.CallAJO("getPurchasedSKUs").FromJavaList<AndroidJavaObject>().Select(o => new NamiSKU(o)).ToList();
            ReferenceId = ajo.CallStr("getReferenceId");
            RelatedSKUs = ajo.CallAJO("getRelatedSKUs").FromJavaList<AndroidJavaObject>().Select(o => new NamiSKU(o)).ToList();
        }

        public NamiEntitlement(object json)
        {
            var dictionary = Json.DeserializeDictionary(json);
            if (dictionary != null)
            {
                dictionary.TryGetValue("activePurchases", out var activePurchasesObject);
                dictionary.TryGetValue("desc", out var descObject);
                dictionary.TryGetValue("name", out var nameObject);
                dictionary.TryGetValue("namiId", out var namiIdObject);
                dictionary.TryGetValue("purchasedSkus", out var purchasedSkusObject);
                dictionary.TryGetValue("referenceId", out var referenceIdObject);
                dictionary.TryGetValue("relatedSkus", out var relatedSkusObject);
                
                if (activePurchasesObject != null)
                {
                    var list = Json.DeserializeList(activePurchasesObject);
                    if (list != null)
                    {
                        ActivePurchases = list.Select(jsonObject => new NamiPurchase(jsonObject)).ToList();
                    }
                }
                Desc = (string)descObject;
                Name = (string)nameObject;
                NamiId = (string)namiIdObject;
                if (purchasedSkusObject != null)
                {
                    var list = Json.DeserializeList(purchasedSkusObject);
                    if (list != null)
                    {
                        PurchasedSKUs = list.Select(jsonObject => new NamiSKU(jsonObject)).ToList();
                    }
                }
                ReferenceId = (string)referenceIdObject;
                if (relatedSkusObject != null)
                {
                    var list = Json.DeserializeList(relatedSkusObject);
                    if (list != null)
                    {
                        RelatedSKUs = list.Select(jsonObject => new NamiSKU(jsonObject)).ToList();
                    }
                }
            }
        }

        public List<NamiPurchase> ActivePurchases { get; private set; }
        public string Desc { get; private set; }
        public string Name { get; private set; }
        public string NamiId { get; private set; }
        public List<NamiSKU> PurchasedSKUs { get; private set; }
        public string ReferenceId { get; private set; }
        public List<NamiSKU> RelatedSKUs { get; private set; }
    }
}