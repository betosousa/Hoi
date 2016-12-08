using UnityEngine;
using System.Collections;

public class Horse : Unit
{

	// Use this for initialization
	void Start ()
	{
		speed = 2;
		level = 1;
		strength = 6;
		resistence = 8;
		intelligence = 8;
		range = 5;
		defense = 5;
		anim = GetComponent<Animator> ();
	}
}

