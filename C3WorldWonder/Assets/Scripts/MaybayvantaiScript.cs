using UnityEngine;
using System.Collections;

public class MaybayvantaiScript : MonoBehaviour {

    public GameObject[] parachute;

    private int parachuteCount;

    public void Fire()
    {
        if (gameObject.active)
        {
            parachuteCount = 0;
            IEnumerator parachute = Parachute();
            StartCoroutine(parachute);
        }
    }

    IEnumerator Parachute()
    {
        yield return new WaitForSeconds(0.5f);
        this.parachute[parachuteCount].GetComponent<ParachuteScript>().Init();
        parachuteCount++;

        //yolo
        IEnumerator parachute = Parachute();
        StartCoroutine(parachute);

        if (parachuteCount > 5)
        {
            StopAllCoroutines();
        }
    }
}
