using System;
using MonoMac.Foundation;
using MonoMac.AppKit;
using System.Timers;

namespace GitHubNotifications
{
	public static class Notifications
	{
		private static GitHubApi _api;
		private static Timer _timer;
		static Notifications ()
		{
			_api = new GitHubApi("0c8114842aa3da6959e3","1e2052e4699998bad9d9fe61160e35048e103240");
			_api.SetAuthenticationToken (SettingsStore.GetPat ());
			/*_timer = NSTimer.CreateRepeatingTimer (new TimeSpan (0, 1, 0), delegate {
				DoNotificationCheck();
				Console.WriteLine("Timer Ran");
			});*/
			_timer = new Timer (60 * 1000);
			_timer.Elapsed += delegate {
				DoNotificationCheck ();
				Console.WriteLine ("Timer Ran");
			};
			_timer.Start();
		}

		public static void UpdateApiToken()
		{
			_api.SetAuthenticationToken (SettingsStore.GetPat ());
		}

		public static void DoNotificationCheck()
		{
			// Do we have any new notifications?
			var notifications = _api.GetNotifications ();

			foreach (ghNotification n in notifications)
			{
				var commentData = _api.GetCommentData (n.subject.latest_comment_url);
				PopNotificationToUrl (n.subject.title, n.subject.type, commentData.html_url, n.url);
			}
		}

		public static void PopNotificationToUrl(string title, string text, string url, string readUrl)
		{
			// First we create our notification and customize as needed
			NSUserNotification not = new NSUserNotification();
			not.Title = title;
			not.InformativeText = text;
			not.DeliveryDate = DateTime.Now;
			not.SoundName = NSUserNotification.NSUserNotificationDefaultSoundName;
			not.SetValueForKey (new NSString (url), new NSString("htmlUrl"));
			not.SetValueForKey (new NSString (readUrl), new NSString("readUrl"));

			// We get the Default notification Center
			NSUserNotificationCenter center = NSUserNotificationCenter.DefaultUserNotificationCenter;

			center.DidActivateNotification += (s, e) => 
			{
				// This removes the notification from the notification center
				center.RemoveDeliveredNotification(e.Notification);

				// This opens a website URL
				NSWorkspace.SharedWorkspace.OpenUrl(new NSUrl(e.Notification.ValueForKey(new NSString("htmlUrl")).ToString()));

				// This marks it as read
				_api.MarkThreadRead(e.Notification.ValueForKey(new NSString("readUrl")).ToString());

			};

			// If we return true here, Notification will show up even if your app is TopMost.
			center.ShouldPresentNotification = (c, n) => { return true; };

			center.ScheduleNotification(not);
		}
	}
}

