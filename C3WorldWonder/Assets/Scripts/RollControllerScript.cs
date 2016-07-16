using UnityEngine;
using System.Collections;

public class RollControllerScript : MonoBehaviour {

    private const float ROTATERATE = 0.05F;
    public Vector3 trend;
    private MainManagerScript mainManagerScript;

    float rotationSpeed;
    void Awake()
    {
        trend = transform.eulerAngles;
        mainManagerScript = GameObject.Find("MainManager").GetComponent<MainManagerScript>();
    }

    public void RotateInTime(float time)
    {
        IEnumerator rotate = Rotate(time);
        StartCoroutine(rotate);
    }
    IEnumerator Rotate(float time)
    {
        float rotateSpace = 90 * ROTATERATE / time;
        float rotateTotal = 0;
        while (rotateTotal < 90)
        {
            gameObject.transform.eulerAngles -= new Vector3(0, rotateSpace, 0);
            rotateTotal += rotateSpace;
            yield return new WaitForSeconds(ROTATERATE);
        }
    }

    public void ResetAnimation()
    {
        StopAllCoroutines();
        transform.eulerAngles = trend;
    }
    void OnDisable()
    {
        ResetAnimation();
    }
}
