using UnityEngine;
using System.Collections;

using Floop;

public class FloopCrossPromotionButton : WebFeatureButton 
{
	public string iOSCrossPromoName = null;
	public string androidCrossPromoName = null;
	private string CrossPromoName = null;
	
	protected override WebFeature CreateWebFeature ()
	{
		#if UNITY_EDITOR
		CrossPromoName = iOSCrossPromoName != null ? iOSCrossPromoName : androidCrossPromoName;
		#elif UNITY_IOS
		CrossPromoName = iOSCrossPromoName;
		#elif UNITY_ANDROID
		CrossPromoName = androidCrossPromoName;
		#endif
		
		WebFeature feature = new WebFeature(
			description: "Cross-Promotion",
			featureName: WebFeatureName.CrossPromotion,
			featureID: CrossPromoName
			);
		
		return feature;
	}
}
  


