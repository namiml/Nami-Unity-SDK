using System.Collections.Generic;
using System.Linq;
using NamiSdk.MiniJSON;
using NamiSdk.Utils;
using UnityEngine;

namespace NamiSdk
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

        public NamiEntitlement(string json)
        {
            if (Json.Deserialize(json) is Dictionary<string, object> jsonDictionary)
            {
                jsonDictionary.TryGetValue("activePurchases", out var activePurchasesObject);
                jsonDictionary.TryGetValue("desc", out var descObject);
                jsonDictionary.TryGetValue("name", out var nameObject);
                jsonDictionary.TryGetValue("namiId", out var namiIdObject);
                jsonDictionary.TryGetValue("purchasedSkus", out var purchasedSkusObject);
                jsonDictionary.TryGetValue("referenceId", out var referenceIdObject);
                jsonDictionary.TryGetValue("relatedSkus", out var relatedSkusObject);
                
                if (activePurchasesObject != null)
                {
                    if (Json.Deserialize((string)activePurchasesObject) is List<string> jsonList)
                    {
                        ActivePurchases = jsonList.Select(jsonString => new NamiPurchase(jsonString)).ToList();
                    }
                }
                Desc = (string)descObject;
                Name = (string)nameObject;
                NamiId = (string)namiIdObject;
                if (purchasedSkusObject != null)
                {
                    if (Json.Deserialize((string)purchasedSkusObject) is List<string> jsonList)
                    {
                        PurchasedSKUs = jsonList.Select(jsonString => new NamiSKU(jsonString)).ToList();
                    }
                }
                ReferenceId = (string)referenceIdObject;
                if (relatedSkusObject != null)
                {
                    if (Json.Deserialize((string)relatedSkusObject) is List<string> jsonList)
                    {
                        RelatedSKUs = jsonList.Select(jsonString => new NamiSKU(jsonString)).ToList();
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