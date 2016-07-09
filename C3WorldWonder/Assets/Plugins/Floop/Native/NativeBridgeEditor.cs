#if UNITY_EDITOR

using System;
using UnityEngine;
using System.Text;
using System.Text.RegularExpressions;

using UnityEditor;

namespace Floop.Native
{
	internal class NativeBridgeEditor : NativeBridgeStub
	{
		public NativeBridgeEditor ()
		{
		}

		public override string ShowParentalGate ()
		{
			LogAction("ShowParentalGate");
			return base.ShowParentalGate ();
		}

		public override void TrackAppEvent(string eventName)
		{
			LogAction("TrackAppEvent: {0}", eventName);
		}

		public override string ShowAdUnit (string adUnitID, float maxHeight, PlacementAnchor anchor)
		{
			LogAction("ShowAdUnit: {0}, {1}, {2}", adUnitID, maxHeight, anchor);
			return base.ShowAdUnit (adUnitID, maxHeight, anchor);
		}

		public override void RemoveViewWithID (string viewID)
		{
			LogAction("RemoveViewWithID: {0}", viewID);
			base.RemoveViewWithID (viewID);
		}

		public override String SaveWebFeatureLauncherButtonToPath(WebFeature feature, string destPath)
		{

			return EmulateNativeBridge(Sdk.FireSaveWebFeatureLauncherButtonToPathCompleted, 
			                           (Action<string> callback) => {
				FloopLogger.Debug("call sdk.StartCoroutine");
				Sdk.StartCoroutine(DownloadSampleIcon(feature, destPath, (bool success) => {
					callback(success.ToString());
				}));
				
			});
		}


		System.Collections.IEnumerator DownloadSampleIcon(WebFeature feature, string destPath, Action<bool> callback)
		{

			var fname = featureIconName(feature.FeatureName);
			string url = String.Format("http://app.getfloop.com/img/default-app-assets/{0}.png", fname);
			FloopLogger.Debug("Start DownloadSampleIcon for {0} at {1} to {2}", feature, url, destPath);
			WWW req = new WWW(url);
			yield return req;
			
			bool success = null == req.error;
			
			if (success)
			{
				Utils.WriteAllBytes(path:destPath, bytes:req.bytes);
			}
			else
			{
				FloopLogger.Error("Failed to download sample icon at {0}: {1}", url, req.error);
			}
			
			callback(success);
		}

		string featureIconName(WebFeatureName featureName)
		{
			var re = new Regex(@"[A-Z][a-z]+");
			var matches = re.Matches(featureName.ToString());
			StringBuilder sb = new StringBuilder();
			bool first = true;
			foreach (Match match in matches)
			{
				if (!first)
				{
					sb.Append("-");
				}

				sb.Append(match.Value.ToLower());

				first = false;
			}

			return sb.ToString();
		}

		public override string ShowWebFeature(WebFeature webFeature)
		{
			LogAction("ShowWebFeature: {0}", webFeature);
			return EmulateNativeBridgeAsync(() => "0123456789", Sdk.FireShowWebFeatureCompleted);
		}
		

		public override void ShowNativeDialog(string title, string message)
		{
			EditorUtility.DisplayDialog(title, message, "OK");
		}

		
		static void LogAction(string format, params object[] args)
		{
			string log = String.Format(format, args);
			Debug.Log("[FloopSDK: Editor Stub]: " + log);
		}


	}


}


#endif
