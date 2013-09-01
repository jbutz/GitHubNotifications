using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;

namespace GitHubNotifications
{
	public partial class GeneralViewController : NSViewController, IPreferencesTab
	{
		#region Constructors
		// Called when created from unmanaged code
		public GeneralViewController (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public GeneralViewController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		// Call to load from the XIB/NIB file
		public GeneralViewController () : base ("GeneralView", NSBundle.MainBundle)
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
				return "General";
			}
		}

		public NSImage Icon {
			get {
				return NSImage.ImageNamed("NSGeneralPreferences");
			}
		}

		#endregion
		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();

			NSCellStateValue value;

			if (SettingsStore.GetStartAtLogon ())
				value = NSCellStateValue.On;
			else
				value = NSCellStateValue.Off;

			StartAtLoginCheckbox.State = value;
		}

		partial void StartAtLoginChange (MonoMac.Foundation.NSObject sender)
		{
			SettingsStore.SetStartAtLogon(StartAtLoginCheckbox.State == NSCellStateValue.On);
		}
	}
}

