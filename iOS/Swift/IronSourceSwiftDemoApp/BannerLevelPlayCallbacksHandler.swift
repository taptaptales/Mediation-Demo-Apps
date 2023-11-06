//
//  BannerLevelPlayCallbacksHandler.swift
//  IronSourceSwiftDemoApp
//
//  Copyright © 2023 IronSource. All rights reserved.
//

import Foundation
import IronSource

class BannerLevelPlayCallbacksHandler: NSObject, LevelPlayBannerDelegate {

    /**
     Called after a banner ad has been successfully loaded
     @param adInfo The info of the ad.
     */
    func didLoad(_ bannerView: ISBannerView!, with adInfo: ISAdInfo!) {
    }
    
    /**
     Called after a banner has attempted to load an ad but failed.
     @param error The reason for the error
     */
    func didFailToLoadWithError(_ error: Error!) {
    }
    
    /**
     Called after a banner has been clicked.
     @param adInfo The info of the ad.
     */
    func didClick(with adInfo: ISAdInfo!) {
    }
    
    /**
     Called when a user was taken out of the application context.
     @param adInfo The info of the ad.
     */
    func didLeaveApplication(with adInfo: ISAdInfo!) {
    }
    
    /**
     Called when a banner presented a full screen content.
     @param adInfo The info of the ad.
     */
    func didPresentScreen(with adInfo: ISAdInfo!) {
    }
    
    /**
     Called after a full screen content has been dismissed.
     @param adInfo The info of the ad.
     */
    func didDismissScreen(with adInfo: ISAdInfo!) {
    }
}

