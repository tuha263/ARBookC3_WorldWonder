using UnityEngine;
using System.Collections;

public class TenluaScript : MonoBehaviour {

    public GameObject effect;

    public void Fire()
    {
        effect.active = true;
    }
    public void Init()
    {
        effect.active = false;
    }

}
