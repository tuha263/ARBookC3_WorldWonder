using UnityEngine;
using System.Collections;

using Floop;

public class FloopVideoGalleryButton : WebFeatureButton 
{
	public string iOSVideoGalleryID = null;
	public string androidVideoGalleryID = null;
	private string VideoGalleryID = null;
	
	protected override WebFeature CreateWebFeature ()
	{
		#if UNITY_EDITOR
		VideoGalleryID = iOSVideoGalleryID != null ? iOSVideoGalleryID : androidVideoGalleryID;
		#elif UNITY_IOS
		VideoGalleryID = iOSVideoGalleryID;
		#elif UNITY_ANDROID
		VideoGalleryID = androidVideoGalleryID;
		#endif
		
		WebFeature feature = new WebFeature(			
		                                    description: "Video Gallery",
		                                    featureName: WebFeatureName.VideoGallery,
		                                    featureID: VideoGalleryID
		                                    );
		return feature;
	}
}



