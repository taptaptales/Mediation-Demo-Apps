
/**
 * @Copyright:   SuperAwesome Trading Limited 2017
 * @Author:      Gabriel Coman (gabriel.coman@superawesome.tv)
 */

#import <AVFoundation/AVFoundation.h>
#import <WebKit/WebKit.h>
#import <SuperAwesome/SuperAwesome-Swift.h>

/**
 * Unity to native iOS method that overrides the current version & sdk
 * strings so that this will get reported correctly in the dashboard.
 *
 * @param nameString pointer to an array of chars containing the version
 */
void SuperAwesomeUnityBumperOverrideName (const char *nameString) {
    NSString *name = [NSString stringWithUTF8String:nameString];
    [SABumperPage overrideName: name];
}
