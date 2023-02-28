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
        
        NSMutableDictionary* dictionary = [NSMutableDictionary dictionary];
        dictionary[@"success"] = @(success);
        dictionary[@"error"] = [error localizedDescription];
        NSString* serializedDictionary = [NamiJsonUtils serializeDictionary:dictionary];
        
        StringCallback(launchCallbackPtr, [NamiUtils createCStringFrom:serializedDictionary]);
    }
                    paywallActionHandler:^(NamiPaywallAction action, NamiSKU * sku, NSError * error, NSArray<NamiPurchase *> * purchases) {
        
        NSMutableDictionary* dictionary = [NSMutableDictionary dictionary];
        dictionary[@"action"] = @(action);
        dictionary[@"sku"] = [NamiJsonUtils serializeNamiSKU:sku];
        dictionary[@"error"] = [error localizedDescription];
        dictionary[@"purchases"] = [NamiJsonUtils serializeNamiPurchaseArray:purchases];
        NSString* serializedDictionary = [NamiJsonUtils serializeDictionary:dictionary];
        
        StringCallback(paywallActionCallbackPtr, [NamiUtils createCStringFrom:serializedDictionary]);
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
        
        NSMutableDictionary* dictionary = [NSMutableDictionary dictionary];
        dictionary[@"campaigns"] = [NamiJsonUtils serializeNamiCampaignArray:campaigns];
        NSString* serializedDictionary = [NamiJsonUtils serializeDictionary:dictionary];
        
        StringCallback(availableCampaignsCallbackPtr, [NamiUtils createCStringFrom:serializedDictionary]);
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
        
        NSMutableDictionary* dictionary = [NSMutableDictionary dictionary];
        dictionary[@"accountStateAction"] = @(accountStateAction);
        dictionary[@"success"] = @(success);
        dictionary[@"error"] = [error localizedDescription];
        NSString* serializedDictionary = [NamiJsonUtils serializeDictionary:dictionary];
        
        StringCallback(accountStateCallbackPtr, [NamiUtils createCStringFrom:serializedDictionary]);
    }];
}

void _nm_registerJourneyStateHandler(void* journeyStateCallbackPtr){
    [NamiCustomerManager registerJourneyStateHandler:^(CustomerJourneyState * journeyState) {
        
        NSMutableDictionary* dictionary = [NSMutableDictionary dictionary];
        dictionary[@"journeyState"] = [NamiJsonUtils serializeCustomerJourneyState:journeyState];
        NSString* serializedDictionary = [NamiJsonUtils serializeDictionary:dictionary];
        
        StringCallback(journeyStateCallbackPtr, [NamiUtils createCStringFrom:serializedDictionary]);
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
        
        NSMutableDictionary* dictionary = [NSMutableDictionary dictionary];
        dictionary[@"entitlements"] = [NamiJsonUtils serializeNamiEntitlementArray:entitlements];
        NSString* serializedDictionary = [NamiJsonUtils serializeDictionary:dictionary];
        
        StringCallback(refreshCallbackPtr, [NamiUtils createCStringFrom:serializedDictionary]);
    }];
}

void _nm_registerActiveEntitlementsHandler(void* activeEntitlementsCallbackPtr){
    [NamiEntitlementManager registerActiveEntitlementsHandler:^(NSArray<NamiEntitlement *> * entitlements) {
        
        NSMutableDictionary* dictionary = [NSMutableDictionary dictionary];
        dictionary[@"entitlements"] = [NamiJsonUtils serializeNamiEntitlementArray:entitlements];
        NSString* serializedDictionary = [NamiJsonUtils serializeDictionary:dictionary];
        
        StringCallback(activeEntitlementsCallbackPtr, [NamiUtils createCStringFrom:serializedDictionary]);
    }];
}


void _nm_registerCloseHandler(void* closeCallbackPtr){
    [NamiPaywallManager registerCloseHandler:^(UIViewController * paywall) {
        StringCallback(closeCallbackPtr, NULL);
    }];
}

void _nm_registerSignInHandler(void* signInCallbackPtr){
    [NamiPaywallManager registerSignInHandler:^(UIViewController * paywall) {
        StringCallback(signInCallbackPtr, NULL);
    }];
}

void _nm_registerBuySkuHandler(void* buySkuCallbackPtr){
    [NamiPaywallManager registerBuySkuHandler:^(UIViewController * paywall, NamiSKU * sku) {
        NSString* skuRefId = [sku id];
        StringCallback(buySkuCallbackPtr, [NamiUtils createCStringFrom:skuRefId]);
    }];
}

void _nm_buySkuComplete(char* purchase, char* skuRefId){
    // TODO
}


/* TODO
 void _nm_consumePurchasedSku(char* skuId);
 
 void _nm_registerPurchasesChangedHandler(void* purchasesChangedCallbackPtr);
 
 bool _nm_isSkuIdPurchased(char* skuId);
 */

}
