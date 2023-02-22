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

        [Nami configureWith:namiConfig];
    }
}
