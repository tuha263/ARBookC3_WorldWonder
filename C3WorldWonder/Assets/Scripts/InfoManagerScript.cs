using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfoManagerScript : MonoBehaviour {

    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    public void Link()
    {
		Floop.FloopManager.Instance.ShowParentalGate((success) => {
			Debug.Log("ShowParentalGate returned with: " + success);
			if (success)
			{
				//	Application.OpenURL("http://google.com/");
				Application.OpenURL("http://thuviensach3d.com");

			}
		});
    }
    public void Back()
    {
        Application.LoadLevel("Menu");
    }
	public void Call()
	{
		Floop.FloopManager.Instance.ShowParentalGate((success) => {
			Debug.Log("ShowParentalGate returned with: " + success);
			if (success)
			{
				//	Application.OpenURL("http://google.com/");
				Application.OpenURL("tel://19006009");
			}
		});
	}
}
