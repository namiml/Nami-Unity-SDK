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
        dictionary[@"error"] = @([NamiUtils createCStringFrom:[error localizedDescription]]);
        NSString* serializedDictionary = [NamiJsonUtils serializeDictionary:dictionary];
        
        StringCallback(launchCallbackPtr, [NamiUtils createCStringFrom:serializedDictionary]);
    }
                    paywallActionHandler:^(NamiPaywallAction action, NamiSKU * sku, NSError * error, NSArray<NamiPurchase *> * purchases) {
        
        NSMutableDictionary* dictionary = [NSMutableDictionary dictionary];
        dictionary[@"action"] = @(action);
        dictionary[@"sku"] = @([NamiUtils createCStringFrom:[NamiJsonUtils serializeNamiSKU:sku]]);
        dictionary[@"error"] = @([NamiUtils createCStringFrom:[error localizedDescription]]);
        dictionary[@"purchases"] = @([NamiUtils createCStringFrom:[NamiJsonUtils serializeNamiPurchaseArray:purchases]]);
        NSString* serializedDictionary = [NamiJsonUtils serializeDictionary:dictionary];
        
        StringCallback(paywallActionCallbackPtr, [NamiUtils createCStringFrom:@"-------------> Test Message"]);
    }];
    
}
}
