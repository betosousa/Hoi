using UnityEngine;
using System.Collections;

public class DefenseClass : Unit
{
	// Use this for initialization
	void Start ()
	{
		level = 1;
		health = 12;
		strength = 8;
		defense = 15;
		intelligence = 8;
		resistence = 10;
		speed = 5;
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