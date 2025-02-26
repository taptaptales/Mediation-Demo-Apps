/**
 * @Copyright:   SuperAwesome Trading Limited 2017
 * @Author:      Gabriel Coman (gabriel.coman@superawesome.tv)
 */

#import "SAUnityCallback.h"
#import <AVFoundation/AVFoundation.h>
#import <WebKit/WebKit.h>
#import <SuperAwesome/SuperAwesome-Swift.h>

/**
 * Native method called from Unity.
 * Method that adds a callback to the SAInterstitialAd static method class
 */
void SuperAwesomeUnitySAInterstitialAdCreate () {
    [SAInterstitialAd setCallback:^(NSInteger placementId, SAEvent event) {
        switch (event) {
            case SAEventAdLoaded: unitySendAdCallback(@"SAInterstitialAd", placementId, @"adLoaded"); break;
            case SAEventAdEmpty: unitySendAdCallback(@"SAInterstitialAd", placementId, @"adEmpty"); break;
            case SAEventAdFailedToLoad: unitySendAdCallback(@"SAInterstitialAd", placementId, @"adFailedToLoad"); break;
            case SAEventAdAlreadyLoaded: unitySendAdCallback(@"SAInterstitialAd", placementId, @"adAlreadyLoaded"); break;
            case SAEventAdShown: unitySendAdCallback(@"SAInterstitialAd", placementId, @"adShown"); break;
            case SAEventAdFailedToShow: unitySendAdCallback(@"SAInterstitialAd", placementId, @"adFailedToShow"); break;
            case SAEventAdClicked: unitySendAdCallback(@"SAInterstitialAd", placementId, @"adClicked"); break;
            case SAEventAdEnded: unitySendAdCallback(@"SAInterstitialAd", placementId, @"adEnded"); break;
            case SAEventAdClosed: unitySendAdCallback(@"SAInterstitialAd", placementId, @"adClosed"); break;
            case SAEventAdPaused: break;
            case SAEventAdPlaying: break;
            case SAEventWebSDKReady: break;
        }
    }];
}

/**
 * Native method called from Unity.
 * Load an interstitial ad
 *
 * @param placementId   the placement id to try to load an ad for
 * @param configuration production = 0 / staging = 1
 * @param test          true / false
 * @param encodedOptions a json encoded dictionary of options to send with requests
 * @param openRtbPartnerId an optional partner id required for making OpenRTB requests
 */
void SuperAwesomeUnitySAInterstitialAdLoad (int placementId, int configuration, bool test, const char *openRtbPartnerId, const char *encodedOptions) {

    [SAInterstitialAd setTestMode:test];
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
            [SAInterstitialAd load: placementId openRtbPartnerId: openRtbPartnerIdString];
        } else {
            [SAInterstitialAd load: placementId openRtbPartnerId: openRtbPartnerIdString options: optionsData];
        }

    } else {
        [SAInterstitialAd load: placementId openRtbPartnerId: openRtbPartnerIdString];
    }
}

/**
 * Native method called from Unity.
 * Check to see if there's an ad available
 *
 * @return true / false
 */
bool SuperAwesomeUnitySAInterstitialAdHasAdAvailable(int placementId) {
    return [SAInterstitialAd hasAdAvailable: placementId];
}

void SuperAwesomeUnitySAInterstitialAdSetCloseButtonState(int closeButtonState, double closeButtonDelay) {
    switch(closeButtonState) {
        case 0: //CloseButtonStateVisibleWithDelay
            [SAInterstitialAd enableCloseButton];
            break;
        case 1: //CloseButtonStateVisibleImmediately
            [SAInterstitialAd enableCloseButtonNoDelay];
            break;
        case 2: //CloseButtonStateHidden
            // Do nothing as Interstitial does not support hidden close button
            break;
        case 3: //CloseButtonStateCustom
            [SAInterstitialAd enableCloseButtonTimeIntervalWithCustomDelay: closeButtonDelay];
            break;
    }
}

/**
 * Native method called from Unity.
 * Play an interstitial ad
 *
 * @param placementId   the placement id to try to load an ad for
 */
void SuperAwesomeUnitySAInterstitialAdPlay (int placementId) {
    UIViewController *root = [UIApplication sharedApplication].keyWindow.rootViewController;
    [SAInterstitialAd play: placementId fromVC: root];
}

/**
 * Native method called from Unity.
 * Apply Settings
 *
 * @param isParentalGateEnabled true / false
 * @param isBumperPageEnabled   true / false
 * @param orientation           ANY = 0 / PORTRAIT = 1 / LANDSCAPE = 2
 * @param isTestingEnabled  true / false
 * @param closeButtonState CloseButtonStateVisibleWithDelay = 0 / CloseButtonStateVisibleImmediately = 1 / CloseButtonStateHidden = 2  / CloseButtonStateCustom = 3
 */
void SuperAwesomeUnitySAInterstitialAdApplySettings (bool isParentalGateEnabled,
                                                     bool isBumperPageEnabled,
                                                     int orientation,
                                                     bool isTestingEnabled,
                                                     int closeButtonState,
                                                     double closeButtonDelay) {
    [SAInterstitialAd setParentalGate:isParentalGateEnabled];
    [SAInterstitialAd setBumperPage:isBumperPageEnabled];
    [SAInterstitialAd setOrientation:[OrientationHelper from: orientation]];
    [SAInterstitialAd setTestMode:isTestingEnabled];
    SuperAwesomeUnitySAInterstitialAdSetCloseButtonState(closeButtonState, closeButtonDelay);
}
