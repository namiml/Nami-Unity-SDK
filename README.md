# Nami Unity SDK

Nami is a cloud service that helps mobile app publishers easily implement and sell in-app purchases and subscriptions.
You can use Nami Unity SDK to provide the features for you Unity app.

## Supported platforms

- iOS
- Android

## Requirements

Unity 2020.3.44f1+

## Installation

Download latest version of Nami Unity SDK (.unitypackage). In Unity editor right click inside Project pane and select Import package -> Custom package. Select file you've downloaded and confirm. Now Nami Unity SDK should be ready to use.

## Configuration

In Unity editor go to Window -> Nami -> Edit Settings and enter your app platform id.

## Usage

### Setup

Initialize the client-side Nami Unity SDK. Required for Nami to see paywalls and campaigns defined on the Nami server.

```
var configuration = new NamiConfiguration.Builder().Build();
Nami.Init(configuration);
```

### NamiConfiguration
Class used to initialize the Nami Unity SDK when an app starts.
#### Parameters
- appPlatformId - the Application Platform ID from the Control Center. Leave this parameter empty to use the Target Platform ID from NamiSettings.
- bypassStore - when true, transactions will not be sent to the store. This allows for simplified testing in development.
- logLevel - set the level of logging printed by the SDK for debugging.
- namiLanguageCode - sets the language used for campaign targeting.

```
new NamiConfiguration.Builder()
    .BypassStore(false)
    .LogLevel(NamiLogLevel.Warn)
    .NamiLanguageCode(NamiLanguageCode.EN)
    .Build();
```

### Launch
Launch a live campaign in your app and show the associated paywall.
#### Parameters
- label - a string matching the label set in the Control Center
- launchHandler - this can be used to know if the launch succeeded or failed to raise a paywall.
- paywallActionHandler - use this to monitor user interactions with the paywall raised by this campaign launch. 

```
NamiCampaignManager.Launch(label);
```

Callback handlers example for Android platforms:

```
var launchHandler = new LaunchHandler(() =>
{
    // on success
}, error =>
{
    // on failure
}, (purchaseState, activePurchases, errorMsg) =>
{
    // on purchase changed
});

var paywallActionHandler = new PaywallActionHandler((namiPaywallAction, sku) =>
{
    // on patwall action
});
            
NamiCampaignManager.Launch(label, launchHandler, paywallActionHandler);
```

Callback handlers example for iOS platforms:

```
var launchHandler = new LaunchHandler((isSuccess, errorMsg) =>
{
    // on launch
});

var paywallActionHandler = new PaywallActionHandler((namiPaywallAction, sku, errorMsg, purchases) =>
{
    // on patwall action
});

NamiCampaignManager.Launch(label, launchHandler, paywallActionHandler);
```

### NamiSKU
Object that contains all the data on a in-app purchase SKU for an App Platform.
#### Parameters (read only)
- Name - The name of the product as set in the Nami Control Center.
- SkuId - The in-app purchase or subscription reference ID from the App Store or Google Play.
- Product - (Apple only) additional product info from Apple StoreKit.
- SkuDetails - (GooglePlay only) additional product info from Google Play Billing.
- Type - indicates subscription or one time purchase.

### NamiCampaign
The campaign object represents the live campaigns configured in the Nami Control Center that are available to the device after all campaign filtering and ordering rules are applied.
#### Parameters (read only)
- Id (iOS only)
- Rule (iOS only)
- Paywall
- Segment
- Type (Android only)
- Value

```
// returns a list of campaigns that are available to for the device.

var campaigns = NamiCampaignManager.AllCampaigns();
```

### Login
Provide a unique identifier that can be used to link different devices to the same customer in the Nami platform. This customer id will also be returned in any data sent from the Nami servers to your systems as well.

The ID sent to Nami must be a valid UUID or you may hash any other identifier with SHA256 and provide it in this call.
#### Parameters
- withId - a string of the unique customer id in UUID or SHA256 format

```
NamiCustomerManager.Login(customerId);
```

### Logout
Disassociate a device from an external id.

```
NamiCustomerManager.Logout();
```

### IsLoggedIn
Return if a user is currently logged into the device.

```
var isLoggedIn = NamiCustomerManager.IsLoggedIn;
```

### CustomerJourneyState
Class representing the state of a customer's subscription journey.
#### Parameters (read only)
- FormerSubscriber - indicates if the customer had subscribed in the past.
- InGracePeriod - indicates the subscription has lapsed due to a payment failure where the platform is still trying to actively recover the payment method and the granted entitlements should still be active.
- InTrialPeriod - indicates the customer is in a free trial.
- InIntroOfferPeriod - indicates the customer is in an introductory offer subscription, where their current price is less than the eventual full price.
- IsCancelled - indicates if the customer used to be a subscriber and cancelled their subscription renewal.
- InPause - (Android only) indicates if the customer's subscription is paused.
- InAccountHold - indicates if the subscription has lapsed due to a payment failure and the granted entitlements are no longer active.

```
// returns the current state of a customer's subscription journey

var customerJourneyState = NamiCustomerManager.JourneyState;
```

### Refresh
Manually trigger a refresh of the user's latest active entitlements from the Nami services.
#### Parameters
- refreshCallback - returns the entitlements list that can be used for processing.

```
NamiEntitlementManager.Refresh(RefreshCallback);

void RefreshCallback(List<NamiEntitlement> entitlements)
{
    // on refresh
}
```

### NamiEntitlement
Object that contains data about an entitlement on the Nami Platform.
#### Parameters (read only)
- ActivePurchases - a NamiPurchase object corresponding to the purchase that granted the entitlement. Will contain any general metadata know by the SDK. If the purchase was made on the current device, will contain additional platform-specific data.
- Desc - the description for the entitlement, set in the Control Center.
- Name - the name of the entitlement, set in the Control Center.
- NamiId - an internal id used by Nami for the entitlement.
- PurchasedSKUs - a list of NamiSKU objects for the purchased in-app products that granted the entitlement. May contain some general metadata about the in-app purchase - product when available. If the entitlement was purchased on device, this object will contain data about the in-app purchase product.
- ReferenceId - the unique id used to reference the entitlement, set in the Control Center
- RelatedSKUs - a list of NamiSKU objects. This is the list of all known in-app purchase products that can grant this entitlement. Set in the Control Center.

```
// returns all active entitlements for a user on the current device

var activeEntitlements = NamiEntitlementManager.Active();
```

## Callbacks
Register and handle callbacks for your app logic.

### RegisterAvailableCampaignsHandler
Receive a callback whenever the SDK gets back the current list of available NamiCampaign objects for the device. This list is personalized for the device by the Nami backend server based upon campaign filtering and priority rules.

```
NamiCampaignManager.RegisterAvailableCampaignsHandler(availableCampaignsCallback);
```

### RegisterAccountStateHandler
Register a callback that will be called whenever `NamiCustomerManager.Login` or `NamiCustomerManager.Logout` is called with results from those calls.

```
NamiCustomerManager.RegisterAccountStateHandler(accountStateCallback);
```

### RegisterJourneyStateHandler
Register a callback that will be made any time there's a change to the Journey State for the user. Note that Nami fetches journey state at the start of each session and this is the most likely time to see a change.

```
NamiCustomerManager.RegisterJourneyStateHandler(journeyStateCallback);
```

### RegisterActiveEntitlementsHandler
Register a callback to react to a potential changes to the active entitlements for the user, whenever such state is fetched from the Nami service. This occurs during the course of the application lifecycle as well as when `NamiEntitlementManager.Refresh` is called.

```
NamiEntitlementManager.RegisterActiveEntitlementsHandler(activeEntitlementsCallback);
```

### RegisterCloseHandler
If this registered, paywall `close` buttons will call back to this handler for your own custom business logic instead of using the system default, which is to just dismiss the paywall.

```
NamiPaywallManager.RegisterCloseHandler(CloseCallback);

void CloseCallback()
{
    // code

#if UNITY_IOS
    NamiPaywallManager.Dismiss();
#endif
}
```

### RegisterSignInHandler
Register a sign-in provider to handle your own sign-in logic.

```
NamiPaywallManager.RegisterSignInHandler(signInCallback);
```

### RegisterBuySkuHandler
By registering this handler, a Nami paywall will handoff to you when the user has selected a sku and asked for the purchase flow to start. It is you're responsible to listen to this handler and handle the purchase using the provided sku.

Once the purchase is successful, indicate it is complete by calling `NamiPaywallManager.BuySkuComplete`.

```
NamiPaywallManager.RegisterBuySkuHandler(buySkuCallback);
```

### RegisterPurchasesChangedHandler
Register a callback that will be made anytime there is a change to purchases made on the device. This will be triggered when a purchase process is started and may have different states based on the particular store platform.

```
NamiPurchaseManager.RegisterPurchasesChangedHandler(purchasesChangedCallback);
```

### RegisterRestorePurchasesHandler
For recommendations on where to present UI elements to your customers during a restore purchases process, see our Restoring Purchases guide.

```
NamiPurchaseManager.RegisterRestorePurchasesHandler(restorePurchasesCallback); // Apple only
```
