using UnityEngine;
using System.Collections;
using Vuforia;

public class MainManagerScript : MonoBehaviour {
        
    public static string nameModel;
    public static bool haveTarget;
    public GameObject targetO;
    public static GameObject target;

    public GameObject imageTarget;
    public GameObject imageTargetDemo;

    private AppControllerScript appcontrollerScript;

    // Use this for initialization
    void Awake()
    {
        appcontrollerScript = imageTarget.GetComponent<AppControllerScript>();
        //PlayerPrefs.SetInt("Actived", 0);
        target = targetO;
    }
    void Start () {
        Screen.orientation = ScreenOrientation.Landscape;
        CheckActive();
    }
	private void CheckActive()
    {
        if (PlayerPrefs.GetInt("Actived", 0) == 100)
        {
            Debug.Log(appcontrollerScript.model.Length);
            for (int i = 0; i < appcontrollerScript.model.Length; i++)
            {
                appcontrollerScript.model[i].transform.parent.gameObject.active = true;
            }
        }
        else
        {
            Debug.Log("InActive");
            for (int i = 0; i < appcontrollerScript.model.Length; i++)
            {
                appcontrollerScript.model[i].transform.parent.gameObject.active = false;
            }
            imageTargetDemo.active = true;
        }
    }
    public void Back()
    {
        Application.LoadLevel("Menu");
    }
}
