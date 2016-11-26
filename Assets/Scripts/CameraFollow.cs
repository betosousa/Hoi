using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public float t = 0.5f;

	Transform player;
	Vector3 offset;

	void Start () {
		offset = transform.position - Vector3.zero;
	}
	
	void Update () {
		if(player != null)
			transform.position = Vector3.Lerp(transform.position, player.position + offset, t);
	}

	public void SetTarget(Transform target){
		player = target;
	}
}
