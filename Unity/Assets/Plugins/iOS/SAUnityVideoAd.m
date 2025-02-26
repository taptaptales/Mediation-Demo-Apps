/**
 * @Copyright:   SuperAwesome Trading Limited 2017
 * @Author:      Gabriel Coman (gabriel.coman@superawesome.tv)
 */

#import "SAUnityCallback.h"
#import <AVFoundation/AVFoundation.h>
#import <CoreMedia/CoreMedia.h>
#import <WebKit/WebKit.h>
#import <SuperAwesome/SuperAwesome-Swift.h>

/**
 * Native method called from Unity.
 * Add a callback to the SAVideoAd static class
 */
void SuperAwesomeUnitySAVideoAdCreate () {
    [SAVideoAd setCallback:^(NSInteger placementId, SAEvent event) {
        switch (event) {
            case SAEventAdLoaded: unitySendAdCallback(@"SAVideoAd", placementId, @"adLoaded"); break;
            case SAEventAdEmpty: unitySendAdCallback(@"SAVideoAd", placementId, @"adEmpty"); break;
            case SAEventAdFailedToLoad: unitySendAdCallback(@"SAVideoAd", placementId, @"adFailedToLoad"); break;
            case SAEventAdAlreadyLoaded: unitySendAdCallback(@"SAVideoAd", placementId, @"adAlreadyLoaded"); break;
            case SAEventAdShown: unitySendAdCallback(@"SAVideoAd", placementId, @"adShown"); break;
            case SAEventAdFailedToShow: unitySendAdCallback(@"SAVideoAd", placementId, @"adFailedToShow"); break;
            case SAEventAdClicked: unitySendAdCallback(@"SAVideoAd", placementId, @"adClicked"); break;
            case SAEventAdEnded: unitySendAdCallback(@"SAVideoAd", placementId, @"adEnded"); break;
            case SAEventAdClosed: unitySendAdCallback(@"SAVideoAd", placementId, @"adClosed"); break;
            case SAEventAdPaused: break;
            case SAEventAdPlaying: break;
            case SAEventWebSDKReady: break;
        }
    }];
}

/**
 * Native method called from Unity.
 * Load a video ad
 *
 * @param placementId   placement id
 * @param configuration production = 0 / staging = 1
 * @param test true / false
 * @param playback sound off = 2 / sound on = 5
 * @param encodedOptions a json encoded dictionary of options to send with requests
 * @param openRtbPartnerId an optional partner id required for making OpenRTB requests
 */

void SuperAwesomeUnitySAVideoAdLoad(int placementId, int configuration, bool test, int playback, const char *openRtbPartnerId, const char *encodedOptions) {

    [SAVideoAd setTestMode:test];
    [SAVideoAd setPlaybackMode:[StartDelayHelper from:playback]];

    NSString *openRtbPartnerIdString = nil;

    if (openRtbPartnerId != nil) {
        openRtbPartnerIdString = [NSString stringWithUTF8String:openRtbPartnerId];
    }

    if (encodedOptions) {

        NSString *options = [NSString stringWithUTF8String:encodedOptions];
        NSData *jsonData = [options dataUsingEncoding:NSUTF8StringEncoding];
        NSError *error;
        NSMutableDictionary *optionsData = [NSJSONSerialization JSONObjectWithData:jsonData options:NSJSONReadingAllowFragments error:&error];

        if (error) {
            // Fallback to loading without options
            [SAVideoAd load: placementId openRtbPartnerId: openRtbPartnerIdString];
        } else {
            [SAVideoAd load: placementId openRtbPartnerId: openRtbPartnerIdString options: optionsData];
        }

    } else {
        [SAVideoAd load: placementId openRtbPartnerId: openRtbPartnerIdString];
    }
}

/**
 * Native method called from Unity.
 * Check to see if there is an video ad available
 *
 * @return true / false
 */
bool SuperAwesomeUnitySAVideoAdHasAdAvailable(int placementId) {
    return [SAVideoAd hasAdAvailable: placementId];
}

void SuperAwesomeUnitySAVideoAdSetCloseButtonState(int closeButtonState, double closeButtonDelay) {
    switch(closeButtonState) {
        case 0: //CloseButtonStateVisibleWithDelay
            [SAVideoAd enableCloseButton];
            break;
        case 1: //CloseButtonStateVisibleImmediately
            [SAVideoAd enableCloseButtonNoDelay];
            break;
        case 2: //CloseButtonStateHidden
            [SAVideoAd disableCloseButton];
            break;
        case 3: //CloseButtonStateCustom
            [SAVideoAd enableCloseButtonTimeIntervalWithCustomDelay: closeButtonDelay];
            break;
    }
}

/**
 * Native method called from Unity.
 * Play a video ad
 *
 * @param placementId   placement id
 */
void SuperAwesomeUnitySAVideoAdPlay(int placementId) {
    UIViewController *root = [UIApplication sharedApplication].keyWindow.rootViewController;
    [SAVideoAd play: placementId fromVC: root];
}

/**
 * Native method called from Unity.
 * Apply video settings
 *
 * @param isParentalGateEnabled      true / false
 * @param isBumperPageEnabled       true / false
 * @param closeButtonState      VISIBLEWITHDELAY = 0 / VISIBLEIMMEDIATELY = 1 / HIDDEN = 2 / CUSTOM = 3
 * @param shouldShowSmallClickButton        true / false
 * @param shouldAutomaticallyCloseAtEnd     true / false
 * @param orientation       ANY = 0 / PORTRAIT = 1 / LANDSCAPE = 2
 * @param shouldShowCloseWarning     true / false
 * @param isTestingEnabled     true / false
 * @param muteOnStart true / false
 */
void SuperAwesomeUnitySAVideoAdApplySettings(bool isParentalGateEnabled,
                                             bool isBumperPageEnabled,
                                             int closeButtonState,
                                             double closeButtonDelay,
                                             bool shouldShowSmallClickButton,
                                             bool shouldAutomaticallyCloseAtEnd,
                                             int orientation,
                                             bool shouldShowCloseWarning,
                                             bool isTestingEnabled,
                                             bool muteOnStart) {

    [SAVideoAd setParentalGate:isParentalGateEnabled];
    [SAVideoAd setBumperPage:isBumperPageEnabled];
    [SAVideoAd setSmallClick:shouldShowSmallClickButton];
    [SAVideoAd setCloseAtEnd:shouldAutomaticallyCloseAtEnd];
    [SAVideoAd setOrientation:[OrientationHelper from: orientation]];
    [SAVideoAd setCloseButtonWarning:shouldShowCloseWarning];
    [SAVideoAd setTestMode: isTestingEnabled];
    SuperAwesomeUnitySAVideoAdSetCloseButtonState(closeButtonState, closeButtonDelay);
    [SAVideoAd setMuteOnStart: muteOnStart];
}
