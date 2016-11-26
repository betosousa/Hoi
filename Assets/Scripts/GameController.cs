using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	CameraFollow cam;
	Transform[] players;
	int targetIndex = 0;


	void Start () {
		cam = Camera.main.GetComponent<CameraFollow>();
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
	}

	public void EndTurn(){
		ChangePlayer();
	}
}
