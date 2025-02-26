using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using MiniJSON;
using System.Runtime.InteropServices;
using System;

namespace tv {
  namespace superawesome {
    namespace sdk {
      namespace publisher {

        public class SAVideoAd: MonoBehaviour {
          #if(UNITY_IPHONE && !UNITY_EDITOR)

            [DllImport("__Internal")]
            private static extern void SuperAwesomeUnitySAVideoAdCreate();

            [DllImport("__Internal")]
            private static extern void SuperAwesomeUnitySAVideoAdLoad(
              int placementId, 
              int configuration, 
              bool test, 
              int playback,
              string openRtbPartnerId,
              string encodedOptions
            );

            [DllImport("__Internal")]
            private static extern void SuperAwesomeUnitySAVideoAdPlay(
              int placementId
            );

            [DllImport("__Internal")]
            private static extern void SuperAwesomeUnitySAVideoAdApplySettings(
              bool isParentalGateEnabled, 
              bool isBumperPageEnabled, 
              int closeButtonState, 
              double closeButtonDelay,
              bool shouldShowSmallClickButton, 
              bool shouldAutomaticallyCloseAtEnd, 
              int lockOrientation, 
              bool shouldShowCloseWarning,
              bool isTestingEnabled,
              bool muteOnStartEnabled
            );

            [DllImport("__Internal")]
            private static extern bool SuperAwesomeUnitySAVideoAdHasAdAvailable(int placementId);
          #endif

          // the video ad static instance
          private static SAVideoAd staticInstance = null;

          // define a default callback so that it's never null and I don't have
          // to do a check every time I want to call it
          private static Action <int, SAEvent> callback = (p, e) => {};

          // assign default values to all of these fields
          private static bool isParentalGateEnabled = SADefines.defaultParentalGate();
          private static bool isBumperPageEnabled = SADefines.defaultBumperPage();
          private static bool shouldShowCloseWarning = SADefines.defaultCloseWarning();
          private static bool shouldShowSmallClickButton = SADefines.defaultSmallClick();
          private static bool shouldAutomaticallyCloseAtEnd = SADefines.defaultCloseAtEnd();
          private static bool isTestingEnabled = SADefines.defaultTestMode();
          private static bool isBackButtonEnabled = SADefines.defaultBackButton();
          private static SAOrientation orientation = SADefines.defaultOrientation();
          private static SAConfiguration configuration = SADefines.defaultConfiguration();
          private static SACloseButtonState closeButtonState = SADefines.defaultCloseButton();
          private static bool muteOnStartEnabled = SADefines.defaultMuteOnStart();
          private static double customCloseButtonDelay = SADefines.defaultCustomCloseButtonDelay();

          // instance constructor
          private static void createInstance() {
            // create just one static instance for ever!
            if (staticInstance == null) {
              GameObject obj = new GameObject();
              staticInstance = obj.AddComponent <SAVideoAd> ();
              staticInstance.name = "SAVideoAd";
              DontDestroyOnLoad(staticInstance);

              //
              // set native version
              SAVersion.setVersionInNative();

              #if(UNITY_IPHONE && !UNITY_EDITOR)
                SAVideoAd.SuperAwesomeUnitySAVideoAdCreate();
              #elif(UNITY_ANDROID && !UNITY_EDITOR)

                var unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                var context = unityClass.GetStatic <AndroidJavaObject> ("currentActivity");

                context.Call("runOnUiThread", new AndroidJavaRunnable(() => {
                  var saplugin = new AndroidJavaClass("tv.superawesome.plugins.publisher.unity.SAUnityVideoAd");
                  saplugin.CallStatic("SuperAwesomeUnitySAVideoAdCreate");
                }));

              #else
                Debug.Log("SAVideoAd Create");
              #endif
            }
          }

          public static void load(int placementId, string openRtbPartnerId = default, Dictionary <string, object> options = default) {
            // create an instrance of an SAVideoAd (for callbacks)
            createInstance();

            string encodedOptions = "";

            Dictionary <string, object> optionsDict = options ?? new Dictionary <string, object> ();

            try {
              encodedOptions = Json.Serialize(optionsDict);
            } catch {
              Debug.Log("Unable to create encodedOptions");
            }

            #if (UNITY_IPHONE && !UNITY_EDITOR)

              SAVideoAd.SuperAwesomeUnitySAVideoAdLoad(
                placementId,
                (int)configuration,
                isTestingEnabled,
                0,
                openRtbPartnerId,
                encodedOptions
              );

            #elif (UNITY_ANDROID && !UNITY_EDITOR)

              var unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
              var context = unityClass.GetStatic <AndroidJavaObject> ("currentActivity");

              context.Call("runOnUiThread", new AndroidJavaRunnable(() => {
                var saplugin = new AndroidJavaClass("tv.superawesome.plugins.publisher.unity.SAUnityVideoAd");
                saplugin.CallStatic("SuperAwesomeUnitySAVideoAdLoad",
                  context,
                  placementId,
                  (int)configuration,
                  isTestingEnabled,
                  0,
                  openRtbPartnerId,
                  encodedOptions
                );
              }));
            #else
              Debug.Log("SAVideoAd Load");
            #endif
          }

          public static void play(int placementId) {
            // create an instance of an SAVideoAd (for callbacks)
            applySettings();

            #if(UNITY_IPHONE && !UNITY_EDITOR)
              SAVideoAd.SuperAwesomeUnitySAVideoAdPlay(
                placementId
              );
            #elif(UNITY_ANDROID && !UNITY_EDITOR)

              var unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
              var context = unityClass.GetStatic <AndroidJavaObject> ("currentActivity");

              context.Call("runOnUiThread", new AndroidJavaRunnable(() => {
                var saplugin = new AndroidJavaClass("tv.superawesome.plugins.publisher.unity.SAUnityVideoAd");
                saplugin.CallStatic("SuperAwesomeUnitySAVideoAdPlay",
                  context,
                  placementId);
              }));

            #else
              Debug.Log("SAVideoAd Play");
            #endif
          }

          private static void applySettings() {
            // create an instrance of an SAVideoAd (for callbacks)
            createInstance();

            #if(UNITY_IPHONE && !UNITY_EDITOR)
              SAVideoAd.SuperAwesomeUnitySAVideoAdApplySettings(
                isParentalGateEnabled, 
                isBumperPageEnabled, 
                (int)closeButtonState, 
                customCloseButtonDelay,
                shouldShowSmallClickButton, 
                shouldAutomaticallyCloseAtEnd, 
                (int)orientation, 
                shouldShowCloseWarning,
                isTestingEnabled,
                muteOnStartEnabled
              );
            #elif(UNITY_ANDROID && !UNITY_EDITOR)

              var unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
              var context = unityClass.GetStatic <AndroidJavaObject> ("currentActivity");

              context.Call("runOnUiThread", new AndroidJavaRunnable(() => {
                var saplugin = new AndroidJavaClass("tv.superawesome.plugins.publisher.unity.SAUnityVideoAd");
                saplugin.CallStatic("SuperAwesomeUnitySAVideoAdApplySettings",
                  isParentalGateEnabled,
                  isBumperPageEnabled,
                  (int)closeButtonState,
                  customCloseButtonDelay,
                  shouldShowSmallClickButton,
                  shouldAutomaticallyCloseAtEnd,
                  (int)orientation,
                  isBackButtonEnabled,
                  shouldShowCloseWarning,
                  isTestingEnabled,
                  muteOnStartEnabled);
              }));

            #else
              Debug.Log("SAVideoAd applySettings");
            #endif
          }

          public static bool hasAdAvailable(int placementId) {
            // create an instrance of an SAVideoAd (for callbacks)
            createInstance();

            #if(UNITY_IPHONE && !UNITY_EDITOR)
              return SAVideoAd.SuperAwesomeUnitySAVideoAdHasAdAvailable(placementId);
            #elif(UNITY_ANDROID && !UNITY_EDITOR)

              var unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
              var context = unityClass.GetStatic <AndroidJavaObject> ("currentActivity");
              var saplugin = new AndroidJavaClass("tv.superawesome.plugins.publisher.unity.SAUnityVideoAd");

              return saplugin.CallStatic <bool> ("SuperAwesomeUnitySAVideoAdHasAdAvailable", placementId);
            #else
              Debug.Log("SAVideoAd hasAdAvailable has not implemented");
              return false;
            #endif
          }

          ////////////////////////////////////////////////////////////////////
          // Setters & getters
          ////////////////////////////////////////////////////////////////////

          public static void setCallback(Action <int, SAEvent> value) {
            callback = value != null ? value : callback;
          }

          public static void setIsParentalGateEnabled(bool value) {
            isParentalGateEnabled = value;
          }

          public static void enableParentalGate() {
            isParentalGateEnabled = true;
          }

          public static void disableParentalGate() {
            isParentalGateEnabled = false;
          }

          public static void enableBumperPage() {
            isBumperPageEnabled = true;
          }

          public static void disableBumperPage() {
            isBumperPageEnabled = false;
          }

          public static void enableTestMode() {
            isTestingEnabled = true;
          }

          public static void disableTestMode() {
            isTestingEnabled = false;
          }

          public static void setConfigurationProduction() {
            configuration = SAConfiguration.PRODUCTION;
          }

          public static void setConfigurationStaging() {
            configuration = SAConfiguration.STAGING;
          }

          public static void setOrientationAny() {
            orientation = SAOrientation.ANY;
          }

          public static void setOrientationPortrait() {
            orientation = SAOrientation.PORTRAIT;
          }

          public static void setOrientationLandscape() {
            orientation = SAOrientation.LANDSCAPE;
          }

          public static void enableCloseButton() {
            closeButtonState = SACloseButtonState.VISIBLEWITHDELAY;
          }

          // enable the close button after a delay where the delay is in seconds
          public static void enableCloseButtonWithDelay(double delay) {
            closeButtonState = SACloseButtonState.CUSTOM;
            customCloseButtonDelay = delay;
          }

          public static void enableCloseButtonNoDelay() {
            closeButtonState = SACloseButtonState.VISIBLEIMMEDIATELY;
          }

          public static void disableCloseButton() {
            closeButtonState = SACloseButtonState.HIDDEN;
          }

          public static void enableSmallClickButton() {
            shouldShowSmallClickButton = true;
          }

          public static void disableSmallClickButton() {
            shouldShowSmallClickButton = false;
          }

          public static void enableCloseAtEnd() {
            shouldAutomaticallyCloseAtEnd = true;
          }

          public static void disableCloseAtEnd() {
            shouldAutomaticallyCloseAtEnd = false;
          }
          
          public static void enableMuteOnStart() {
            muteOnStartEnabled = true;
          }

          public static void disableMuteOnStart() {
            muteOnStartEnabled = false;
          }

          public static void enableBackButton() {
            isBackButtonEnabled = true;
          }

          public static void enableLeaveWarning() {
            shouldShowCloseWarning = true;
          }

          public static void disableLeaveWarning() {
            shouldShowCloseWarning = false;
          }

          public static void enableCloseButtonWithWarning() {
            closeButtonState = SACloseButtonState.VISIBLEWITHDELAY;
            shouldShowCloseWarning = true;
          }

          public static void disableBackButton() {
            isBackButtonEnabled = false;
          }

          ////////////////////////////////////////////////////////////////////
          // Native callbacks
          ////////////////////////////////////////////////////////////////////

          public void nativeCallback(string payload) {
            Dictionary <string, object> payloadDict;
            string type = "";
            int placementId = 0;

            // try to get payload and type data
            try {
              payloadDict = Json.Deserialize(payload) as Dictionary <string, object>;
              type = (string) payloadDict["type"];
              string plac = (string) payloadDict["placementId"];
              int.TryParse(plac, out placementId);
            } catch {
              Debug.Log("Error w/ callback!");
              return;
            }

            switch (type) {
            case "sacallback_adLoaded":
              callback(placementId, SAEvent.adLoaded);
              break;
            case "sacallback_adEmpty":
              callback(placementId, SAEvent.adEmpty);
              break;
            case "sacallback_adFailedToLoad":
              callback(placementId, SAEvent.adFailedToLoad);
              break;
            case "sacallback_adAlreadyLoaded":
              callback(placementId, SAEvent.adAlreadyLoaded);
              break;
            case "sacallback_adShown":
              callback(placementId, SAEvent.adShown);
              break;
            case "sacallback_adFailedToShow":
              callback(placementId, SAEvent.adFailedToShow);
              break;
            case "sacallback_adClicked":
              callback(placementId, SAEvent.adClicked);
              break;
            case "sacallback_adEnded":
              callback(placementId, SAEvent.adEnded);
              break;
            case "sacallback_adClosed":
              callback(placementId, SAEvent.adClosed);
              break;
            case "sacallback_adPaused":
              callback(placementId, SAEvent.adPaused);
              break;
            case "sacallback_adPlaying":
              callback(placementId, SAEvent.adPlaying);
              break;
            }
          }
        }
      }
    }
  }
}