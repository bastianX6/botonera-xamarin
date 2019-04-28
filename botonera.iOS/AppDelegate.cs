using System;
using System.Collections.Generic;
using System.Linq;
using botonera.iOS.Delegates;
using Firebase.CloudMessaging;
using Foundation;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using UIKit;
using UserNotifications;

namespace botonera.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IMessagingDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            InitAppCenter();
            InitFirebase();
            SetupNotifications();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        private void InitAppCenter()
        {
            AppCenter.Start("f70377f4-7c1e-4bee-98af-5c6db9d27bbe", typeof(Analytics), typeof(Crashes));
        }

        private void InitFirebase()
        {
            Firebase.Core.App.Configure();
        }

        private void SetupNotifications()
        {
            var authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;
            UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (granted, error) => {
                System.Diagnostics.Debug.WriteLine($"Push notifications permissions granted: {granted}");
                System.Diagnostics.Debug.WriteLine($"Push notifications permissions error: {error}");
            });

            // For iOS 10 display notification (sent via APNS)
            UNUserNotificationCenter.Current.Delegate = new UserNotificationCenterDelegate();

            // For iOS 10 data message (sent via FCM)
            Messaging.SharedInstance.Delegate = this;

            UIApplication.SharedApplication.RegisterForRemoteNotifications();
        }

        [Export("application:didRegisterForRemoteNotificationsWithDeviceToken:")]
        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            Messaging.SharedInstance.ApnsToken = deviceToken;
        }

        [Export("messaging:didReceiveRegistrationToken:")]
        public void DidReceiveRegistrationToken(Messaging messaging, string fcmToken)
        {
            System.Diagnostics.Debug.WriteLine($"Firebase registration token: {fcmToken}");
        }
    }
}
