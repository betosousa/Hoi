using UnityEngine;
using System.Collections;

public class Horse : MonoBehaviour {
	Animator anim;
	float speed;
	int turnSpeed;
	int strength;
	int resistence;
	int defense;
	int intelligence;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		speed=1.5f;
		turnSpeed=0;
		strength=10;
		resistence=2;
		defense=5;
		intelligence=4;
	}

	// Update is called once per frame
	void Update () 
	{
		var vertical = Input.GetAxis("Vertical");
		var horizontal = Input.GetAxis("Horizontal");
		if (vertical > 0)
		{
			transform.position += Vector3.up * speed * Time.deltaTime;
			anim.SetInteger("Direction", 2);
		}
		else if (vertical < 0)
		{
			anim.SetInteger("Direction", 0);
			transform.position += Vector3.down * speed * Time.deltaTime;
		}
		else if (horizontal > 0)
		{
			anim.SetInteger("Direction", 3);
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		else if (horizontal < 0)
		{
			anim.SetInteger("Direction", 1);
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		else if(vertical == 0 )
		{
			anim.SetInteger("Direction", 4);
		}
		movement ();
	}

	void movement()
	{
		var vertical = Input.GetAxis("Vertical");
		var horizontal = Input.GetAxis("Horizontal");
		if (vertical > 0)
		{
			WaitForSeconds(1);
			transform.position += Vector3.up * speed * Time.deltaTime;

		}
		else if (vertical < 0)
		{
			WaitForSeconds(1);
			transform.position += Vector3.down * speed * Time.deltaTime;
		}
		else if (horizontal > 0)
		{
			WaitForSeconds(1);
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		else if (horizontal < 0)
		{
			WaitForSeconds(1);
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
	}
	
}
