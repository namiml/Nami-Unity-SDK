#import <Foundation/Foundation.h>
#import "NamiApple/NamiApple-Swift.h"

@interface NamiJsonUtils : NSObject

+ (NSString *)serializeArray:(NSMutableArray *)array;
+ (NSString *)serializeDictionary:(NSDictionary *)dictionary;

+ (NSArray *)deserializeArray:(NSString *)jsonArray;
+ (NSDictionary *)deserializeDictionary:(NSString *)jsonDic;
+ (NSArray<NSNumber *> *)deserializeNumbersArray:(NSArray *)numbers;

+ (NSString *)serializeNamiSKU:(NamiSKU *)sku;
+ (NSString *)serializeNamiSKUArray:(NSArray<NamiSKU *> *)skus;
+ (NSString *)serializeNamiEntitlement:(NamiEntitlement *)entitlement;
+ (NSString *)serializeNamiEntitlementArray:(NSArray<NamiEntitlement *> *)entitlements;
+ (NSString *)serializeNamiPurchase:(NamiPurchase *)purchase;
+ (NSString *)serializeNamiPurchaseArray:(NSArray<NamiPurchase *> *)purchases;

@end
