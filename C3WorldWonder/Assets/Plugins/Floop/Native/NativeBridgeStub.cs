using System;
using System.Collections.Generic;
using UnityEngine;

namespace Floop.Native
{
	internal class NativeBridgeStub : INativeBridge
	{
		protected FloopSdk Sdk { get; private set; }
		HashSet<string> trackedViewIDs;

		public NativeBridgeStub ()
		{
			trackedViewIDs = new HashSet<string>();
		}
		
		public virtual String Version()
		{
			return "2.2.0";
		}
		
		public String CurrentStatus()
		{
			return FloopStatus.LoggedOut.ToString();
		}
		
		public void SetLogLevel(string logLevel)
		{
		}
		
		public String Start(String appKey, FloopSdk sdk)
		{
			this.Sdk = sdk;
			return EmulateNativeBridgeAsync(CurrentStatus, sdk.FireStartCompleted);
		}
  
		public virtual String  HasVideo()
		{
			return EmulateNativeBridgeAsync(() => true.ToString(), Sdk.FireHasVideoCompleted);
		}

		public virtual string ShowVideo()
		{
			String videoCompletedStatus = "videoCompleted:" + true.ToString () + ",ctaClicked:" + true.ToString ();
			return EmulateNativeBridgeAsync(() => videoCompletedStatus, Sdk.FireShowVideoCompleted);
		}

		public virtual String ShowParentalGate()
		{
			return EmulateNativeBridgeAsync(() => true.ToString(), Sdk.FireShowParentalGateCompleted);
		}
		
		public virtual String SaveWebFeatureLauncherButtonToPath(WebFeature feature, string destPath)
		{
			return EmulateNativeBridgeAsync(() => false.ToString(), Sdk.FireSaveWebFeatureLauncherButtonToPathCompleted);
		}

		public virtual String ShowWebFeature(WebFeature webFeature)
		{
			return EmulateNativeBridgeAsync(() => "", Sdk.FireShowWebFeatureCompleted);
		}

		public virtual void ShowNativeDialog(string title, string message)
		{
		}

		public virtual void TrackAppEvent(string eventName)
		{
		}

		public virtual string ShowAdUnit(string adUnitID, float maxHeight, PlacementAnchor anchor)
		{
			string viewID = EmulateCreateTrackedView();
			return EmulateNativeBridgeAsync(() => viewID, Sdk.FireShowWebFeatureCompleted);
		}

		public virtual void RemoveViewWithID(string viewID)
		{
			if(!trackedViewIDs.Remove(viewID))
			{
				FloopLogger.Warn("RemoveViewWithID called for unknown view {0}", viewID);
			}
		}

		protected string EmulateNativeBridgeAsync(Func<string> action, Action<string> completion)
		{
			return EmulateNativeBridge(completion, (Action<string> callback) => {
				InvokeAfterSeconds(0.1f, () =>{
					string result = action();
					callback(result);
				});
			});
		}


		void InvokeAfterSeconds(float seconds, Action action)
		{
			Sdk.StartCoroutine(InvokeAfterSecondsCoroutine(seconds, action));
		}

		System.Collections.IEnumerator InvokeAfterSecondsCoroutine(float seconds, Action action)
		{
			yield return new WaitForSeconds(seconds);
			action();
		}

		
		protected static string EmulateNativeBridge(Action<string> completion, Action<Action<string>> action)
		{
			string eventID = GenEventID();

			action((string result) => {
				string eventData = MakeEventData(eventID, result);
				completion(eventData);
			});

			return eventID;
		}


		static string MakeEventData(string eventID, string eventArg)
		{
			return string.Format("{0}.{1}", eventID, eventArg);
		}

		string EmulateCreateTrackedView()
		{
			string viewID = GenUUID();
			trackedViewIDs.Add(viewID);
			return viewID;
		}
		
		static string GenEventID()
		{
			return GenUUID();
		}

		static string GenUUID()
		{
			return Guid.NewGuid().ToString().Replace("-", "").ToLower();
		}
	}
}

