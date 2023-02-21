namespace NamiSdk
{
    public enum NamiPaywallAction
    {
        ClosePaywall,
        RestorePurchases,
        SignIn,
        BuySku,
        SelectSku,
#if UNITY_IOS
        PurchaseSelectedSku,
        PurchaseSuccess,
        PurchaseDeferred,
        PurchaseFailed,
        PurchaseCancelled,
        PurchaseUnknown
#endif
    }
}