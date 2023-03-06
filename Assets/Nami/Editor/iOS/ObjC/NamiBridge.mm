#import <StoreKit/StoreKit.h>
#import "NamiApple/NamiApple-Swift.h"
#import "NamiJsonUtils.h"
#import "NamiUtils.h"
#import "NamiCallbacks.h"

#pragma mark - C interface

extern "C" {
static StringCallbackDelegate* StringCallback;

void _init_stringCallback(StringCallbackDelegate stringCallback){
    if (StringCallback == NULL){
        StringCallback = stringCallback;
    }
}

void _nm_init(char *configurationJson){
    NSString* configurationString = [NamiUtils createNSStringFrom:configurationJson];
    NSDictionary* configurationDict = [NamiJsonUtils deserializeDictionary:configurationString];
    
    NamiConfiguration* namiConfig = [NamiConfiguration configurationForAppPlatformId:configurationDict[@"appPlatformId"]];
    namiConfig.logLevel = (NamiLogLevel)(NSInteger)configurationDict[@"logLevel"];
    namiConfig.bypassStore = configurationDict[@"bypassStore"];
    namiConfig.fullScreenPresentation = configurationDict[@"fullScreenPresentation"];
    namiConfig.namiLanguageCode = configurationDict[@"namiLanguageCode"];
    namiConfig.namiCommands = configurationDict[@"settingsList"];
    
    [Nami configureWith:namiConfig];
}

void _nm_launch(char *label, void* launchCallbackPtr, void* paywallActionCallbackPtr){
    NSString* labelString = [NamiUtils createNSStringFrom:label];
    
    [NamiCampaignManager launchWithLabel:labelString
                           launchHandler:^(BOOL success, NSError* error) {
        
        if (StringCallback == NULL || launchCallbackPtr == NULL){
            return;
        }
        
        NSMutableDictionary* dictionary = [NSMutableDictionary dictionary];
        dictionary[@"success"] = @(success);
        dictionary[@"error"] = [error localizedDescription];
        NSString* serializedDictionary = [NamiJsonUtils serializeDictionary:dictionary];
        
        dispatch_async(dispatch_get_main_queue(), ^{
            StringCallback(launchCallbackPtr, [NamiUtils createCStringFrom:serializedDictionary]);
        });
    }
                    paywallActionHandler:^(NamiPaywallAction action, NamiSKU * sku, NSError * error, NSArray<NamiPurchase *> * purchases) {
        
        if (StringCallback == NULL || paywallActionCallbackPtr == NULL){
            return;
        }
        
        NSMutableDictionary* dictionary = [NSMutableDictionary dictionary];
        dictionary[@"action"] = @(action);
        dictionary[@"sku"] = [NamiJsonUtils serializeNamiSKU:sku];
        dictionary[@"error"] = [error localizedDescription];
        dictionary[@"purchases"] = [NamiJsonUtils serializeNamiPurchaseArray:purchases];
        NSString* serializedDictionary = [NamiJsonUtils serializeDictionary:dictionary];
        
        dispatch_async(dispatch_get_main_queue(), ^{
            StringCallback(paywallActionCallbackPtr, [NamiUtils createCStringFrom:serializedDictionary]);
        });
    }];
    
}

char* _nm_allCampaigns(){
    NSMutableDictionary* dictionary = [NSMutableDictionary dictionary];
    dictionary[@"campaigns"] = [NamiJsonUtils serializeNamiCampaignArray:[NamiCampaignManager allCampaigns]];
    NSString* serializedDictionary = [NamiJsonUtils serializeDictionary:dictionary];
    
    return [NamiUtils createCStringFrom:serializedDictionary];
}

void _nm_registerAvailableCampaignsHandler(void* availableCampaignsCallbackPtr){
    [NamiCampaignManager registerAvailableCampaignsHandler:^(NSArray<NamiCampaign *> * campaigns) {
        
        if (StringCallback == NULL || availableCampaignsCallbackPtr == NULL){
            return;
        }
        
        NSMutableDictionary* dictionary = [NSMutableDictionary dictionary];
        dictionary[@"campaigns"] = [NamiJsonUtils serializeNamiCampaignArray:campaigns];
        NSString* serializedDictionary = [NamiJsonUtils serializeDictionary:dictionary];
        
        dispatch_async(dispatch_get_main_queue(), ^{
            StringCallback(availableCampaignsCallbackPtr, [NamiUtils createCStringFrom:serializedDictionary]);
        });
    }];
}

bool _nm_isLoggedIn(){
    return [NamiCustomerManager isLoggedIn];
}

char* _nm_journeyState(){
    NSMutableDictionary* dictionary = [NSMutableDictionary dictionary];
    dictionary[@"journeyState"] = [NamiJsonUtils serializeCustomerJourneyState:[NamiCustomerManager journeyState]];
    NSString* serializedDictionary = [NamiJsonUtils serializeDictionary:dictionary];
    
    return [NamiUtils createCStringFrom:serializedDictionary];
}

char* _nm_loggedInId(){
    NSString* loggedInId = [NamiCustomerManager loggedInId];
    return [NamiUtils createCStringFrom:loggedInId];
}

void _nm_login(char* withId){
    [NamiCustomerManager loginWithId:[NamiUtils createNSStringFrom:withId] loginCompleteHandler:^(BOOL success, NSError * error) {
    }];
}

void _nm_logout(){
    [NamiCustomerManager logoutWithLogoutCompleteHandler:^(BOOL success, NSError * error) {
    }];
}

void _nm_registerAccountStateHandler(void* accountStateCallbackPtr){
    [NamiCustomerManager registerAccountStateHandler:^(enum AccountStateAction accountStateAction, BOOL success, NSError * error) {
        
        if (StringCallback == NULL || accountStateCallbackPtr == NULL){
            return;
        }
        
        NSMutableDictionary* dictionary = [NSMutableDictionary dictionary];
        dictionary[@"accountStateAction"] = @(accountStateAction);
        dictionary[@"success"] = @(success);
        dictionary[@"error"] = [error localizedDescription];
        NSString* serializedDictionary = [NamiJsonUtils serializeDictionary:dictionary];
        
        dispatch_async(dispatch_get_main_queue(), ^{
            StringCallback(accountStateCallbackPtr, [NamiUtils createCStringFrom:serializedDictionary]);
        });
    }];
}

void _nm_registerJourneyStateHandler(void* journeyStateCallbackPtr){
    [NamiCustomerManager registerJourneyStateHandler:^(CustomerJourneyState * journeyState) {
        
        if (StringCallback == NULL || journeyStateCallbackPtr == NULL){
            return;
        }
        
        NSMutableDictionary* dictionary = [NSMutableDictionary dictionary];
        dictionary[@"journeyState"] = [NamiJsonUtils serializeCustomerJourneyState:journeyState];
        NSString* serializedDictionary = [NamiJsonUtils serializeDictionary:dictionary];
        
        dispatch_async(dispatch_get_main_queue(), ^{
            StringCallback(journeyStateCallbackPtr, [NamiUtils createCStringFrom:serializedDictionary]);
        });
    }];
}

char* _nm_active(){
    NSMutableDictionary* dictionary = [NSMutableDictionary dictionary];
    dictionary[@"entitlements"] = [NamiJsonUtils serializeNamiEntitlementArray:[NamiEntitlementManager active]];
    NSString* serializedDictionary = [NamiJsonUtils serializeDictionary:dictionary];
    
    return [NamiUtils createCStringFrom:serializedDictionary];
}

bool _nm_isEntitlementActive(char* referenceId){
    return [NamiEntitlementManager isEntitlementActive:[NamiUtils createNSStringFrom:referenceId]];
}

void _nm_refresh(void* refreshCallbackPtr){
    [NamiEntitlementManager refresh:^(NSArray<NamiEntitlement *> * entitlements) {
        
        if (StringCallback == NULL || refreshCallbackPtr == NULL){
            return;
        }
        
        NSMutableDictionary* dictionary = [NSMutableDictionary dictionary];
        dictionary[@"entitlements"] = [NamiJsonUtils serializeNamiEntitlementArray:entitlements];
        NSString* serializedDictionary = [NamiJsonUtils serializeDictionary:dictionary];
        
        dispatch_async(dispatch_get_main_queue(), ^{
            StringCallback(refreshCallbackPtr, [NamiUtils createCStringFrom:serializedDictionary]);
        });
    }];
}

void _nm_registerActiveEntitlementsHandler(void* activeEntitlementsCallbackPtr){
    [NamiEntitlementManager registerActiveEntitlementsHandler:^(NSArray<NamiEntitlement *> * entitlements) {
        
        if (StringCallback == NULL || activeEntitlementsCallbackPtr == NULL){
            return;
        }
        
        NSMutableDictionary* dictionary = [NSMutableDictionary dictionary];
        dictionary[@"entitlements"] = [NamiJsonUtils serializeNamiEntitlementArray:entitlements];
        NSString* serializedDictionary = [NamiJsonUtils serializeDictionary:dictionary];
        
        dispatch_async(dispatch_get_main_queue(), ^{
            StringCallback(activeEntitlementsCallbackPtr, [NamiUtils createCStringFrom:serializedDictionary]);
        });
    }];
}

void _nm_dismiss(bool animated, void* completionCallbackPtr){
    [NamiPaywallManager dismissWithAnimated:animated completion:^{
        
        if (StringCallback == NULL || completionCallbackPtr == NULL){
            return;
        }
        
        dispatch_async(dispatch_get_main_queue(), ^{
            StringCallback(completionCallbackPtr, NULL);
        });
    }];
}

void _nm_registerCloseHandler(void* closeCallbackPtr){
    [NamiPaywallManager registerCloseHandler:^(UIViewController * paywall) {
        
        if (StringCallback == NULL || closeCallbackPtr == NULL){
            return;
        }
        
        dispatch_async(dispatch_get_main_queue(), ^{
            StringCallback(closeCallbackPtr, NULL);
        });
    }];
}

void _nm_registerSignInHandler(void* signInCallbackPtr){
    [NamiPaywallManager registerSignInHandler:^(UIViewController * paywall) {
        
        if (StringCallback == NULL || signInCallbackPtr == NULL){
            return;
        }
        
        dispatch_async(dispatch_get_main_queue(), ^{
            StringCallback(signInCallbackPtr, NULL);
        });
    }];
}

void _nm_registerBuySkuHandler(void* buySkuCallbackPtr){
    [NamiPaywallManager registerBuySkuHandler:^(UIViewController * paywall, NamiSKU * sku) {
        
        if (StringCallback == NULL || buySkuCallbackPtr == NULL){
            return;
        }
        
        NSString* skuRefId = [sku id];
        dispatch_async(dispatch_get_main_queue(), ^{
            StringCallback(buySkuCallbackPtr, [NamiUtils createCStringFrom:skuRefId]);
        });
    }];
}

void _nm_buySkuComplete(char* purchase, char* skuRefId){
    // TODO
}

void _nm_consumePurchasedSku(char* skuId){
    [NamiPurchaseManager consumePurchasedSkuWithSkuId:[NamiUtils createNSStringFrom:skuId]];
}

void _nm_registerPurchasesChangedHandler(void* purchasesChangedCallbackPtr){
    [NamiPurchaseManager registerPurchasesChangedHandler:^(NSArray<NamiPurchase *> * purchases, enum NamiPurchaseState purchaseState, NSError * error) {
        
        if (StringCallback == NULL || purchasesChangedCallbackPtr == NULL){
            return;
        }
        
        NSMutableDictionary* dictionary = [NSMutableDictionary dictionary];
        dictionary[@"purchases"] = [NamiJsonUtils serializeNamiPurchaseArray:purchases];
        dictionary[@"purchaseState"] = @(purchaseState);
        dictionary[@"error"] = [error localizedDescription];
        NSString* serializedDictionary = [NamiJsonUtils serializeDictionary:dictionary];
        
        dispatch_async(dispatch_get_main_queue(), ^{
            StringCallback(purchasesChangedCallbackPtr, [NamiUtils createCStringFrom:serializedDictionary]);
        });
    }];
}

bool _nm_isSkuIdPurchased(char* skuId){
    return [NamiPurchaseManager skuPurchased:[NamiUtils createNSStringFrom:skuId]];
}

void _nm_presentCodeRedemptionSheet(){
    [NamiPurchaseManager presentCodeRedemptionSheet];
}

void _nm_registerRestorePurchasesHandler(void* restorePurchasesCallbackPtr){
    [NamiPurchaseManager registerRestorePurchasesHandlerWithRestorePurchasesStateHandler:^(enum NamiRestorePurchasesState restorePurchaseState, NSArray<NamiPurchase *> * newPurchases, NSArray<NamiPurchase *> * oldPurchases, NSError * error) {
        
        if (StringCallback == NULL || restorePurchasesCallbackPtr == NULL){
            return;
        }
        
        NSMutableDictionary* dictionary = [NSMutableDictionary dictionary];
        dictionary[@"restorePurchaseState"] = @(restorePurchaseState);
        dictionary[@"newPurchases"] = [NamiJsonUtils serializeNamiPurchaseArray:newPurchases];
        dictionary[@"oldPurchases"] = [NamiJsonUtils serializeNamiPurchaseArray:oldPurchases];
        dictionary[@"error"] = [error localizedDescription];
        NSString* serializedDictionary = [NamiJsonUtils serializeDictionary:dictionary];
        
        dispatch_async(dispatch_get_main_queue(), ^{
            StringCallback(restorePurchasesCallbackPtr, [NamiUtils createCStringFrom:serializedDictionary]);
        });
    }];
}

void _nm_restorePurchases(void* restorePurchasesCallbackPtr){
    [NamiPurchaseManager restorePurchasesWithStatehandler:^(enum NamiRestorePurchasesState restorePurchaseState, NSArray<NamiPurchase *> * newPurchases, NSArray<NamiPurchase *> * oldPurchases, NSError * error) {
        
        if (StringCallback == NULL || restorePurchasesCallbackPtr == NULL){
            return;
        }
        
        NSMutableDictionary* dictionary = [NSMutableDictionary dictionary];
        dictionary[@"restorePurchaseState"] = @(restorePurchaseState);
        dictionary[@"newPurchases"] = [NamiJsonUtils serializeNamiPurchaseArray:newPurchases];
        dictionary[@"oldPurchases"] = [NamiJsonUtils serializeNamiPurchaseArray:oldPurchases];
        dictionary[@"error"] = [error localizedDescription];
        NSString* serializedDictionary = [NamiJsonUtils serializeDictionary:dictionary];
        
        dispatch_async(dispatch_get_main_queue(), ^{
            StringCallback(restorePurchasesCallbackPtr, [NamiUtils createCStringFrom:serializedDictionary]);
        });
    }];
}

}
