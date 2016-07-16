using System;

namespace Floop
{
	public class FloopVideoResult
	{
		public bool VideoCompleted { get; set; } 
		public bool CTAClicked { get; set; } 

		public FloopVideoResult (bool _VideoCompleted, bool _CTAClicked)
		{
			VideoCompleted = _VideoCompleted;
			CTAClicked = _CTAClicked;
		}
	}
}

