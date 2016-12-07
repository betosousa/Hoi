using UnityEngine;
using System.Collections;

public class StrongClass : Attributes
{
	// Use this for initialization
	void Start ()
	{
		level = 1;
		health = 10;
		strength = 15;
		defense = 12;
		intelligence = 5;
		resistence = 8;
		speed = 10;
		range = 5;
		anim = GetComponent<Animator> ();
	}
}

/*
	15
	12
	10
	10
	8
	5
	5
*/
