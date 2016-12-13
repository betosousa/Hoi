using UnityEngine;
using UnityEngine.UI;
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
	public bool haveMoved = false;
	private Slider heathBar;
	private Image barFill;

	//Audio
	private AudioSource source;
	public AudioClip shootSound;

	void Awake() {
		
		map = GameObject.FindObjectOfType<TileMap>();
		GameController.OnEndTurn += EndTurn;
		source = GameObject.Find("blii").GetComponent<AudioSource>();
	}

	protected void SetSlider(){
		heathBar = GetComponentInChildren<Slider>();
		heathBar.maxValue = health;

		Image[] imgs = GetComponentsInChildren<Image>();
		barFill = (imgs[0].name == "Fill") ? imgs[0] : imgs[1];
		barFill.color = mark.lado.Equals("Dragon")? Color.red : Color.blue;

		UpdateHealthBar();
	}

	public void Die(){
		GameController.OnEndTurn -= EndTurn;
		mark.unidades.Remove (this);
		Debug.Log ("DEAD");
		Destroy(gameObject);
	}

	void EndTurn(){
		ClearStats ();
		map.UnDrawRange(transform.position, range);
	}

	protected virtual void SetAttributes(){}

	public void InitUnit(Mark m){
		mark = m;
		SetAttributes();
		SetSlider();
	}

	void UpdateHealthBar(){
		heathBar.value = health;
	}

	public void TakeDamage(float amount){
		health -= amount;
		UpdateHealthBar();
	}

	public void OnTriggerEnter(Collider other){
		DisplayStats ();
		Mark m = other.GetComponent<Mark>();
		if(m != null){
			if(!haveMoved){
				mark.OnTileSelect += SelectedUnit;
			}
		}
	}

	public void OnTriggerExit(Collider other){
		ClearStats ();
		Mark m = other.GetComponent<Mark>();
		if(m != null){
			if(!haveMoved){
				mark.OnTileSelect -= SelectedUnit;
			}
		}
	}

	void DisplayStats() {
		GameObject.Find ("Side").GetComponent<Text>().text = lado;
		GameObject.Find ("Health").GetComponent<Text>().text = "Health: " + health;
		GameObject.Find ("Strength").GetComponent<Text>().text = "Strength: " + strength;
		GameObject.Find ("Defense").GetComponent<Text>().text = "Defense: " + defense;
		GameObject.Find ("Intelligence").GetComponent<Text>().text = "Intelligence: " + intelligence;
		GameObject.Find ("Resistance").GetComponent<Text>().text = "Resistance: " + resistence;
		GameObject.Find ("Speed").GetComponent<Text>().text = "Speed: " + speed;
		GameObject.Find ("Range").GetComponent<Text>().text = "Range: " + range;
	}

	void ClearStats() {
		GameObject.Find ("Side").GetComponent<Text>().text = "";
		GameObject.Find ("Health").GetComponent<Text>().text = "";
		GameObject.Find ("Strength").GetComponent<Text>().text = "";
		GameObject.Find ("Defense").GetComponent<Text>().text = "";
		GameObject.Find ("Intelligence").GetComponent<Text>().text = "";
		GameObject.Find ("Resistance").GetComponent<Text>().text = "";
		GameObject.Find ("Speed").GetComponent<Text>().text = "";
		GameObject.Find ("Range").GetComponent<Text>().text = "";
	}

	void SelectedUnit(Vector3 position){
		if(!haveMoved){
			map.DrawRange (transform.position, range, false);
			mark.OnTileSelect += MovePosition;
		}
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
		if(map.IsInRange(transform.position, pos, rangeAtk)){
			RaycastHit hit;
			Ray ray = new Ray (pos, Vector3.back);
			if (Physics.Raycast(ray, out hit, 1)) {
				Unit enemy = hit.collider.GetComponent<Unit> ();
				if (enemy != null ) {
					// verifica se não estão do mesmo lado
					if ( !enemy.mark.lado.Equals (mark.lado) ) {
						enemy.TakeDamage( Dano (enemy));
						int contra = Contragolpe (enemy);
						source.Play();
						//TODO: não ta pegando o outro "birl" no contragolpe
						if (contra > 0) {
							source.Stop ();
							source.Play ();
						}
						TakeDamage(contra);
						Debug.Log ( "atacou " );
						Debug.Log ( Dano (enemy) );
						Debug.Log ( "contragolpe " );
						Debug.Log ( contra );
						Debug.Log ("akiii");
						Debug.Log (source);



						// Verifica se morreu
						if (enemy.health <= 0) {
							enemy.Die();
						}
					}
				}


				mark.OnTileSelect -= Attack;
				//desdesenhar range
				haveMoved = true;
				map.UnDrawRange(transform.position, rangeAtk);

			}
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
			haveMoved = true;
			mark.OnTileSelect -= MovePosition;
			//desenhar range de ataque
			if (HasEnemy()) {
				map.DrawRange (transform.position, rangeAtk, true);
				mark.OnTileSelect += Attack;
			}

		}
	}
}

