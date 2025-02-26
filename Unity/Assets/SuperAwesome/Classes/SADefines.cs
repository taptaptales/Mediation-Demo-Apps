using UnityEngine;
using System.Collections;

namespace tv {
  namespace superawesome {
    namespace sdk {
      namespace publisher {

        [System.Serializable]
        public class GetIsMinorModel {
          public string country;
          public int consentAgeForCountry;
          public int age;
          public bool isMinor;

          public GetIsMinorModel(string country, int consentAgeForCountry, int age, bool isMinor) {
            this.country = country;
            this.consentAgeForCountry = consentAgeForCountry;
            this.age = age;
            this.isMinor = isMinor;
          }
        }

        public enum SAOrientation {
            ANY = 0,
            PORTRAIT = 1,
            LANDSCAPE = 2
        }

        public enum SAConfiguration {
            PRODUCTION = 0,
            STAGING = 1
        }

        public enum SAEvent {
            adLoaded = 0,
            adEmpty = 1,
            adFailedToLoad = 2,
            adAlreadyLoaded = 3,
            adShown = 4,
            adFailedToShow = 5,
            adClicked = 6,
            adEnded = 7,
            adClosed = 8,
            adPaused = 9,
            adPlaying = 10
        }

        public enum SACloseButtonState {
            VISIBLEWITHDELAY = 0,
            VISIBLEIMMEDIATELY = 1,
            HIDDEN = 2,
            CUSTOM = 3
        }

        // enums for different banner specific properties
        public enum SABannerPosition {
            TOP = 0,
            BOTTOM = 1
        }

        public class SADefines {

          // default state vars
          public static int defaultPlacementId() {
            return 0;
          }

          public static bool defaultTestMode() {
            return false;
          }

          public static bool defaultParentalGate() {
            return false;
          }

          public static bool defaultBumperPage() {
            return false;
          }

          public static SAConfiguration defaultConfiguration() {
            return SAConfiguration.PRODUCTION;
          }

          public static SAOrientation defaultOrientation() {
            return SAOrientation.ANY;
          }

          public static SACloseButtonState defaultCloseButton() {
            return SACloseButtonState.HIDDEN;
          }
          
          public static SACloseButtonState defaultCloseButtonInterstitial() {
            return SACloseButtonState.VISIBLEWITHDELAY;
          }

          public static bool defaultSmallClick() {
            return false;
          }

          public static bool defaultCloseAtEnd() {
            return true;
          }

          public static bool defaultBgColor() {
            return false;
          }

          public static bool defaultBackButton() {
            return false;
          }
          
          public static bool defaultMuteOnStart() {
            return false;
          }

          public static bool defaultCloseWarning() {
            return false;
          }

          public static SABannerPosition defaultBannerPosition() {
            return SABannerPosition.BOTTOM;
          }

          public static int defaultBannerWidth() {
            return 320;
          }

          public static int defaultBannerHeight() {
            return 50;
          }

          public static double defaultCustomCloseButtonDelay() {
            return 1.0;
          }
        }
      }
    }
  }
}