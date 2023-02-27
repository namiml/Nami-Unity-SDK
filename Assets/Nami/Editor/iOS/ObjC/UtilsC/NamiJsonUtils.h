#import <Foundation/Foundation.h>
#import "NamiApple/NamiApple-Swift.h"

@interface NamiJsonUtils : NSObject

+ (NSString *)serializeArray:(NSMutableArray *)array;
+ (NSString *)serializeDictionary:(NSDictionary *)dictionary;

+ (NSArray *)deserializeArray:(NSString *)jsonArray;
+ (NSDictionary *)deserializeDictionary:(NSString *)jsonDic;
+ (NSArray<NSNumber *> *)deserializeNumbersArray:(NSArray *)numbers;

+ (NSDictionary *)serializeNamiSKU:(NamiSKU *)sku;
+ (NSArray *)serializeNamiSKUArray:(NSArray<NamiSKU *> *)skus;
+ (NSDictionary *)serializeNamiEntitlement:(NamiEntitlement *)entitlement;
+ (NSArray *)serializeNamiEntitlementArray:(NSArray<NamiEntitlement *> *)entitlements;
+ (NSDictionary *)serializeNamiPurchase:(NamiPurchase *)purchase;
+ (NSArray *)serializeNamiPurchaseArray:(NSArray<NamiPurchase *> *)purchases;

@end
