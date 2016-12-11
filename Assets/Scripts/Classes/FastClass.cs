using UnityEngine;
using System.Collections;

public class FastClass : Unit
{
	protected override void SetAttributes ()
	{
		level = 1;
		health = 8;
		strength = 10;
		defense = 5;
		intelligence = 10;
		resistence = 5;
		speed = 15;
		range = 10;
		price = 1;
		rangeAtk = 2;
	}
}