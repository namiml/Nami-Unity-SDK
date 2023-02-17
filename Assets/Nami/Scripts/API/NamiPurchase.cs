using System;
using NamiSdk.JNI;
using UnityEngine;

namespace NamiSdk
{
    public class NamiPurchase
    {
        public NamiPurchase(AndroidJavaObject ajo)
        {
            PurchaseInitiatedTimestamp = ajo.CallLong("getPurchaseInitiatedTimestamp");
            Expires = ajo.CallAJO("getExpires").JavaToDateTime();
            PurchaseSource = ajo.CallAJO("getPurchaseSource").JavaToEnum<NamiPurchaseSource>();
            SkuId = ajo.CallStr("getSkuId");
            SkuUUID = ajo.CallStr("getSkuUUID");
            TransactionIdentifier = ajo.CallStr("getTransactionIdentifier");
            PurchaseToken = ajo.CallStr("getPurchaseToken");
            LocalizedDescription = ajo.CallStr("getLocalizedDescription");
        }

        public long PurchaseInitiatedTimestamp { get; private set; }

        public DateTime Expires { get; private set; }

        public NamiPurchaseSource PurchaseSource { get; private set; }

        public string SkuId { get; private set; }

        public string SkuUUID { get; private set; }

        public string TransactionIdentifier { get; private set; }

        public string PurchaseToken { get; private set; }

        public string LocalizedDescription { get; private set; }
    }
}