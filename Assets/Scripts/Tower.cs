using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tower : MonoBehaviour {

	bool givecoins = false;
	float buttonWidth = 50, buttonHeight = 50, offset = 30;
	bool selected = false;
	Vector3 spawnPosition = new Vector3(0,0,-0.3f);
	public Mark m;

	Image image;
	GameController gc;

	private Unit[] unitsPrefabs;
	private Texture[] unitsImgs;

	[SerializeField] private Unit[] snakePrefabs;
	[SerializeField] private Texture[] snakeImgs;

	[SerializeField] private Unit[] dragonPrefabs;
	[SerializeField] private Texture[] dragonImgs;

	void Start(){
		GameController.OnEndTurn += CloseShop;
		GameController.OnEndTurn += GiveCoin;
		Image[] imgs = GetComponentsInChildren<Image>();
		image = (imgs[0].name == "taken") ? imgs[0] : imgs[1]; 
		SetImageColor();
		gc = GameObject.FindObjectOfType<GameController>();		


	}

	void GiveCoin(){
		if(m != null && givecoins){
			m.ReceiveCoins(1);
		}
		givecoins = !givecoins;
	}

	void SetImageColor(){
		if(m != null){	
			image.color = m.lado.Equals("Dragon")? Color.red : Color.blue;
		}else{
			image.color = Color.white;
		}
			
	}
		

	public void OnTriggerEnter(Collider other){
		Unit unit = other.GetComponent<Unit>();
		if(unit != null){
			string oldSide = null;
			if(m != null){
				oldSide = m.lado;
			}
			m = unit.mark;
			SetImageColor();
			if(!unit.mark.lado.Equals(oldSide))
				gc.ChangeScore(oldSide, unit.mark.lado);
		}
		if(m != null){
			Mark mark = other.GetComponent<Mark>();
			if(m != null && (mark!=null)){
				if(m.lado.Equals(mark.lado))
					mark.OnTowerSelect += OpenShop;
			}
		}

	}

	public void OnTriggerExit(Collider other){
		Mark mark = other.GetComponent<Mark>();
		if(mark != null){
			mark.OnTowerSelect -= OpenShop;
			selected = false;
		}
	}

	void OpenShop(){
		selected = true;
	}

	void CloseShop(){
		selected = false;
	}

	void OnGUI(){
		if(selected){

			if(m.lado.Equals("Dragon")){
				unitsPrefabs = dragonPrefabs;
				unitsImgs = dragonImgs;
			}else{
				unitsPrefabs = snakePrefabs;
				unitsImgs = snakeImgs;
			}
				
			for(int i = 0; i < unitsPrefabs.Length; i++){
				Rect pos = new Rect(offset + i*(buttonWidth + offset), Screen.height - (buttonHeight + offset), buttonWidth, buttonHeight);

				if(GUI.Button(pos, unitsImgs[i])){
					RaycastHit hit;
					Ray ray = new Ray (transform.position, Vector3.back);
					if (Physics.Raycast(ray, out hit, 1)) {
						if(hit.collider.GetComponent<Unit>() != null){
							return;
						}
					}

					GameObject unitObject = Instantiate (unitsPrefabs [i].gameObject, transform.position + spawnPosition, Quaternion.identity) as GameObject; 

					Unit unit = (unitObject).GetComponent<Unit>();
					unit.InitUnit (m);

					Debug.Log ("coins: " + m.GetCoins());
					Debug.Log ("preço: " + unit.price);
					if (m.GetCoins () >= unit.price) {
						m.TakeCoins (unit.price);
						m.unidades.Add (unit);
					} else {
						Debug.Log ("Entrou...");
						Destroy (unitObject);
						Debug.Log ("Saiu...");
					}
				}
			}

		}
	}
		
}

