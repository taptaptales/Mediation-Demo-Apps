/**
 * @Copyright:   SuperAwesome Trading Limited 2017
 * @Author:      Gabriel Coman (gabriel.coman@superawesome.tv)
 */

#import <UIKit/UIKit.h>

static inline NSString* jsonData(NSDictionary *data) {
    if ([NSJSONSerialization isValidJSONObject:data]) {
        NSData *json = [NSJSONSerialization dataWithJSONObject:data options:kNilOptions error:nil];
        return [[NSString alloc] initWithData:json encoding:NSUTF8StringEncoding];
    }
    return nil;
}

/**
 * Generic method used to send messages back to unity
 *
 * @param unityName the name of the unity ad to send the message back to
 * @param data      a dictionary of data to send back
 */
static inline void sendToUnity (NSString *unityName, NSDictionary *data) {

    const char *name = [unityName UTF8String];
    NSString *payload = jsonData(data);
    const char *payloadUTF8 = [payload UTF8String];
    UnitySendMessage (name, "nativeCallback", payloadUTF8);

}

/**
 * Method that sends back ad data to Unity
 *
 * @param unityName     the name of the unity ad to send the message back to
 * @param placementId   placement id of the ad that called this
 * @param callback      callback method
 */
static inline void unitySendAdCallback (NSString *unityName, NSInteger placementId, NSString *callback) {

    NSDictionary *data = @{
        @"placementId": [NSString stringWithFormat:@"%ld", (long) placementId],
        @"type": [NSString stringWithFormat:@"sacallback_%@", callback]
    };

    sendToUnity(unityName, data);

}
