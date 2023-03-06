using System;
using System.Collections.Generic;
using System.Linq;
using NamiSDK.MiniJSON;
using NamiSDK.Utils;
using UnityEngine;

namespace NamiSDK
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

        public NamiPurchase(object json)
        {
            var dictionary = Json.DeserializeDictionary(json);
            if (dictionary != null)
            {
                dictionary.TryGetValue("purchaseInitiatedTimestamp", out var purchaseInitiatedTimestampObject);
                dictionary.TryGetValue("expires", out var expiresObject);
                dictionary.TryGetValue("skuId", out var skuIdObject);
                dictionary.TryGetValue("transactionIdentifier", out var transactionIdentifierObject);
                dictionary.TryGetValue("sku", out var skuObject);
                dictionary.TryGetValue("entitlementsGranted", out var entitlementsGrantedObject);
                dictionary.TryGetValue("transaction", out var transactionObject);

                if (purchaseInitiatedTimestampObject != null) PurchaseInitiatedTimestamp = (long)(double)purchaseInitiatedTimestampObject;
                if (expiresObject != null) Expires = expiresObject.ToDateTime();
                SkuId = (string)skuIdObject;
                TransactionIdentifier = (string)transactionIdentifierObject;
                if (skuObject != null) Sku = new NamiSKU(skuObject);
                if (entitlementsGrantedObject != null)
                {
                    var list = Json.DeserializeList(entitlementsGrantedObject);
                    if (list != null)
                    {
                        EntitlementsGranted = list.Select(jsonObject => new NamiEntitlement(jsonObject)).ToList();
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