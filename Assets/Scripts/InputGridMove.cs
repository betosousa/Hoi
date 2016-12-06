using UnityEngine;
using System.Collections;

public class InputGridMove : MonoBehaviour {
	private float stepTime = 0.25f, nextStep;
	private TileMap map;
	private GameController gc;
	private Color rangeColor;
	void Start(){
		nextStep = Time.time;
		map = GameObject.FindObjectOfType<TileMap>();
		gc = GameObject.FindObjectOfType<GameController>();
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Space))
			gc.EndTurn();

		int x = 0, y = 0;

		if(Time.time >= nextStep){
			float h = Input.GetAxis("Horizontal"), v = Input.GetAxis("Vertical");
			if(h < 0)		x = -1;
			if(h > 0)		x = 1;
			if(v > 0)		y = 1;
			if(v < 0)		y = -1;

			if(x != 0 || y != 0)	nextStep = Time.time + stepTime;
		}

		Vector3 newPosition = new Vector3(transform.position.x + x, transform.position.y + y, 0);

		if(map.IsValid(newPosition))
			transform.position = newPosition;

		if(Input.GetKeyDown(KeyCode.R)){
			map.DrawRange(transform.position, 10);
		}
		if(Input.GetKeyDown(KeyCode.U)){
			map.UnDrawRange(transform.position, 10);
		}
	}
}
