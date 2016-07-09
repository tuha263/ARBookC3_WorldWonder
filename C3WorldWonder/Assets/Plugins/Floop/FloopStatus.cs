using System;

namespace Floop
{
	public enum FloopStatus
	{
		Unknown = -1,
		NotInitialized,
	    Initializing,
		InitializedWithoutAppKey,
		Initialized,
	    LoggedOut,
	    WaitingForNetwork,
	    Error,
	}
}

