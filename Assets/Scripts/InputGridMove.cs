using UnityEngine;
using System.Collections;

public class InputGridMove : MonoBehaviour {
	private float stepTime = 0.25f, nextStep;
	private TileMap map;
	void Start(){
		nextStep = Time.time;
		map = GameObject.FindObjectOfType<TileMap>();
	}

	void Update(){
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
	}
}
