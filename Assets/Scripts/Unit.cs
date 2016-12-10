using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour{

	public int price = 1;

	public int level;

	// atk e def
	public int strength;
	public int defense;

	// ranges de movimentacao e atk
	public int range;
	public int rangeAtk;

	// modificador de ataque
	public int intelligence;

	// modificador de defesa
	public int resistence;

	// ajuda a desviar
	public int speed;
	public float health;

	public string className;

	public string lado;

	float zOffset = -0.3f;

	TileMap map;
	public Mark mark;
	public bool havePlayed = false;


	void Awake() {
		map = GameObject.FindObjectOfType<TileMap>();
	}

	public void OnTriggerEnter(Collider other){
		Mark m = other.GetComponent<Mark>();
		if(m != null){
			if(!havePlayed){
				mark.OnSelectUnit += SelectedUnit;
			}
		}
	}

	public void OnTriggerExit(Collider other){
		Mark m = other.GetComponent<Mark>();
		if(m != null){
			if(!havePlayed){
				mark.OnSelectUnit-=SelectedUnit;
			}
		}
	}

	void SelectedUnit(){
		map.DrawRange(transform.position, range);
		mark.OnTileSelect += MovePosition;
	}

	void MovePosition(Vector3 position){
		if(!map.IsLockedPosition(position) && map.IsInRange(transform.position, position, range)){
			map.UnLockPosition(transform.position);
			map.UnDrawRange(transform.position, range);
			position.z = zOffset;
			transform.position = position;
			map.LockPosition(transform.position);
			havePlayed = true;
			mark.OnTileSelect -= MovePosition;
		}
	}
}
