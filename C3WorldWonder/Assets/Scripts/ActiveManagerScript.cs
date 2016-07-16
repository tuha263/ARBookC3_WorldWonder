using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActiveManagerScript : MonoBehaviour {

    public Text codeInputText;
    public GameObject success;
    public GameObject fail;
    private string codeListString = null;

    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        TextAsset codeList = Resources.Load("CodeList") as TextAsset;
        Debug.Log(codeList.text);
        Debug.Log(codeList.text.Length);
        if (PlayerPrefs.GetString("CodeList", "") == "")
        {
            PlayerPrefs.SetString("CodeList", codeList.text);
        }
        codeListString = PlayerPrefs.GetString("CodeList");
    }

    public void EditValueEnd()
    {
        if (codeListString.Contains(codeInputText.text.ToUpper()) )
        {
            PlayerPrefs.SetInt("Actived", 100);
            success.active = true;
            Invoke("Back", 1);
        }
        else
        {
            fail.active = true;
            Invoke("ActiceAgain", 1);
        }
    }
    public void Back()
    {
        Application.LoadLevel("Menu");
    }
    public void ActiceAgain()
    {
        fail.active = false;
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
