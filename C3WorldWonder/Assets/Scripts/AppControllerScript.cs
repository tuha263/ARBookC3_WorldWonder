using UnityEngine;
using System.Collections;

public class AppControllerScript : MonoBehaviour {

    private RollScript rollScript;
    private float zoom = 1;
    private Vector3[] defaultScale;

    public GameObject[] model;
    public GameObject[] effect;
    public GameObject[] canonFire;
    public GameObject[] akFire;

    public GameObject tenlua;
    public GameObject maybaytiemkich;
    public GameObject maybayvantai;
    public GameObject taungam;

    private Animator tenluaAnim;
    private Animator maybaytiemkichAnim;
    private Animator taungamAnim;
    private MaybayvantaiScript maybayvantaiScript;

    void Awake()
    {
        rollScript = GetComponent<RollScript>();
        for (int i = 0; i < model.Length; i++)
        {
            model[i].GetComponent<RollScript>().enabled = false;
        }
        tenluaAnim = tenlua.GetComponent<Animator>();
        maybaytiemkichAnim = maybaytiemkich.GetComponent<Animator>();
        taungamAnim = taungam.GetComponent<Animator>();
        maybayvantaiScript = maybayvantai.GetComponent<MaybayvantaiScript>();
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
            CheckEffect();
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
            CheckEffect();
            for (int i = 0; i < model.Length; i++)
            {
                model[i].transform.localScale /= 2;
            }
        }
    }

    private void CheckEffect()
    {
        bool check = zoom == 1;
        for (int i = 0; i < effect.Length; i++)
            {
                effect[i].active = check;
            }

    }

    public void Fire()
    {
        if (MainManagerScript.nameModel == "tenlua")
        {
            if (true)
            {
                tenluaAnim.enabled = true;
                tenluaAnim.Play("Fire", -1, 0f);
                Vibrate();
            }
        }
        else
        if (MainManagerScript.nameModel == "maybaytiemkich")
        {
            maybaytiemkichAnim.enabled = true;
            maybaytiemkichAnim.Play("Fire", -1, 0f);
            Vibrate();
        }
        else
        if (MainManagerScript.nameModel == "taungam")
        {
            taungamAnim.enabled = true;
            taungamAnim.Play("Fire", -1, 0f);
            Vibrate();
        }
        else
        {
            
            for (int i = 0; i < canonFire.Length; i++)
            {
                if (canonFire[i].active != true)
                {
                    
                    //fire
                    canonFire[i].active = true;
                    IEnumerator disableFire = DisableFire(canonFire[i], 5);
                    StartCoroutine(disableFire);
                    Vibrate();
                }
            }
        }
    }

    public void SetDefaultScale()
    {
        zoom = 1;
        CheckEffect();
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

    public void StartAKFire()
    {
        //scale
        zoom = 1;
        CheckEffect();
        Debug.Log(model.Length);
        for (int i = 0; i < model.Length; i++)
        {
            model[i].transform.localScale = defaultScale[i];
        }
        for (int i = 0; i < akFire.Length; i++)
        {
            akFire[i].active = true;
        }
        maybayvantaiScript.Fire();

    }

    public void EndAKFire()
    {
        for (int i = 0; i < akFire.Length; i++)
        {
            akFire[i].active = false;
        }
        maybayvantaiScript.StopAllCoroutines();
    }
}
