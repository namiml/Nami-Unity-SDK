#import <Foundation/Foundation.h>
#import "NamiApple/NamiApple-Swift.h"

@interface NamiJsonUtils : NSObject

+ (NSString *)serializeArray:(NSMutableArray *)array;
+ (NSString *)serializeDictionary:(NSDictionary *)dictionary;

+ (NSArray *)deserializeArray:(NSString *)jsonArray;
+ (NSDictionary *)deserializeDictionary:(NSString *)jsonDic;
+ (NSArray<NSNumber *> *)deserializeNumbersArray:(NSArray *)numbers;

@end
