using System;


namespace Floop
{
	public static class FloopLogger
	{
		static FloopLogLevel _logLevel = FloopLogLevel.Info;

		public static void SetLogLevel(FloopLogLevel level)
		{
			_logLevel = level;
		}
		
		public static void Debug(String format, params object[] args)
		{
			Log (FloopLogLevel.Debug, format, args);
		}
		
		public static void Info(String format, params object[] args)
		{
			Log (FloopLogLevel.Info, format, args);
		}
		
		public static void Warn(String format, params object[] args)
		{
			Log (FloopLogLevel.Warn, format, args);
		}

		public static void Error(String format, params object[] args)
		{
			Log (FloopLogLevel.Error, format, args);
		}

		public static void Fatal(String format, params object[] args)
		{
			Log (FloopLogLevel.Fatal, format, args);
		}
		
		public static void Log(FloopLogLevel level, String format, params object[] args)
		{
			if (level >= _logLevel)
			{
				string message = FormatMessage(level, format, args);
				
				if (FloopLogLevel.Error <= level)
				{
					UnityEngine.Debug.LogError(message);
				}
				else if (FloopLogLevel.Warn == level)
				{
					UnityEngine.Debug.LogWarning(message);
				}
				else
				{
					UnityEngine.Debug.Log(message);
				}
				
				//Append(message);
			}
		}

		static String FormatMessage(FloopLogLevel level, String format, params object[] args)
		{
			DateTime now = DateTime.Now;
			String nowStr = now.ToString("yyyy-MM-dd HH:mm:ss.fff");
			String msg = String.Format(format, args);

			String levelStr = level.ToString().ToUpper();
			
			String formatted = String.Format("{0} [floop-unity] [{1}]: {2}", nowStr, levelStr, msg);

			return formatted;
		}

		static void Append(string message)
		{
			UnityEngine.Debug.Log(message);
		}


	}
}

