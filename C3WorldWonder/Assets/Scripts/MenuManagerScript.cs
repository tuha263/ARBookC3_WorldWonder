using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Floop;


public class MenuManagerScript : MonoBehaviour {

    public GameObject activedListButton;
    public GameObject notActiveListButton;
	FloopManager floopManager = FloopManager.Instance;

    // Use this for initialization
    void Start() {
		PlayerPrefs.SetInt ("Actived", 100);
        Screen.orientation = ScreenOrientation.Portrait;
        if (PlayerPrefs.GetInt("Actived", 0) == 100)
        {
            activedListButton.active = true;
            notActiveListButton.active = false;
        }
        else
        {
            activedListButton.active = false;
            notActiveListButton.active = true;
        }
    }
	
    public void LoadScreen(int k)
    {
        switch (k)
        {
            case 1:
                Application.LoadLevel("Info");
                break;
            case 2:
                Application.LoadLevel("Loading");
                break;
            case 3:
                Application.LoadLevel("Loading");
                break;
            case 4:
                Application.LoadLevel("Loading");
                break;
            case 5:
				floopManager.ShowParentalGate((success) => {
					Debug.Log("ShowParentalGate returned with: " + success);
					if (success)
					{
					//	Application.OpenURL("http://google.com/");
						Application.LoadLevel("Active");
					}
				});
                break;
        }
    }
    public void Call()
    {
		floopManager.ShowParentalGate((success) => {
			Debug.Log("ShowParentalGate returned with: " + success);
			if (success)
			{
			//	Application.OpenURL("http://google.com/");
				Application.OpenURL("tel://19006009");
			}
		});


    }
	public void MoreBookLink()
	{
		floopManager.ShowParentalGate((success) => {
			Debug.Log("ShowParentalGate returned with: " + success);
			if (success)
			{
				//	Application.OpenURL("http://google.com/");
				Application.OpenURL("http://thuviensach3d.com");

			}
		});
	}

	public void Game(){
		Application.LoadLevelAsync("Game");
	}
}
