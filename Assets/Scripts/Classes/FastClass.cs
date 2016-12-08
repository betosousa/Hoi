using UnityEngine;
using System.Collections;

public class FastClass : Unit
{
	// Use this for initialization
	void Start ()
	{
		level = 1;
		health = 8;
		strength = 10;
		defense = 5;
		intelligence = 10;
		resistence = 5;
		speed = 15;
		range = 10;
		anim = GetComponent<Animator> ();
	}
}