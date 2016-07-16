using UnityEngine;
using System.Collections;

public class RollScript : MonoBehaviour {

    public float ROLLINGRATE = 300F;

    public int rollTrend;

    // Update is called once per frame
    void Update() {
        switch (rollTrend) {
            case 1:
                transform.Rotate(Vector3.right * Time.deltaTime * ROLLINGRATE);
                break;
            case 2:
                transform.Rotate(Vector3.left * Time.deltaTime * ROLLINGRATE);
                break;
            case 3:
                transform.Rotate(Vector3.forward * Time.deltaTime * ROLLINGRATE);
                break;
            case 4:
                transform.Rotate(Vector3.back * Time.deltaTime * ROLLINGRATE);
                break;
            case 5:
                transform.Rotate(Vector3.up * Time.deltaTime * ROLLINGRATE);
                break;
            case 6:
                transform.Rotate(Vector3.down * Time.deltaTime * ROLLINGRATE);
                break;

        }
    }
}
