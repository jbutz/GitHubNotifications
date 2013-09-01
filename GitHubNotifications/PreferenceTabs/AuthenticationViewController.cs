using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;

namespace GitHubNotifications
{
	public partial class AuthenticationViewController : NSViewController, IPreferencesTab
	{
		#region Constructors
		// Called when created from unmanaged code
		public AuthenticationViewController (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public AuthenticationViewController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		// Call to load from the XIB/NIB file
		public AuthenticationViewController () : base ("AuthenticationView", NSBundle.MainBundle)
		{
			Initialize ();
		}
		// Shared initialization code
		void Initialize ()
		{
		}
		#endregion


		#region IPreferencesTab implementation

		public string Name {
			get {
				return "Authentication";
			}
		}

		public NSImage Icon {
			get {
				return NSImage.ImageNamed("NSUser");
			}
		}

		#endregion

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();

			string pat = SettingsStore.GetPat ();
			PatTextField.StringValue = pat;
		}

		// Save the Personal Access Token entered
		partial void PatEndEdit (MonoMac.Foundation.NSObject sender)
		{
			var field = (NSTextField) sender;
			SettingsStore.SetPat(field.StringValue.ToString());
			Notifications.UpdateApiToken();
		}

		// Open GitHub
		partial void GotoGitHub (MonoMac.Foundation.NSObject sender)
		{
			NSWorkspace.SharedWorkspace.OpenUrl(new NSUrl("https://github.com/settings/applications"));
		}
	}
}

