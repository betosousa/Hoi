using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public float t = 0.5f;

	public float minX = 0, minY = 0, maxX = 20, maxY = 20;

	Transform player;
	Vector3 offset;

	void Start () {
		offset = transform.position - Vector3.zero;
	}
	
	void Update () {
		if(player != null){
			Vector3 temp = player.position;

			temp.x =  Mathf.Clamp(temp.x, minX, maxX);
			temp.y =  Mathf.Clamp(temp.y, minY, maxY);

			transform.position = Vector3.Lerp(transform.position, temp + offset, t);

		}
	}

	public void SetTarget(Transform target){
		player = target;
	}
}
