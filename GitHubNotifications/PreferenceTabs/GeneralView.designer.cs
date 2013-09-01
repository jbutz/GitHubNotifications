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
	[Register ("GeneralViewController")]
	partial class GeneralViewController
	{
		[Outlet]
		MonoMac.AppKit.NSButton StartAtLoginCheckbox { get; set; }

		[Action ("StartAtLoginChange:")]
		partial void StartAtLoginChange (MonoMac.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (StartAtLoginCheckbox != null) {
				StartAtLoginCheckbox.Dispose ();
				StartAtLoginCheckbox = null;
			}
		}
	}

	[Register ("GeneralView")]
	partial class GeneralView
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}
