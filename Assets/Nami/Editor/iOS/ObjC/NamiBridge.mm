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

/* TODO
bool _nm_isLoggedIn();

char* _nm_journeyState();

char* _nm_loggedInId();

void _nm_login(char* withId);

void _nm_logout();

void _nm_registerAccountStateHandler(void* accountStateCallbackPtr);

void _nm_registerJourneyStateHandler(void* journeyStateCallbackPtr);
*/

/* TODO
char* _nm_active();

bool _nm_isEntitlementActive(char* referenceId);

void _nm_refresh(void* refreshCallbackPtr);

void _nm_registerActiveEntitlementsHandler(void* activeEntitlementsCallbackPtr);
*/
*/

}
