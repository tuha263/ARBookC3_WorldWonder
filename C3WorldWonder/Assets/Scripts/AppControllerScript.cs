using UnityEngine;
using System.Collections;

public class AppControllerScript : MonoBehaviour {

    private RollScript rollScript;
    private float zoom = 1;
    private Vector3[] defaultScale;

    public GameObject[] model;

    void Awake()
    {
        rollScript = GetComponent<RollScript>();
        for (int i = 0; i < model.Length; i++)
        {
            model[i].GetComponent<RollScript>().enabled = false;
        }
    }

    void Start()
    {
        defaultScale = new Vector3[model.Length];
        for (int i = 0; i < defaultScale.Length; i++)
        {
            defaultScale[i] = model[i].transform.localScale;
        }
    }

    public void Roll()
    {
        for (int i = 0; i < model.Length; i++)
        {
            model[i].GetComponent<RollScript>().enabled = !model[i].GetComponent<RollScript>().enabled;
        }
    }

    public void ZoomIn()
    {
        if (zoom  < 4f)
        {
            zoom *= 2;
            for (int i = 0; i < model.Length; i++)
            {
                model[i].transform.localScale *= 2;
            }
        }
    }

    public void ZoomOut()
    {
        if (zoom > 0.25f)
        {
            zoom /= 2;
        }
    }

    public void SetDefaultScale()
    {
        zoom = 1;
        for (int i = 0; i < model.Length; i++)
        {
            model[i].transform.localScale = defaultScale[i];
        }
    }

    private void Vibrate()
    {
        Handheld.Vibrate();
    }

    IEnumerator DisableFire(GameObject effect, float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("Disable");
        effect.active = false;
    }
}
