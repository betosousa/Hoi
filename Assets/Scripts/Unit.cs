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
		GameController.OnEndTurn += EndTurn;
	}

	public void Die(){
		GameController.OnEndTurn -= EndTurn;
		Destroy(gameObject);
	}

	void EndTurn(){
		map.UnDrawRange(transform.position, range);
	}


	public void OnTriggerEnter(Collider other){
		Mark m = other.GetComponent<Mark>();
		if(m != null){
			if(!havePlayed){
				mark.OnTileSelect += SelectedUnit;
			}
		}
	}

	public void OnTriggerExit(Collider other){
		Mark m = other.GetComponent<Mark>();
		if(m != null){
			if(!havePlayed){
				mark.OnTileSelect -= SelectedUnit;
			}
		}
	}

	void SelectedUnit(Vector3 position){
		//if (!HasEnemy ()) {
			map.DrawRange (transform.position, range, false);
			mark.OnTileSelect += MovePosition;
		//} else {
		//	map.DrawRange (transform.position, rangeAtk, true);
		//	mark.OnTileSelect += Attack;
		//}
	}

	public int Dano(Unit enemy)
	{
		if (strength > enemy.defense) {
			return (strength - enemy.defense);
		} else {
			Debug.Log ("Defendeu");
			return 0;
		}
			
	}

	public int Contragolpe(Unit enemy)
	{
		if ( Random.Range(0,100) >= enemy.speed + (enemy.speed - speed) ) {
			
			return Dano (enemy) / 2;
		} else {
			return 0;
		}

	}

	bool HasEnemy()
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

	void Attack(Vector3 pos)
	{
		RaycastHit hit;
		Ray ray = new Ray (pos, Vector3.back);
		if (Physics.Raycast(ray, out hit, 1)) {
			Unit enemy = hit.collider.GetComponent<Unit> ();
			if (enemy != null ) {
				// verifica se não estão do mesmo lado
				if ( !enemy.mark.lado.Equals (mark.lado) ) {
					enemy.health -= Dano (enemy);
					int contra = Contragolpe (enemy);
					health -= contra;
					Debug.Log ( "atacou " );
					Debug.Log ( Dano (enemy) );
					Debug.Log ( "contragolpe " );
					Debug.Log ( contra );

					// Verifica se morreu
					if (enemy.health <= 0) {
						enemy.Die();
					}
				}
			}
			mark.OnTileSelect -= Attack;
			//desdesenhar range
			havePlayed = true;
			map.UnDrawRange(transform.position, rangeAtk);

		}
	}

	bool SamePosition (Vector3 pos){
		return (pos.x == transform.position.x) && (pos.y == transform.position.y); // o z deles eh diferente
	}

	void MovePosition(Vector3 position){
		if((!map.IsLockedPosition(position) && map.IsInRange(transform.position, position, range))// nova posicao livre na range
			|| SamePosition(position)){ // nao se moveu
			map.UnLockPosition(transform.position);
			map.UnDrawRange(transform.position, range);
			position.z = zOffset;
			transform.position = position;
			map.LockPosition(transform.position);
			havePlayed = true;
			mark.OnTileSelect -= MovePosition;
			//desenhar range de ataque
			if (HasEnemy()) {
				map.DrawRange (transform.position, rangeAtk, true);
				mark.OnTileSelect += Attack;
			}

		}
	}
}
