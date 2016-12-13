using UnityEngine;
using System.Collections;

public class ResClass : Unit
{
	protected override void SetAttributes ()
	{
		level = 1;
		health = 12;
		strength = 8;
		defense = 10;
		intelligence = 8;
		resistence = 15;
		speed = 5;
		range = 8;
		price = 5;
		rangeAtk = 2;
	}
}