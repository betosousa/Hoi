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
	//bool hasEnemy = false;

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
		if (!hasEnemy ()) {
			map.DrawRange (transform.position, range, false);
			mark.OnTileSelect += MovePosition;
		} else {
			map.DrawRange (transform.position, rangeAtk, true);
			mark.OnTileSelect += attack;
		}
	}

	public int dano(Unit enemy)
	{
		if (strength > enemy.defense) {
			return (strength - enemy.defense);
		} else {
			Debug.Log ("Defendeu");
			return 0;
		}
			
	}

	bool hasEnemy()
	{
		Collider[] hitCollider = Physics.OverlapSphere (transform.position, rangeAtk);
		int i=0;
		Debug.Log (hitCollider.Length);
		while(i < hitCollider.Length)
		{

			Unit enemy = hitCollider[i].GetComponent<Unit> ();
			if (enemy != null ) {
				// verifica se não estão do mesmo lado
				if ( !enemy.mark.lado.Equals (mark.lado) ) {
					map.DrawRange (transform.position, rangeAtk, true);
					return true;
				}
			}
			i++;
		}
		return false;
	}

	void attack(Vector3 pos)
	{
		RaycastHit hit;
		Ray ray = new Ray (pos, Vector3.back);
		if (Physics.Raycast(ray, out hit, 1)) {
			Unit enemy = hit.collider.GetComponent<Unit> ();
			if (enemy != null ) {
				// verifica se não estão do mesmo lado
				if ( !enemy.mark.lado.Equals (mark.lado) ) {
					enemy.health -= dano (enemy);
					Debug.Log ( "atacou " );
					Debug.Log ( dano (enemy) );


				}
			}
			mark.OnTileSelect -= attack;
			//desdesenhar range
			havePlayed = true;
			map.UnDrawRange(transform.position, rangeAtk);
			// Verifica se morreu
			if (enemy.health <= 0) {
				Destroy (enemy.gameObject);
			}

		}
	}

	void contragolpe()
	{
		
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
			//desenhar range de ataque
			if (hasEnemy()) {
				map.DrawRange (transform.position, rangeAtk, true);
				mark.OnTileSelect += attack;
			}

		}
	}
}
