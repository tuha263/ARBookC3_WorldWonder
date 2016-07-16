using System;

namespace Floop.Native
{
	internal interface INativeBridge
	{
		String Version();
		String CurrentStatus();
		void SetLogLevel(string logLevel);
		
		String Start(String appKey, FloopSdk sdk);
		String ShowParentalGate();
		String HasVideo();
		String ShowVideo();

		String SaveWebFeatureLauncherButtonToPath(WebFeature feature, string destPath);
		String ShowWebFeature(WebFeature feature);
		
		void TrackAppEvent(string eventName);
		
		String ShowAdUnit(string adUnitID, float maxHeight, PlacementAnchor anchor);

		void RemoveViewWithID(string viewID);
		void ShowNativeDialog(string title, string message);
	}
}

