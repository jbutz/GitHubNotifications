// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoMac.Foundation;
using System.CodeDom.Compiler;

namespace GitHubNotifications
{
	[Register ("AppDelegate")]
	partial class AppDelegate
	{
		[Outlet]
		MonoMac.AppKit.NSMenu statusMenu { get; set; }

		[Action ("checkNowClick:")]
		partial void checkNowClick (MonoMac.Foundation.NSObject sender);

		[Action ("quitClick:")]
		partial void quitClick (MonoMac.Foundation.NSObject sender);

		[Action ("ShowPreferenceWindow:")]
		partial void ShowPreferenceWindow (MonoMac.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (statusMenu != null) {
				statusMenu.Dispose ();
				statusMenu = null;
			}
		}
	}
}
