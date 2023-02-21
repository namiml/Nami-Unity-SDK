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
            TransactionIdentifier = ajo.CallStr("getTransactionIdentifier");
            PurchaseToken = ajo.CallStr("getPurchaseToken");
        }

        public long PurchaseInitiatedTimestamp { get; private set; }
        public DateTime Expires { get; private set; }
        public NamiPurchaseSource PurchaseSource { get; private set; }
        public string SkuId { get; private set; }
        public string TransactionIdentifier { get; private set; }
        public string PurchaseToken { get; private set; }
    }

    // TODO implemetn NamiPurchase for iOS

    /*
        App Store

        sku - a NamiSKU object representing the in-app purchase product SKU the device purchased.
        expires - date the purchase expires if it is a subscription
        entitlementGranted - a NamiEntitlement object for the entitlement granted by this purchase.
        transactionIdentifier - App Store ID for the transaction
        transaction - the StoreKit transaction object for the purchase
        skuId - the App Store reference ID of the purchased product SKU
        purchaseInitiatedTimestamp - The date and time when the purchase was initiated

        Google Play

        purchaseInitiatedTimestamp - The date and time when the purchase was initiated
        expires - (bypass store only) Indicates when this purchase will cease
        purchaseSource - NamiPurchaseSource
        skuId - the Google Play reference ID of the purchased product SKU
        transactionIdentifier - The purchase order ID record associated to this purchase
        purchaseToken - The purchase token associated to this purchase
     */
}