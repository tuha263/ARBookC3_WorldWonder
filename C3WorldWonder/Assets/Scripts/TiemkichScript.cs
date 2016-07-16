using UnityEngine;
using System.Collections;

public class TiemkichScript : MonoBehaviour {

    public GameObject fire1;
    public GameObject fire2;

    public void StartFire()
    {
        fire1.active = true;
        fire2.active = true;
    }

    public void EndFire()
    {
        fire1.active = false;
        fire2.active = false;
    }

}
