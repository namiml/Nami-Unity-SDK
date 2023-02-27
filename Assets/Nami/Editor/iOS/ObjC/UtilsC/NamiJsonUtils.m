#import "NamiJsonUtils.h"
#import "NamiUtils.h"

@implementation NamiJsonUtils

+ (NSString *)serializeArray:(NSArray *)array {
    NSError *error;
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:array options:nil error:&error];
    return [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
}

+ (NSString *)serializeDictionary:(NSDictionary *)dictionary {
    NSError *error;
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:dictionary options:nil error:&error];
    return [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
}

+ (NSArray *)deserializeArray:(NSString *)jsonArray {
    NSError *e = nil;
    NSArray *array = [NSJSONSerialization JSONObjectWithData:[jsonArray dataUsingEncoding:NSUTF8StringEncoding] options:NSJSONReadingMutableContainers error:&e];
    if (array != nil) {
        NSMutableArray *prunedArr = [NSMutableArray array];
        [array enumerateObjectsUsingBlock:^(id obj, NSUInteger idx, BOOL *stop) {
            if (![obj isKindOfClass:[NSNull class]]) {
                prunedArr[idx] = obj;
            }
        }];
        return prunedArr;
    }
    return array;
}

+ (NSDictionary *)deserializeDictionary:(NSString *)jsonDic {
    NSError *e = nil;
    NSDictionary *dictionary = [NSJSONSerialization JSONObjectWithData:[jsonDic dataUsingEncoding:NSUTF8StringEncoding] options:NSJSONReadingMutableContainers error:&e];
    if (dictionary != nil) {
        NSMutableDictionary *prunedDict = [NSMutableDictionary dictionary];
        [dictionary enumerateKeysAndObjectsUsingBlock:^(NSString *key, id obj, BOOL *stop) {
            if (![obj isKindOfClass:[NSNull class]]) {
                prunedDict[key] = obj;
            }
        }];
        return prunedDict;
    }
    return dictionary;
}

+ (NSArray<NSNumber *> *)deserializeNumbersArray:(NSArray *)numbers {
    NSMutableArray<NSNumber *> *result = [NSMutableArray new];
    
    for (NSNumber *n in numbers) {
        [result addObject:n];
    }
    
    return result;
}

+ (NSString *)serializeSKProduct:(SKProduct *)product {
    NSError *error;
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:product options:nil error:&error];
    return [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
}

+ (NSString *)serializeNamiSKU:(NamiSKU *)sku{
    NSMutableDictionary* dictionary = [NSMutableDictionary dictionary];
    dictionary[@"name"] = @([NamiUtils createCStringFrom:[sku name]]);
    dictionary[@"skuId"] = @([NamiUtils createCStringFrom:[sku skuId]]);
    dictionary[@"product"] = @([NamiUtils createCStringFrom:[self serializeSKProduct:[sku product]]]);
    dictionary[@"type"] = @([sku type]);
    return [self serializeDictionary:dictionary];
}

+ (NSString *)serializeNamiSKUArray:(NSArray<NamiSKU *> *)skus{
    NSUInteger count = skus.count;
    NSMutableArray* array = [NSMutableArray arrayWithCapacity:count];
    for (NSUInteger i = 0; i < count; ++i){
        [array addObject:[self serializeNamiSKU:skus[i]]];
    }
    return [self serializeArray:array];
}

+ (NSString *)serializeNamiEntitlement:(NamiEntitlement *)entitlement{
    NSMutableDictionary* dictionary = [NSMutableDictionary dictionary];
    dictionary[@"activePurchases"] = @([NamiUtils createCStringFrom:[self serializeNamiPurchaseArray:[entitlement activePurchases]]]);
    dictionary[@"desc"] = @([NamiUtils createCStringFrom:[entitlement desc]]);
    dictionary[@"name"] = @([NamiUtils createCStringFrom:[entitlement name]]);
    dictionary[@"namiId"] = @([NamiUtils createCStringFrom:[entitlement namiId]]);
    dictionary[@"purchasedSkus"] = @([NamiUtils createCStringFrom:[self serializeNamiSKUArray:[entitlement purchasedSkus]]]);
    dictionary[@"referenceId"] = @([NamiUtils createCStringFrom:[entitlement referenceId]]);
    dictionary[@"relatedSkus"] = @([NamiUtils createCStringFrom:[self serializeNamiSKUArray:[entitlement relatedSkus]]]);
    return [self serializeDictionary:dictionary];
}

+ (NSString *)serializeNamiEntitlementArray:(NSArray<NamiEntitlement *> *)entitlements{
    NSUInteger count = entitlements.count;
    NSMutableArray* array = [NSMutableArray arrayWithCapacity:count];
    for (NSUInteger i = 0; i < count; ++i){
        [array addObject:[self serializeNamiEntitlement:entitlements[i]]];
    }
    return [self serializeArray:array];
}

+ (NSString *)serializeNamiPurchase:(NamiPurchase *)purchase{
    NSMutableDictionary* dictionary = [NSMutableDictionary dictionary];
    dictionary[@"purchaseInitiatedTimestamp"] = @([[purchase purchaseInitiatedTimestamp] timeIntervalSince1970]);
    dictionary[@"expires"] = @([[purchase expires] timeIntervalSince1970]);
    dictionary[@"skuId"] = @([NamiUtils createCStringFrom:[purchase skuId]]);
    dictionary[@"transactionIdentifier"] = @([NamiUtils createCStringFrom:[purchase transactionIdentifier]]);
    dictionary[@"sku"] = @([NamiUtils createCStringFrom:[self serializeNamiSKU:[purchase sku]]]);
    dictionary[@"entitlementsGranted"] = @([NamiUtils createCStringFrom:[self serializeNamiEntitlementArray:[purchase entitlementsGranted]]]);
    dictionary[@"transaction"] = @([NamiUtils createCStringFrom:[purchase transactionIdentifier]]);
    return [self serializeDictionary:dictionary];
}

+ (NSString *)serializeNamiPurchaseArray:(NSArray<NamiPurchase *> *)purchases{
    NSUInteger count = purchases.count;
    NSMutableArray* array = [NSMutableArray arrayWithCapacity:count];
    for (NSUInteger i = 0; i < count; ++i){
        [array addObject:[self serializeNamiPurchase:purchases[i]]];
    }
    return [self serializeArray:array];
}

@end
