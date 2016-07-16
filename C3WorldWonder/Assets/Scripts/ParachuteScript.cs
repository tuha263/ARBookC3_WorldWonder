using UnityEngine;
using System.Collections;

public class ParachuteScript : MonoBehaviour {

    private Vector3 STARTPOSITION;

    void Awake()
    {
        STARTPOSITION = transform.localPosition;
        gameObject.active = false;
    }

    public void Init()
    {
        transform.localPosition = STARTPOSITION;
        gameObject.active = true;
        Invoke("End", 5);
    }

    private void End()
    {
        gameObject.active = false;
    }
}
