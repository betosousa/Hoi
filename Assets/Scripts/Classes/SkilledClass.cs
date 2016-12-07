using UnityEngine;
using System.Collections;

public class SkilledClass : Attributes
{
	// Use this for initialization
	void Start ()
	{
		level = 1;
		health = 10;
		strength = 5;
		defense = 8;
		intelligence = 15;
		resistence = 12;
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