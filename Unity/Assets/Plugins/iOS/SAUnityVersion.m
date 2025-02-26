/**
 * @Copyright:   SuperAwesome Trading Limited 2017
 * @Author:      Gabriel Coman (gabriel.coman@superawesome.tv)
 */

#import "SAUnityCallback.h"
#import <AVFoundation/AVFoundation.h>
#import <WebKit/WebKit.h>
#import <SuperAwesome/SuperAwesome-Swift.h>

/**
 * Unity to native iOS method that overrides the current version & sdk
 * strings so that this will get reported correctly in the dashboard.
 *
 * @param versionString pointer to an array of chars containing the version
 * @param sdkString     pointer to an array of chars containing the sdk
 */
void SuperAwesomeUnityVersionSetVersion (const char *versionString, const char *sdkString) {
    NSString *version = [NSString stringWithUTF8String:versionString];
    NSString *sdk = [NSString stringWithUTF8String:sdkString];

    [SdkInfo overrideVersion:version withPlatform: sdk];
}
