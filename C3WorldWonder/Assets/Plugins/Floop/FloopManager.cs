using UnityEngine;
using System;
using System.Collections.Generic;


namespace Floop
{
	using Floop.Native;
	
	public delegate void FloopCallback<T> (T result);

	internal enum AsyncMethod
	{
		Start,
		ShowVideo,
		HasVideo,
		ShowParentalGate,
		SaveWebFeatureLauncherButtonToPath,
		ShowWebFeature,
		TrackAppEvent,
		ShowAdUnit,
		RemoveAdUnit,
	}

	public class FloopManager
	{
		const int MaxWorkQueueWaitMillis = 15 * 1000;
		
		static FloopManager _instance;
		INativeBridge nativeBridge;
		FloopSdk sdk;
		Dictionary<string,Delegate> eventCallbacks;
		bool notEnabledWarningShown;
		bool isDebug;
		Dictionary<PlacementAnchor, string> shownAds;

		bool started;
		List<Action> actionsPendingStart;
		
		static FloopManager()
		{
			_instance = new FloopManager();
		}
		
		public static FloopManager Instance { get { return _instance; } }
				
		
		FloopManager ()
		{
			eventCallbacks = new Dictionary<string,Delegate>();
			actionsPendingStart = new List<Action>();
			shownAds = new Dictionary<PlacementAnchor, string>();
			CreateNativeBridge();
		}
		
		
		void CreateNativeBridge()
		{
			if (RuntimePlatform.IPhonePlayer == Application.platform) {
#if UNITY_IOS
				nativeBridge = new NativeBridgeIOS();
#endif
			} else if (RuntimePlatform.OSXEditor == Application.platform || RuntimePlatform.WindowsEditor == Application.platform) {
#if UNITY_EDITOR
				nativeBridge = new NativeBridgeEditor ();
#endif
			} else if (RuntimePlatform.Android == Application.platform) {
#if UNITY_ANDROID
				nativeBridge = new NativeBridgeAndroid ();
#endif
			}
			else
			{
				nativeBridge = new NativeBridgeStub();
			}
		}
		
		public bool IsInitialized { get; private set; }
				
		public bool IsEnabled 
		{
			get { return IsInitialized && IsPlatformSupported; }
		}
		
		public string AppKey { get; private set; }
		
		public bool IsPlatformSupported
		{
			get
			{
				return null != this.nativeBridge;
			}
		}
		
		public String Version
		{
			get
			{
				String version = "FloopSDK ";
				if (IsPlatformSupported)
				{
					version += String.Format("[{0}]: {1}", GetBridgeName(), nativeBridge.Version());
				}
				else
				{
					version += "[unvailable: platform not supported]";
				}
				
				return version;
			}
		}

		string GetBridgeName()
		{
			string className = nativeBridge.GetType().Name;

			return className.Substring("NativeBridge".Length);

		}
		
		internal void Init(String appKey, FloopSdk sdk)
		{
			SetFloopSdk(sdk);	
			if (!String.IsNullOrEmpty(appKey))
			{
				this.AppKey = appKey;
				IsInitialized = true;
			}
				
		}
		
		internal bool Start()
		{
			//nativeBridge must be able to handle null AppKey
			if (IsPlatformSupported)
			{
				DoNativeAsync(AsyncMethod.Start, () => nativeBridge.Start(AppKey, sdk), (FloopCallback<FloopStatus>)StartCompleted);
			}
			return IsEnabled;
		}

		void StartCompleted(FloopStatus status)
		{

			lock(this)
			{
				FloopLogger.Debug("StartCompleted: invoking {0} pending actions", actionsPendingStart.Count);
				started = true;
				foreach (Action action in actionsPendingStart)
				{
					RunUserCallback(action);
				}
				actionsPendingStart.Clear();
			}
		}
		
		void SetFloopSdk(FloopSdk sdk)
		{
			this.sdk = sdk;
			this.sdk.AsyncMethodCompleted += (AsyncMethod m, string e, object d) => OnAsyncMethodCompleted(m, e, d); 
		}

		public void SetDebugMode(bool isDebug) 
		{
			this.isDebug = isDebug;
		}

		public void SetLogLevel(FloopLogLevel logLevel)
		{
			FloopLogger.SetLogLevel(logLevel);

			if (IsPlatformSupported)
			{
				nativeBridge.SetLogLevel(logLevel.ToString());
			}
		}
		
		public FloopStatus CurrentStatus
		{
			get
			{
				return DoIfEnabled("CurrentStatus", () => ParseStatus(nativeBridge.CurrentStatus()));
			}
		}
		
		FloopStatus ParseStatus(string statusString)
		{
			FloopStatus status;
			FloopSdk.TryParseStatus(statusString, out status);
			return status;
		}
			
		public void ShowParentalGate(Action<bool> callback)
		{
			DoNativeAsync(AsyncMethod.ShowParentalGate, () => nativeBridge.ShowParentalGate(), callback);
		}
		
		System.Collections.IEnumerator LoadImageTextureFromTempFile(string tempPath, FloopCallback<Texture2D> callback)
		{
			WWW file = new WWW("file://" + tempPath);
			yield return file;

			System.IO.File.Delete(tempPath);
			callback(file.texture);

		}

		public void HasVideo(Action<bool> callback)
		{
			DoNativeAsync(AsyncMethod.HasVideo, () => nativeBridge.HasVideo(), callback);
		}

		public void ShowVideo(Action<FloopVideoResult> callback)
		{
			if (this.CurrentStatus == FloopStatus.Initialized) {
				DoNativeAsync (AsyncMethod.ShowVideo, () => nativeBridge.ShowVideo (), callback);
			} else {
				Debug.Log("show video not ready to be called: " + this.CurrentStatus);
				ShowNativeDialog("ShowVideo Debug" , "showVideo not ready to be called - sdk state: " + this.CurrentStatus);
			}
		}

		public void GetWebFeatureLauncherImage(MonoBehaviour context, WebFeature feature, FloopCallback<Texture2D> callback)
		{
			string tempPath;

			if (RuntimePlatform.Android == Application.platform) {
				tempPath = Application.persistentDataPath + "/";
				string glyphs= "abcdefghijklmnopqrstuvwxyz0123456789";

				for(int i=0; i< 20; i++)
				{
					tempPath += glyphs[UnityEngine.Random.Range(0, glyphs.Length)];
				}

				tempPath += ".tmp";
				Debug.Log ("new random string =  " + tempPath);

			} else {
				tempPath = System.IO.Path.GetTempFileName();
			}

			Debug.Log ("tempPath=" + tempPath);
			FloopCallback<bool> saveCompleted = (bool success) => {

				FloopLogger.Debug("Button download completed with {0}", success);

				if (context != null)
				{
					if (success)
					{
						context.StartCoroutine(LoadImageTextureFromTempFile(tempPath, callback));
					}
					else
					{
						callback(null);
					}
				}
			};

			FloopLogger.Debug("Trying download of button at {0}", tempPath);
			DoAsyncAfterStart(AsyncMethod.SaveWebFeatureLauncherButtonToPath, 
			                  () => nativeBridge.SaveWebFeatureLauncherButtonToPath(feature:feature, destPath:tempPath), 
			                 saveCompleted
			                 );
		}

		public void ShowWebFeature(WebFeature feature, FloopCallback<string> callback)
		{
			DoAsyncAfterStart(AsyncMethod.ShowWebFeature, 
			                   () => nativeBridge.ShowWebFeature(feature), 
			                   callback
			                   );

		}

		
		public void TrackAppEvent(string eventName)
		{
			DoIfEnabled(desc: AsyncMethod.TrackAppEvent.ToString(), 
			                action: () => nativeBridge.TrackAppEvent(eventName)
			                );
		}

		public void ShowNativeDialog(String title, String message) {
			if(isDebug)
			{
				nativeBridge.ShowNativeDialog(title, message);
				FloopLogger.Warn ("is debug activated");
			}
			else
			{
				FloopLogger.Warn ("is debug not activated");
			}
		}

		public void ShowAdUnit(string adUnitID, float maximumHeight, PlacementAnchor anchor)
		{
			if (shownAds.ContainsKey(anchor))
			{
				FloopLogger.Warn("Not showing ad at {0} because another one is already being shown", anchor);
			}
			else
			{

				FloopCallback<string> showAdCompleted = (string viewID) => {
					FloopLogger.Debug("showAdCompleted: {0}", viewID);
					shownAds[anchor] = viewID;
				};

				DoAsyncAfterStart(AsyncMethod.ShowAdUnit, 
				                  () => nativeBridge.ShowAdUnit(adUnitID:adUnitID, maxHeight:maximumHeight, anchor:anchor),
				                  showAdCompleted);
			}
		}

		public void HideAdUnit(PlacementAnchor anchor)
		{
			string viewID;
			if(shownAds.TryGetValue(anchor, out viewID))
			{
				DoIfEnabled(desc: AsyncMethod.RemoveAdUnit.ToString(), 
				            action: () => nativeBridge.RemoveViewWithID(viewID)
				            );

				shownAds.Remove(anchor);
			}
		}

		void DoAsyncAfterStart(AsyncMethod method, Func<string> action, Delegate eventCallback)
		{
			DoAfterStart(() => DoAsyncIfEnabled(method, action, eventCallback));
		}

		void DoAsyncIfEnabled(AsyncMethod method, Func<string> action, Delegate eventCallback)
		{
			DoIfEnabled(method.ToString(), () => DoNativeAsync(method, action, eventCallback));
		}

		void DoIfEnabled(String desc, Action action)
		{
			DoIfEnabled<int>(desc, () => { 
				action();
				return 0;
			});
		}
		
		T DoIfEnabled<T>(String desc, Func<T> action)
		{
			T result;
			
			if (IsEnabled)
			{
				result = action();
			}
			else
			{
				EnsureNotEnabledWarningShown();
				result = default(T);
			}
			
			return result;
		}
		
		void EnsureNotEnabledWarningShown()
		{
			if (!notEnabledWarningShown)
			{
				notEnabledWarningShown  = true;
				FloopLogger.Warn("Operation failed: not enabled; appKey: {0}; platformSupported: {1}", AppKey, IsPlatformSupported);
				
				if (string.IsNullOrEmpty(this.AppKey) && IsPlatformSupported)
				{
					nativeBridge.ShowNativeDialog("Floop SDK", "Please enter your Floop AppKey (shown in your developer dashboard) in the FloopSDK prefab. See documentation for more details.");
				}			
			}
		}

		void DoAfterStart(Action call)
		{
			lock(this)
			{
				if (started)
				{
					call();
				}
				else
				{
					actionsPendingStart.Add(call);
				}
			}
		}

		
		void DoNativeAsync(AsyncMethod method, Func<string> action, Delegate eventCallback)
		{
			string eventID = action();
			EnqueueEventCallback(eventID, eventCallback);
		}
		
		void EnqueueEventCallback(string eventID, Delegate eventCallback)
		{
			if (eventCallback != null && eventID != null)
			{					
				lock(this)
				{
					eventCallbacks[eventID] = eventCallback;
				}
			}
		}
		
		void OnAsyncMethodCompleted(AsyncMethod method, string eventID, object result)
		{
			Delegate callback = DequeueEventCallback(eventID);
			if (null != callback)
			{
				//callback.DynamicInvoke(result);
				FloopLogger.Debug("Run aync callback for {0},{1}", method, eventID);
				RunUserCallback( () => callback.DynamicInvoke(result) );
			}
		}

		void RunUserCallback(Action action)
		{
			//by running the action within 
			sdk.OnNextUpdate += action;
		}
		
		Delegate DequeueEventCallback(string eventID)
		{
			Delegate callback;
			if (eventCallbacks.TryGetValue(eventID, out callback))
			{
				lock(this)
				{
					eventCallbacks.Remove(eventID);
				}
			}
			
			return callback;
		}

	} 
}

