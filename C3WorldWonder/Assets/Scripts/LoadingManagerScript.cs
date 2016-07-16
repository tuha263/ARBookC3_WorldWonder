using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingManagerScript : MonoBehaviour
{
    public float startPosition;
    public float endPosition;
    public GameObject bus;
    public Text loadingText;
	public GameObject pleaseText;

    private const float MOVESPACE = 0.01f;
    private const float TIME = 2;
    private const float TIMESPACE = 0.00333333f;

    private AsyncOperation async = null;
    private float timeMin = 1.5f;
    private bool isInvoking = false;
    // Use this for initialization
    [SerializeField]
    //private SkeletonAnimation skeletonAnimation;
    void Start()
    {
		if (PlayerPrefs.GetInt ("Actived", 0) != 100) {
			pleaseText.GetComponent<Text> ().text = "XEM DEMO Ở BÌA SÁCH";
		}
        Screen.orientation = ScreenOrientation.Landscape;
        //INIT
        async = Application.LoadLevelAsync("Main");
        async.allowSceneActivation = false;
        bus.transform.position = new Vector3(startPosition, 0, 0);


        //START LOAD
        StartCoroutine("Load");
        StartCoroutine("wait2s");
        //skeletonAnimation.state.AddAnimation(0, "begin", false, 0);
        //skeletonAnimation.state.AddAnimation(0, "idle-scaletime0.3", true, 0);

    }
    IEnumerator wait2s()
    {
        yield return new WaitForSeconds(this.timeMin);
        StartCoroutine("StartMainScreen");
    }
    IEnumerator StartMainScreen()
    {
        while (bus.transform.position.x < endPosition)
            yield return new WaitForEndOfFrame();
        async.allowSceneActivation = true;
    }

    IEnumerator Load()
    {
        while (async != null)
        {
            yield return async;
        }
        yield return new WaitForEndOfFrame();
    }
    // Update is called once per frame
    void Update()
    {
        loadingText.text = Mathf.FloorToInt((bus.transform.position.x + endPosition) / (endPosition - startPosition) * 100).ToString() + "%";
        if (async != null && bus.transform.position.x < endPosition && !isInvoking)
        {
            InvokeRepeating("IncreaseBarInvoke", 0, TIMESPACE);
            isInvoking = true;
        }
    }
    void IncreaseBarInvoke()
    {
        if ((bus.transform.position.x + endPosition) / (endPosition - startPosition) >= async.progress + MOVESPACE)
        {
            CancelInvoke();
            isInvoking = false;
        }
        bus.transform.position += new Vector3(MOVESPACE, 0, 0);
    }

}
