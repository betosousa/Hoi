using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
	CameraFollow cam;
	Transform[] players;
	int targetIndex = 0;
	public Text infoText, coinsText;
	const string NEW_TURN = "New turn for ";
	const float WAIT_TIME = 1.5f;

	// evento de alguem clicar no botao 'end turn'
	public delegate void EndTurnAction();
	public static event EndTurnAction OnEndTurn;

	// Cria um botao pra passar o turno
	void OnGUI(){
		Mark mark = players[targetIndex].GetComponent<Mark>();
		Rect sizeAndPosition = new Rect( Screen.width/2 - 50 , Screen.height-25, 120, 20);
		string buttonName = "End " + mark.lado + "'s Turn";
		bool buttonClicked = GUI.Button(sizeAndPosition, buttonName);

		if(buttonClicked){
			if(OnEndTurn != null)
				OnEndTurn();
		}
	}


	void Start () {
		cam = Camera.main.GetComponent<CameraFollow>();

		// quando alguem clicar no 'end turn' chama 'ChangePlayer'
		OnEndTurn += ChangePlayer;

		if(players != null){
			ChangePlayer();
		}


	}

	public void InitPlayers(GameObject m1, GameObject m2){
		players = new Transform[2];
		players[0] = m1.GetComponent<Transform>();
		players[1] = m2.GetComponent<Transform>();
	}

	void ChangePlayer(){
		players[targetIndex].gameObject.SetActive(false);
		targetIndex = (targetIndex + 1) % (players.Length);
		players[targetIndex].gameObject.SetActive(true);
		cam.SetTarget(players[targetIndex]);

		Mark mark = players[targetIndex].GetComponent<Mark>();
		mark.SetCoinsText(coinsText);
		StartCoroutine("InfoText", NEW_TURN + mark.lado);
	}

	IEnumerator InfoText(string text){
		infoText.text = text;
		yield return new WaitForSeconds(WAIT_TIME);
		infoText.text = "";
	}


}
