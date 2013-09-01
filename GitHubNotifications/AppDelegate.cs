using System;
using System.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;

namespace GitHubNotifications
{
	public partial class AppDelegate : NSApplicationDelegate
	{
		//MainWindowController mainWindowController;
		NSStatusItem _statusItem;
		PreferencesWindowController _preferencesWindowController;

		public AppDelegate ()
		{
		}

		public override void FinishedLaunching (NSObject notification)
		{
			//mainWindowController = new MainWindowController ();
			//mainWindowController.Window.MakeKeyAndOrderFront (this);
		}

		public override void AwakeFromNib()
		{
			SetStatusItem ();
			//TODO Check if it should be running at login, if so make it so
		}

		private void SetStatusItem()
		{
			if(_statusItem == null)
				_statusItem = NSStatusBar.SystemStatusBar.CreateStatusItem(30);
			_statusItem.Menu = statusMenu;
			_statusItem.Image = NSImage.FromStream(System.IO.File.OpenRead(NSBundle.MainBundle.ResourcePath + @"/icon.icns"));
			_statusItem.HighlightMode = true;
		}

		partial void quitClick (MonoMac.Foundation.NSObject sender)
		{
			NSApplication.SharedApplication.Terminate(this);
		}

		partial void checkNowClick (MonoMac.Foundation.NSObject sender)
		{
			//TODO breakout getting notifications and linking it all up into a function
			//TODO use settings for token

			Notifications.DoNotificationCheck();
		}

		private void sendNotification(string title, string text, string url)
		{
			// First we create our notification and customize as needed
			NSUserNotification not = new NSUserNotification();
			not.Title = title;
			not.InformativeText = text;
			not.DeliveryDate = DateTime.Now;
			not.SoundName = NSUserNotification.NSUserNotificationDefaultSoundName;

			// We get the Default notification Center
			NSUserNotificationCenter center = NSUserNotificationCenter.DefaultUserNotificationCenter;

			/*center.DidDeliverNotification += (s, e) => 
			{
				Console.WriteLine("Notification Delivered");
				DeliveredColorWell.Color = NSColor.Green;
			};*/

			center.DidActivateNotification += (s, e) => 
			{
				// This removes the notification from the notification center
				center.RemoveDeliveredNotification(e.Notification);
				Console.WriteLine("Opening " + url);
				// This opens a website URL
				NSWorkspace.SharedWorkspace.OpenUrl(new NSUrl(url));
			};

			// If we return true here, Notification will show up even if your app is TopMost.
			center.ShouldPresentNotification = (c, n) => { return true; };

			center.ScheduleNotification(not);
		}

		public override NSApplicationTerminateReply ApplicationShouldTerminate(NSApplication sender)
		{
			// Clean up all of the panels, and disposable resources
			if (_statusItem != null)
			{
				_statusItem.Dispose();
				_statusItem = null;
			}

			return NSApplicationTerminateReply.Now;
		}

		partial void ShowPreferenceWindow (MonoMac.Foundation.NSObject sender)
		{
			Console.WriteLine("I can has perfences?");
			if(_preferencesWindowController == null)
				_preferencesWindowController = new PreferencesWindowController();

			//NSApplication.SharedApplication.RunModalForWindow(_preferencesWindowController.Window);
			_preferencesWindowController.ShowWindow(this);
			_preferencesWindowController.Window.Level = NSWindowLevel.Status;
			//_preferencesWindowController.Window.MakeMainWindow();
		}
	}
}

