using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.CoreData;

namespace GitHubNotifications
{
	public partial class AuthenticationView : MonoMac.AppKit.NSView
	{

		#region Constructors
		// Called when created from unmanaged code
		public AuthenticationView (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public AuthenticationView (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		// Shared initialization code
		void Initialize ()
		{
		}
		#endregion
	}
}

