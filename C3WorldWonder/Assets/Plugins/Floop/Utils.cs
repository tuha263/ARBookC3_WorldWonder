using System;
using System.Timers;

namespace Floop
{
	internal static class Utils
	{
		public static void DoAfter (int timeoutMillis, Action action)
		{
			
			var timer = new Timer();
			timer.AutoReset = false;
			timer.Elapsed += (src, e) => {
				action();
			};
			
			timer.Interval = timeoutMillis;
			timer.Enabled = true;

		}

		public static void WriteAllBytes (string path, byte [] bytes)
		{
			using (System.IO.Stream stream = System.IO.File.Create (path)) {
				stream.Write (bytes, 0, bytes.Length);
			}
		}

	}
}

