#import <StoreKit/StoreKit.h>
#import "NamiApple/NamiApple-Swift.h"
#import "NamiJsonUtils.h"
#import "NamiUtils.h"

#pragma mark - C interface

extern "C" {
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

void _nm_launch(char *label){
    NSString* labelString = [NamiUtils createNSStringFrom:label];
    
    dispatch_async(dispatch_get_main_queue(), ^{
        [NamiCampaignManager launchWithLabel:labelString
                               launchHandler:^(BOOL isSuccess, NSError* error) {
            // TODO
        }
                        paywallActionHandler:^(NamiPaywallAction action, NamiSKU * sku, NSError * error, NSArray<NamiPurchase *> * purchases) {
            // TODO
        }];
    });
    
}
}
