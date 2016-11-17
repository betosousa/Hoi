using UnityEngine;
using System.Collections;

public class Blink : MonoBehaviour {
	
	public Color minColor, maxColor;

	Material material;
	Color target;
	float nextSwap, swapTime = 0.5f;
	bool isNextMax = false;


	void Start () {
		material = GetComponent<MeshRenderer>().material;
		UpdateSwap();
	}

	void UpdateSwap(){
		nextSwap = Time.time+ swapTime;
		target = isNextMax? maxColor : minColor;
		isNextMax = !isNextMax;
	}

	void Update () {
		if(Time.time >= nextSwap){
			material.color = target;
			UpdateSwap();
		}
	}
}
