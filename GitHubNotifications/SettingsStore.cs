using System;
using System.Dynamic;
using MonoMac.CoreData;
using MonoMac.Foundation;
using MonoMac.Security;

namespace GitHubNotifications
{
	public static class SettingsStore
	{
		// Default Keys
		private const string kStartAtLogon = "StartAtLogon";
		//
		private const string _serviceName = "GitHubNotifier";
		private const string _account = "GitHub";
		private static NSUserDefaults defaults = NSUserDefaults.StandardUserDefaults;
		static SettingsStore ()
		{
			// NSUserDefaults example:
			//	 http://oss.linn.co.uk/repos/Public/Songcast/Linn/Songcast/MacOsX/App/ViewPreferences.cs
			// Keychain Example:
			//	http://dan.clarke.name/2012/08/using-the-keychain-in-monomac/
			object[] keys = new object[] {
				kStartAtLogon
			};
			object[] vals = new object[] {
				false
			};
			defaults.RegisterDefaults(NSDictionary.FromObjectsAndKeys( vals, keys));
		}

		public static bool GetStartAtLogon()
		{
			defaults.Synchronize ();
			return Convert.ToBoolean(defaults.BoolForKey(kStartAtLogon));
		}
		public static void SetStartAtLogon(bool value)
		{
			defaults.Synchronize ();
			defaults.SetBool (value, kStartAtLogon);
		}

		public static string GetPat()
		{
			SecRecord searchRecord;

			var record = FetchKeychainRecord (out searchRecord);

			if (record == null)
				return string.Empty;

			return NSString.FromData (record.ValueData, NSStringEncoding.UTF8).ToString();

		}

		public static void SetPat(string pat)
		{
			SecRecord searchRecord;
			var record = FetchKeychainRecord (out searchRecord);

			if (record == null)
			{
				record = new SecRecord(SecKind.InternetPassword)
				{
					Server = _serviceName,
					Label = _serviceName,
					Account = _account,
					ValueData = NSData.FromString(pat)
				};

				SecKeyChain.Add(record);
				return;
			}

			record.ValueData = NSData.FromString(pat);
			SecKeyChain.Update(searchRecord, record);

		}

		private static SecRecord FetchKeychainRecord(out SecRecord searchRecord)
		{
			searchRecord = new SecRecord(SecKind.InternetPassword)
			{
				Service = _serviceName,
				Account = _account
			};

			SecStatusCode code;
			var data = SecKeyChain.QueryAsRecord(searchRecord, out code);

			if (code == SecStatusCode.Success)
				return data;
			else
				return null;
		}
	}
}

