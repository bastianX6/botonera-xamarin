using System;
using Foundation;
using UserNotifications;

namespace botonera.iOS.Delegates
{
    public class UserNotificationCenterDelegate: UNUserNotificationCenterDelegate
    {
        public UserNotificationCenterDelegate()
        {
        }

        [Export("userNotificationCenter:willPresentNotification:withCompletionHandler:")]
        public override void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            completionHandler(UNNotificationPresentationOptions.Alert | UNNotificationPresentationOptions.Sound);
        }
    }
}
