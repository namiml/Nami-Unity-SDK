using System;
using System.Collections.Generic;
using System.Linq;
using NamiSdk.MiniJSON;
using NamiSdk.Utils;
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
            TransactionIdentifier = ajo.CallStr("getTransactionIdentifier");
            PurchaseToken = ajo.CallStr("getPurchaseToken");
        }

        public NamiPurchase(string json)
        {
            if (Json.Deserialize(json) is Dictionary<string, object> jsonDictionary)
            {
                jsonDictionary.TryGetValue("purchaseInitiatedTimestamp", out var purchaseInitiatedTimestampObject);
                jsonDictionary.TryGetValue("expires", out var expiresObject);
                jsonDictionary.TryGetValue("skuId", out var skuIdObject);
                jsonDictionary.TryGetValue("transactionIdentifier", out var transactionIdentifierObject);
                jsonDictionary.TryGetValue("sku", out var skuObject);
                jsonDictionary.TryGetValue("entitlementsGranted", out var entitlementsGrantedObject);
                jsonDictionary.TryGetValue("transaction", out var transactionObject);

                if (purchaseInitiatedTimestampObject != null) PurchaseInitiatedTimestamp = (long)(double)purchaseInitiatedTimestampObject;
                if (expiresObject != null) Expires = expiresObject.ToDateTime();
                SkuId = (string)skuIdObject;
                TransactionIdentifier = (string)transactionIdentifierObject;
                if (skuObject != null) Sku = new NamiSKU((string)skuObject);
                if (entitlementsGrantedObject != null)
                {
                    if (Json.Deserialize((string)entitlementsGrantedObject) is List<string> jsonList)
                    {
                        EntitlementsGranted = jsonList.Select(jsonString => new NamiEntitlement(jsonString)).ToList();
                    }
                }
                Transaction = (string)transactionObject;
            }
        }

        public long PurchaseInitiatedTimestamp { get; private set; }

        public DateTime Expires { get; private set; }

        /// <summary> Android platforms only </summary>
        public NamiPurchaseSource PurchaseSource { get; private set; }

        public string SkuId { get; private set; }

        public string TransactionIdentifier { get; private set; }

        /// <summary> Android platforms only </summary>
        public string PurchaseToken { get; private set; }

        /// <summary> Apple platforms only </summary>
        public NamiSKU Sku { get; private set; }

        /// <summary> Apple platforms only </summary>
        public List<NamiEntitlement> EntitlementsGranted { get; private set; }

        /// <summary> Apple platforms only </summary>
        public string Transaction { get; private set; }
    }
}