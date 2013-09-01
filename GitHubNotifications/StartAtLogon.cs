using System;
using MonoMac.ObjCRuntime;
using System.Runtime.InteropServices;
using MonoMac.Foundation;

namespace GitHubNotifications
{
	public static class StartAtLogon
	{
		static StartAtLogon ()
		{
		}

		public static void SetLogin()
		{

		}

	}
	//http://stackoverflow.com/questions/17220540/how-to-make-xamarin-mac-app-open-at-login
	/*public static class LSSharedFileList
	{
		//needed library
		const string DllName = "/System/Library/Frameworks/ApplicationServices.framework/ApplicationServices";
		static IntPtr dllHandle;
		static IntPtr kLSSharedFileListSessionLoginItems;
		static IntPtr kLSSharedFileListItemLast;

		static LSSharedFileList ()
		{
			dllHandle = Dlfcn.dlopen (DllName, 0);

			kLSSharedFileListSessionLoginItems = *Dlfcn.GetStringConstant (dllHandle, "kLSSharedFileListSessionLoginItems");
			kLSSharedFileListItemLast = *Dlfcn.GetStringConstant (dllHandle, "kLSSharedFileListItemLast");
		}

		public static void EnableLogInItem ()
		{
			IntPtr pFileList = IntPtr.Zero;
			IntPtr pItem = IntPtr.Zero;
			try
			{
				pFileList = LSSharedFileListCreate (
					IntPtr.Zero,
					kLSSharedFileListSessionLoginItems,
					IntPtr.Zero);

				pItem = LSSharedFileListInsertItemURL (
					pFileList,
					kLSSharedFileListItemLast,
					IntPtr.Zero,
					IntPtr.Zero,
					NSBundle.MainBundle.BundleUrl.Handle,
					IntPtr.Zero,
					IntPtr.Zero);

			}
			finally
			{
				CFRelease (pItem);
				CFRelease (pFileList);
			}
		}


		[DllImport(DllName)]
		public static extern IntPtr LSSharedFileListCreate (
			IntPtr inAllocator,
			IntPtr inListType,
			IntPtr listOptions);

		[DllImport(DllName, CharSet=CharSet.Unicode)]
		public static extern void CFRelease (
			IntPtr cf
			);


		[DllImport(DllName)]
		public extern static IntPtr LSSharedFileListInsertItemURL (
			IntPtr inList,
			IntPtr insertAfterThisItem,
			IntPtr inDisplayName,
			IntPtr inIconRef,
			IntPtr inURL,
			IntPtr inPropertiesToSet,
			IntPtr inPropertiesToClear);
	}*/

}

