using UnityEngine;
using System.Collections;
using Floop;

public class FloopSampleApp : MonoBehaviour {

	//Initialize FloopManager
	FloopManager floopManager = FloopManager.Instance;

	//ParentalGate 
	public void OnParentGateClicked()
	{
		floopManager.ShowParentalGate((success) => {
			if(success){
				floopManager.ShowNativeDialog("parentalGate", "ShowParentalGate returned with SUCCESS");
			}else{
				floopManager.ShowNativeDialog("parentalGate", "ShowParentalGate returned with FAILED");
			}
		});
	}

	//HasVideo
	public void OnHasVideoClicked() {
		floopManager.HasVideo((success) => {
			string message = "[Sample] HasVideo returned with: " + success;
			floopManager.ShowNativeDialog("hasVideo", message);
		});
	}
		
	//Show Video
	public void OnVideoAdClicked() {
		floopManager.ShowVideo((result) => {
			string message = "[Sample] ShowVideo returned with: videoCompleted=" + result.VideoCompleted + " CTAClicked= " + result.CTAClicked;
			floopManager.ShowNativeDialog("showVideo", message);
		}); 
	}
}
