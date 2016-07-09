using UnityEngine;
using Floop;
using System;
using System.Collections;


	public class FloopSdk  : MonoBehaviour
	{
		public string floopIOSAppKey;
		public string floopAndroidAppKey;
		private string floopAppKey;
		public FloopLogLevel LogLevel = FloopLogLevel.Warn;
		public bool isDebug = false;
		
		private FloopManager _manager;
		
		public delegate void FloopCallback<T>(bool hasRun, T result); 
	
		private delegate bool TryParse<T>(string str, out T data);

		internal event Action<AsyncMethod, String, object> AsyncMethodCompleted;
		internal event Action OnNextUpdate;


		public void Awake()
		{
			gameObject.name = "FloopSdk";
			DontDestroyOnLoad(gameObject);		

			Manager.SetLogLevel(LogLevel);
			Manager.SetDebugMode (isDebug);
			
			#if UNITY_EDITOR
				floopAppKey = floopIOSAppKey != null ? floopIOSAppKey : floopAndroidAppKey;
			#elif UNITY_IOS
				floopAppKey = floopIOSAppKey;
			#elif UNITY_ANDROID
				floopAppKey = floopAndroidAppKey;
			#endif

			Manager.Init(floopAppKey, this);
		
			FloopLogger.Debug("gameObject Awake: name: '{0}'; FloopAppKey: '{1}'", gameObject.name, floopAppKey);
				
		}
		
		public void Start()
		{
			if (null != floopAppKey)
			{
				Manager.Start();
			}		
		}

		public void Update()
		{
			if (null != OnNextUpdate)
			{
				Action action = OnNextUpdate;	
				OnNextUpdate = null;
				action();
			}
		}

		FloopManager Manager
		{
			get
			{
				if (null == _manager)
				{
					_manager = FloopManager.Instance;
				}
				
				return _manager;
			}
		}
				
		public void DoAtEndOfFrame(Action action)
		{
			StartCoroutine(WaitForEndOfFrameThen(action));
		}
	
		IEnumerator WaitForEndOfFrameThen(Action action)
		{
			yield return new WaitForEndOfFrame();
			action();
		
		}
		
		internal void FireStartCompleted(string eventData)
		{
			ProcessEventCompletion<FloopStatus>(eventData, TryParseStatus, AsyncMethod.Start);
		}
		
		internal static bool TryParseStatus(string statusString, out FloopStatus status)
		{
			bool success = Enum.IsDefined(typeof(FloopStatus), statusString);
			if (success)
			{
				status = (FloopStatus)Enum.Parse(typeof(FloopStatus), statusString, true);
			}
			else
			{
				status = FloopStatus.Unknown;
			}
				
			return success;
		}
		
		internal void FireCheckMessageCompleted(String eventData) {
			FloopLogger.Debug("FireCheckMessageCompleted: event ID {0}", eventData);
		}

		internal void FireHasVideoCompleted(String eventData)
		{
			Debug.Log ("FireHasVideoCompleted with " + eventData);
			FloopLogger.Debug("FireHasVideoCompleted: event ID {0}", eventData);
			ProcessEventCompletion<bool> (eventData, bool.TryParse, AsyncMethod.HasVideo);
		}

		internal void FireShowVideoCompleted(string eventData)
		{
			ProcessEventCompletion<FloopVideoResult>(eventData, TryParseVideoResult, AsyncMethod.ShowVideo);
		}

		internal static bool TryParseVideoResult(string videoCompletedStatus, out FloopVideoResult result)
		{
			Debug.Log ("videoCompletedStatus " + videoCompletedStatus);
			string[] resultTokens = videoCompletedStatus.Split(new char[]{','}, 2);
			char[] delimiter = {':'};
			string[] completed = resultTokens [0].Split(delimiter, 2);
			string[] ctaClicked  = resultTokens [1].Split(delimiter, 2);
			result = new FloopVideoResult(Convert.ToBoolean(completed[1]), Convert.ToBoolean(ctaClicked[1]));

			return true;
		}
		
		internal void FireShowParentalGateCompleted(string eventData)
		{
			ProcessEventCompletion<bool>(eventData, bool.TryParse, AsyncMethod.ShowParentalGate);
		}
		
		internal void FireSaveWebFeatureLauncherButtonToPathCompleted(string eventData)
		{
			ProcessEventCompletion<bool>(eventData, bool.TryParse, AsyncMethod.SaveWebFeatureLauncherButtonToPath);		
		}
		
		internal void FireShowWebFeatureCompleted(string eventData)
		{
			ProcessEventCompletion<string>(eventData, ParseStringIdent, AsyncMethod.ShowWebFeature);		
		}
	
		internal void FireShowAdUnitCompleted(string eventData)
		{
			ProcessEventCompletion<string>(eventData, ParseStringIdent, AsyncMethod.ShowAdUnit);		
		}

		static bool ParseStringIdent(string str, out string res)
		{
			res = str;
			return true;
		}
		
		void ProcessEventCompletion<T>(string eventData, TryParse<T> parseEventArg, AsyncMethod method)
		{
			string eventID, eventArgStr;
			if (ParseEventData(eventData, out eventID, out eventArgStr))
			{
				T eventArg;
				if (parseEventArg(eventArgStr, out eventArg))
				{
					FloopLogger.Debug("event ID {0} completed with {1}", eventID, eventArg);
		
					if (null != AsyncMethodCompleted)
					{
						AsyncMethodCompleted(method, eventID, eventArg);
					}
				}
				else
				{
					FloopLogger.Error("Could not parse eventData: {0} ({1})", eventData, eventArgStr);
				}
				
			}
			else
			{
				FloopLogger.Error("Received unexpected event data format: {0}", eventData);
			}
		}
		
		bool ParseEventData(string eventData, out string eventID, out string eventArg)
		{	
			string[] parts = eventData.Split(new char[]{'.'}, 2);
			bool success = 2 == parts.Length;
	
			if (success)
			{
				eventID = parts[0];
				eventArg = parts[1]; 
			}
			else
			{
				eventID = null;
				eventArg = null;
			}
				
			return success;
		}
}


