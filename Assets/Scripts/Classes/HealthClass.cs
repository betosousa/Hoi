using UnityEngine;
using System.Collections;

public class HealthClass : Unit
{
	protected override void SetAttributes ()
	{
		level = 1;
		health = 20;
		strength = 5;
		defense = 10;
		intelligence = 10;
		resistence = 10;
		speed = 5;
		range = 5;
		price = 2;
		rangeAtk = 2;
	}
}
