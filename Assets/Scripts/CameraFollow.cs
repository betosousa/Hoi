using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public float t = 0.5f;

	Transform player;
	Vector3 offset;

	void Start () {
		player = GameObject.FindWithTag("Mark").GetComponent<Transform>();
		offset = transform.position - player.position;
	}
	
	void Update () {
		transform.position = Vector3.Lerp(transform.position, player.position + offset, t);
	}
}
