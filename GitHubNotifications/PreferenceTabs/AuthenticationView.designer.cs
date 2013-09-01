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
	[Register ("AuthenticationViewController")]
	partial class AuthenticationViewController
	{
		[Outlet]
		MonoMac.AppKit.NSTextField PatTextField { get; set; }

		[Action ("GotoGitHub:")]
		partial void GotoGitHub (MonoMac.Foundation.NSObject sender);

		[Action ("PatEndEdit:")]
		partial void PatEndEdit (MonoMac.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (PatTextField != null) {
				PatTextField.Dispose ();
				PatTextField = null;
			}
		}
	}

	[Register ("AuthenticationView")]
	partial class AuthenticationView
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}
