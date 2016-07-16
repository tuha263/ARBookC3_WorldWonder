using UnityEngine;
using System.Collections;

public class DemoManagerScript : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        Screen.orientation = ScreenOrientation.Landscape;
    }

    // Update is called once per frame
    void Update () {
	
	}
    public void Back()
    {
        Application.LoadLevel("Menu");
    }
}
