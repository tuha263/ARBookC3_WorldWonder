#if UNITY_IOS

using System;
using System.Text;
using System.Collections;
using UnityEngine;
using System.Runtime.InteropServices;


namespace Floop.Native
{
	internal class NativeBridgeIOS : INativeBridge
	{
		const string FP_IPHONE_DLL = "__Internal";
		
		[DllImport (FP_IPHONE_DLL)]
		static extern String FPBridgeVersion();
		
		[DllImport (FP_IPHONE_DLL)]
		static extern String FPBridgeStart(String appKey);
		
		[DllImport (FP_IPHONE_DLL)]
		static extern String FPBridgeShowParentalGate();

		[DllImport (FP_IPHONE_DLL)]
		static extern String FPBridgeCurrentStatus();
		
		[DllImport (FP_IPHONE_DLL)]
		static extern void FPBridgeSetLogLevel(String logLevel);
		
		[DllImport (FP_IPHONE_DLL)]
		static extern void FPBridgeShowNativeDialog(string title, string message);

		[DllImport (FP_IPHONE_DLL)]
		static extern String FPBridgeSaveWebFeatureLauncherButtonToPath(string featureName, string featureID, string destPath);
		
		[DllImport (FP_IPHONE_DLL)]
		static extern String FPBridgeShowWebFeature(string featureName, string featureID);

		[DllImport (FP_IPHONE_DLL)]
		static extern String FPBridgeHasVideo();

		[DllImport (FP_IPHONE_DLL)]
		static extern String FPBridgeShowVideo();

		[DllImport (FP_IPHONE_DLL)]
		static extern void FPBridgeTrackAppEvent(string eventName);

		[DllImport (FP_IPHONE_DLL)]
		static extern String FPBridgeShowAdUnit(string adUnitID, float maxHeight, PlacementAnchor anchor);
				
		[DllImport (FP_IPHONE_DLL)]
		static extern void FPBridgeRemoveViewWithID(string viewID);


		public String Version() 
		{
			return FPBridgeVersion();
		}
		
		public String Start(String appKey, FloopSdk sdk)
		{
			return FPBridgeStart(appKey);
		}

		public String ShowParentalGate()
		{
			return FPBridgeShowParentalGate();
		}

		public String CurrentStatus()
		{
			return FPBridgeCurrentStatus();
		}
		
		public void SetLogLevel(string logLevel)
		{
			FPBridgeSetLogLevel(logLevel);
		}
		
		public void ShowNativeDialog(string title, string message)
		{
			FPBridgeShowNativeDialog(title, message);
		}
		
		public String SaveWebFeatureLauncherButtonToPath(WebFeature feature, string destPath)
		{
			return FPBridgeSaveWebFeatureLauncherButtonToPath(featureName:feature.FeatureName.ToString(), featureID:feature.FeatureID, destPath:destPath);	
		}
		
		public String ShowWebFeature(WebFeature feature)
		{
			return FPBridgeShowWebFeature(featureName:feature.FeatureName.ToString(), featureID:feature.FeatureID);
		}

		public String HasVideo()
		{
			return FPBridgeHasVideo();
		}

		public String ShowVideo()
		{
			return FPBridgeShowVideo();
		}

		public void TrackAppEvent(string eventName)
		{
			FPBridgeTrackAppEvent(eventName);
		}

		public String ShowAdUnit(string adUnitID, float maxHeight, PlacementAnchor anchor)
		{
			return FPBridgeShowAdUnit(adUnitID:adUnitID, maxHeight:maxHeight, anchor:anchor);
		}

		public void RemoveViewWithID(string viewID)
		{
			FPBridgeRemoveViewWithID(viewID);
		}
	}
}


#endif
