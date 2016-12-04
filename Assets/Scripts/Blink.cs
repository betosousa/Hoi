using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Blink : MonoBehaviour {
	
	public Color minColor, maxColor;

	Material material;
	Color target;
	float nextSwap, swapTime = 0.5f;
	bool isNextMax = false;


	void Start () {
		var renderer = GetComponent<MeshRenderer>();

		if(renderer == null){
			material = GetComponent<Text>().material;
		}else{
			material = renderer.material;
		}

		UpdateSwap();
	}

	protected void UpdateSwap(){
		nextSwap = Time.time+ swapTime;
		target = isNextMax? maxColor : minColor;
		isNextMax = !isNextMax;
	}

	protected void Update () {
		if(Time.time >= nextSwap){
			material.color = target;
			UpdateSwap();
		}
	}
}
