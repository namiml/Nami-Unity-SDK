#import <Foundation/Foundation.h>

@interface NamiUtils : NSObject

// Converts C style string to NSString
+ (NSString *)createNSStringFrom:(const char *)cstring;

// Conver NSString to C style string
+ (char *)createCStringFrom:(NSString *)string;

@end
