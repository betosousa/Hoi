using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
	CameraFollow cam;
	Transform[] players;
	int targetIndex = 0;
	Text infoText;
	const string NEW_TURN = "New turn for ";
	const float WAIT_TIME = 1.5f;

	void Start () {
		cam = Camera.main.GetComponent<CameraFollow>();
		infoText = GameObject.FindObjectOfType<Text>();
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


		StartCoroutine("InfoText", NEW_TURN + players[targetIndex].GetComponent<Mark>().lado);
	}

	IEnumerator InfoText(string text){
		infoText.text = text;
		yield return new WaitForSeconds(WAIT_TIME);
		infoText.text = "";
	}

	public void EndTurn(){
		ChangePlayer();
	}
}
