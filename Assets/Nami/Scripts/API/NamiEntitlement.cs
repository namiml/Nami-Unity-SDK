using System.Collections.Generic;
using System.Linq;
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

        public List<NamiPurchase> ActivePurchases { get; private set; }
        public string Desc { get; private set; }
        public string Name { get; private set; }
        public string NamiId { get; private set; }
        public List<NamiSKU> PurchasedSKUs { get; private set; }
        public string ReferenceId { get; private set; }
        public List<NamiSKU> RelatedSKUs { get; private set; }
    }
}