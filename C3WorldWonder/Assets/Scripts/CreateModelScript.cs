using UnityEngine;
using System.Collections;
using Vuforia;

public class CreateModelScript : MonoBehaviour {

    public string prefabName;


    public IEnumerator CreateModel3D()
    {
        GameObject modelPrefab;
        GameObject model;
        //model = (GameObject)Instantiate(modelPrefab);
        modelPrefab = Resources.Load("Prefabs/3Dmodel/" + prefabName) as GameObject;
        while (!modelPrefab)
        {
            yield return new WaitForEndOfFrame();
        }
        model = (GameObject)Instantiate(modelPrefab);
        model.transform.localScale *= 256;
        model.transform.parent = transform;
        gameObject.GetComponent<DefaultTrackableEventHandler>().vehicle = model;
    }
}
