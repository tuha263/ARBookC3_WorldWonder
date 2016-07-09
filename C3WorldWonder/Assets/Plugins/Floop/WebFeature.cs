using UnityEngine;
using System;

namespace Floop
{
	public enum WebFeatureName
	{
		CrossPromotion,
		VideoGallery,
	}

	public class WebFeature
	{
		public WebFeature (string description, WebFeatureName featureName, string featureID)
		{
			this.Description = description;
			this.FeatureName = featureName;
			this.FeatureID = featureID;
		}

		public WebFeatureName FeatureName { get; private set; }
		public string FeatureID { get; private set; }
		public string Description { get; private set; }

		public override string ToString ()
		{
			return string.Format ("[WebFeature: FeatureName={0}, FeatureID={1}, Description={2}]", FeatureName, FeatureID, Description);
		}
	}
}

