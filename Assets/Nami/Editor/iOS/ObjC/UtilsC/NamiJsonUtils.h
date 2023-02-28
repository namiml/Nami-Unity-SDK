#import <Foundation/Foundation.h>
#import "NamiApple/NamiApple-Swift.h"

@interface NamiJsonUtils : NSObject

+ (NSString *)serializeArray:(NSMutableArray *)array;
+ (NSString *)serializeDictionary:(NSDictionary *)dictionary;

+ (NSArray *)deserializeArray:(NSString *)jsonArray;
+ (NSDictionary *)deserializeDictionary:(NSString *)jsonDic;
+ (NSArray<NSNumber *> *)deserializeNumbersArray:(NSArray *)numbers;

+ (NSDictionary *)serializeNamiSKU:(NamiSKU *)sku;
+ (NSMutableArray *)serializeNamiSKUArray:(NSArray<NamiSKU *> *)skus;
+ (NSDictionary *)serializeNamiEntitlement:(NamiEntitlement *)entitlement;
+ (NSMutableArray *)serializeNamiEntitlementArray:(NSArray<NamiEntitlement *> *)entitlements;
+ (NSDictionary *)serializeNamiPurchase:(NamiPurchase *)purchase;
+ (NSMutableArray *)serializeNamiPurchaseArray:(NSArray<NamiPurchase *> *)purchases;
+ (NSDictionary *)serializeNamiCampaign:(NamiCampaign *)campaign;
+ (NSMutableArray *)serializeNamiCampaignArray:(NSArray<NamiCampaign *> *)campaigns;
+ (NSDictionary *)serializeCustomerJourneyState:(CustomerJourneyState *)journetState;

@end
