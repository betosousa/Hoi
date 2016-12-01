using UnityEngine;
using System.Collections;

public class Attributes : MonoBehaviour{
	
	public int level;
	public int strength;
	public int resistence;
	public int defense;
	public int range;
	public int intelligence;
	public float speed;
	public Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () 
	{
		update_animation();
		simple_movement (transform);
	}

	void update_animation()
	{
		var vertical = Input.GetAxis("Vertical");
		var horizontal = Input.GetAxis("Horizontal");
		if (vertical > 0)
		{
			anim.SetInteger("Direction", 3);
		}
		else if (vertical < 0)
		{
			anim.SetInteger("Direction", 1);
		}
		else if (horizontal > 0)
		{
			anim.SetInteger("Direction", 4);
		}
		else if (horizontal < 0)
		{
			anim.SetInteger("Direction", 2);
		}
		else if(vertical == 0 )
		{
			anim.SetInteger("Direction", 0);
		}
	}
	void simple_movement(Transform transform)
	{
		var vertical = Input.GetAxis("Vertical");
		var horizontal = Input.GetAxis("Horizontal");
		if (vertical > 0)
		{
			transform.position += Vector3.up * speed * Time.deltaTime;

		}
		else if (vertical < 0)
		{
			transform.position += Vector3.down * speed * Time.deltaTime;
		}
		else if (horizontal > 0)
		{
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		else if (horizontal < 0)
		{
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
	}
}
