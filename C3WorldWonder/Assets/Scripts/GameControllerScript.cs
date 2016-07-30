using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour {

	private string rightAnswerResultText = "Chúc mừng, đáp án chính xác!";
	private string wrongAnswerResultText = "Chưa đúng rồi, thử lại nhé!";

	public Text Q;
	public Text aA;
	public Text aB;
	public Text aC;
	public Text aD;
	public Text result;
	public GameObject resultPanel;

	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.Portrait;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Answer(string a){
		switch (a) {
		case "A":
			ShowResult (rightAnswerResultText);
			break;
		default:
			ShowResult (wrongAnswerResultText);
			break;
		}
	}

	public void ShowResult(string mess){
		result.text = mess;
		resultPanel.active = true;
		Invoke ("DisableResultPanel", 3);
	}

	void DisableResultPanel(){
		resultPanel.active = false;
	}

	public void Back(){
		Application.LoadLevel ("Menu");
	}
}
