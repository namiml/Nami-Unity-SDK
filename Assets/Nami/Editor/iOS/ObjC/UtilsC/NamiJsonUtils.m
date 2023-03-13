#import "NamiJsonUtils.h"
#import "NamiUtils.h"

@implementation NamiJsonUtils

+ (NSString *)serializeArray:(NSArray *)array {
    if (array == NULL){
        return NULL;
    }
    NSError *error;
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:array options:nil error:&error];
    return [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
}

+ (NSString *)serializeDictionary:(NSDictionary *)dictionary {
    if (dictionary == NULL){
        return NULL;
    }
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

+ (NSDictionary *)serializeSKProduct:(SKProduct *)product {
    if (product == NULL){
        return NULL;
    }
    NSMutableDictionary* dictionary = [NSMutableDictionary dictionary];
    dictionary[@"localizedDescription"] = product.localizedDescription;
    dictionary[@"localizedTitle"] = product.localizedTitle;
    dictionary[@"productIdentifier"] = product.productIdentifier;
    dictionary[@"price"] = [product.price stringValue];
    dictionary[@"priceLocale"] = product.priceLocale.localeIdentifier;
    return dictionary;
}

+ (NSDictionary *)serializeNamiSKU:(NamiSKU *)sku{
    if (sku == NULL){
        return NULL;
    }
    NSMutableDictionary* dictionary = [NSMutableDictionary dictionary];
    dictionary[@"name"] = [sku name];
    dictionary[@"skuId"] = [sku skuId];
    dictionary[@"product"] = [NamiJsonUtils serializeDictionary:[self serializeSKProduct:[sku product]]];
    dictionary[@"type"] = @([sku type]);
    return dictionary;
}

+ (NSMutableArray *)serializeNamiSKUArray:(NSArray<NamiSKU *> *)skus{
    if (skus == NULL){
        return NULL;
    }
    NSUInteger count = skus.count;
    NSMutableArray* array = [NSMutableArray arrayWithCapacity:count];
    for (NSUInteger i = 0; i < count; ++i){
        [array addObject:[self serializeNamiSKU:skus[i]]];
    }
    return array;
}

+ (NSDictionary *)serializeNamiEntitlement:(NamiEntitlement *)entitlement{
    if (entitlement == NULL){
        return NULL;
    }
    NSMutableDictionary* dictionary = [NSMutableDictionary dictionary];
    dictionary[@"activePurchases"] = [self serializeNamiPurchaseArray:[entitlement activePurchases]];
    dictionary[@"desc"] = [entitlement desc];
    dictionary[@"name"] = [entitlement name];
    dictionary[@"namiId"] = [entitlement namiId];
    dictionary[@"purchasedSkus"] = [self serializeNamiSKUArray:[entitlement purchasedSkus]];
    dictionary[@"referenceId"] = [entitlement referenceId];
    dictionary[@"relatedSkus"] = [self serializeNamiSKUArray:[entitlement relatedSkus]];
    return dictionary;
}

+ (NSMutableArray *)serializeNamiEntitlementArray:(NSArray<NamiEntitlement *> *)entitlements{
    if (entitlements == NULL){
        return NULL;
    }
    NSUInteger count = entitlements.count;
    NSMutableArray* array = [NSMutableArray arrayWithCapacity:count];
    for (NSUInteger i = 0; i < count; ++i){
        [array addObject:[self serializeNamiEntitlement:entitlements[i]]];
    }
    return array;
}

+ (NSDictionary *)serializeNamiPurchase:(NamiPurchase *)purchase{
    if (purchase == NULL){
        return NULL;
    }
    NSMutableDictionary* dictionary = [NSMutableDictionary dictionary];
    dictionary[@"purchaseInitiatedTimestamp"] = [NSNumber numberWithDouble:[[purchase purchaseInitiatedTimestamp] timeIntervalSince1970]];
    dictionary[@"expires"] = [NSNumber numberWithDouble:[[purchase expires] timeIntervalSince1970]];
    dictionary[@"skuId"] = [purchase skuId];
    dictionary[@"transactionIdentifier"] = [purchase transactionIdentifier];
    dictionary[@"sku"] = [self serializeNamiSKU:[purchase sku]];
    // dictionary[@"entitlementsGranted"] = [self serializeNamiEntitlementArray:[purchase entitlementsGranted]]; TODO
    dictionary[@"transaction"] = [purchase transactionIdentifier];
    return dictionary;
}

+ (NSMutableArray *)serializeNamiPurchaseArray:(NSArray<NamiPurchase *> *)purchases{
    if (purchases == NULL){
        return NULL;
    }
    NSUInteger count = purchases.count;
    NSMutableArray* array = [NSMutableArray arrayWithCapacity:count];
    for (NSUInteger i = 0; i < count; ++i){
        [array addObject:[self serializeNamiPurchase:purchases[i]]];
    }
    return array;
}

+ (NSDictionary *)serializeNamiCampaign:(NamiCampaign *)campaign{
    if (campaign == NULL){
        return NULL;
    }
    NSMutableDictionary* dictionary = [NSMutableDictionary dictionary];
    dictionary[@"id"] = [campaign id];
    dictionary[@"rule"] = [campaign rule];
    dictionary[@"paywall"] = [campaign paywall];
    dictionary[@"segment"] = [campaign segment];
    dictionary[@"value"] = [campaign value];
    return dictionary;
}

+ (NSMutableArray *)serializeNamiCampaignArray:(NSArray<NamiCampaign *> *)campaigns{
    if (campaigns == NULL){
        return NULL;
    }
    NSUInteger count = campaigns.count;
    NSMutableArray* array = [NSMutableArray arrayWithCapacity:count];
    for (NSUInteger i = 0; i < count; ++i){
        [array addObject:[self serializeNamiCampaign:campaigns[i]]];
    }
    return array;
}

+ (NSDictionary *)serializeCustomerJourneyState:(CustomerJourneyState *)journetState{
    if (journetState == NULL){
        return NULL;
    }
    NSMutableDictionary* dictionary = [NSMutableDictionary dictionary];
    dictionary[@"formerSubscriber"] = @([journetState formerSubscriber]);
    dictionary[@"inGracePeriod"] = @([journetState inGracePeriod]);
    dictionary[@"inTrialPeriod"] = @([journetState inTrialPeriod]);
    dictionary[@"inIntroOfferPeriod"] = @([journetState inIntroOfferPeriod]);
    dictionary[@"isCancelled"] = @([journetState isCancelled]);
    dictionary[@"inPause"] = @([journetState inPause]);
    dictionary[@"inAccountHold"] = @([journetState inAccountHold]);
    return dictionary;
}

@end
