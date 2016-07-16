#if UNITY_ANDROID

using UnityEngine;
using System.Collections;

namespace Floop.Native
{
	internal class NativeBridgeAndroid : INativeBridge {
		private AndroidJavaObject floopSDKManager = null;
		private AndroidJavaObject activityContext = null;

		public string Version ()
		{
			return floopSDKManager.Call<string>("FPBridgeVersion");
		}

		public string CurrentStatus ()
		{
			return floopSDKManager.Call<string>("FPBridgeCurrentStatus");
		}
		
		public void SetLogLevel (string logLevel)
		{
			Debug.Log ("Android set log level call");
		}
		
		public string Start (string appKey, FloopSdk sdk)
		{
			FloopLogger.Debug ("Android Start Method Call");

			if (floopSDKManager == null) {
				using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
					activityContext = activityClass.GetStatic<AndroidJavaObject> ("currentActivity");
				}
				
				using (AndroidJavaClass pluginClass = new AndroidJavaClass("com.getfloop.android.sdk.UnityBridge")) {
					if (pluginClass != null) {
						floopSDKManager = pluginClass.CallStatic<AndroidJavaObject> ("instance");
					}
				}
			}

			return floopSDKManager.Call<string>("FPBridgeStart", activityContext, appKey);
		}
		
		public string ShowParentalGate ()
		{
			Debug.Log ("show parental gate click");
			return floopSDKManager.Call<string>("FPBridgeShowParentalGate");
		}

		public string HasVideo ()
		{
			Debug.Log ("has video click");
			return floopSDKManager.Call<string>("FPBridgeHasVideo");
		}

		public string ShowVideo ()
		{
			Debug.Log ("show video click");
			return floopSDKManager.Call<string>("FPBridgeShowVideo");
		}

		public string SaveWebFeatureLauncherButtonToPath (WebFeature feature, string destPath)
		{
			Debug.Log ("SaveWebFeatureLauncherButtonToPath");
			return floopSDKManager.Call<string>("FPBridgeSaveWebFeatureLauncherButtonToPath", feature.FeatureName.ToString(), feature.FeatureID, destPath);
		}

		public string ShowWebFeature (WebFeature feature)
		{
			Debug.Log ("show web feature");
			return floopSDKManager.Call<string>("FPBridgeShowWebFeature", feature.FeatureName.ToString(), feature.FeatureID);
		}

		public void TrackAppEvent (string eventName)
		{
			throw new System.NotImplementedException ();
		}

		public string ShowAdUnit (string adUnitID, float maxHeight, PlacementAnchor anchor)
		{
			throw new System.NotImplementedException ();
		}

		public void RemoveViewWithID (string viewID)
		{
			throw new System.NotImplementedException ();
		}

		public void ShowNativeDialog (string title, string message)
		{
			floopSDKManager.Call("FPBridgeShowNativeDialog", title, message);
		}
	}
}

#endif