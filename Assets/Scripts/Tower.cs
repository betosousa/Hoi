using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tower : MonoBehaviour {

	float buttonWidth = 50, buttonHeight = 50, offset = 30;
	bool selected = false;
	Vector3 spawnPosition = new Vector3(0,0,-0.3f);
	public Mark m;

	Image image;

	[SerializeField] private Unit[] unitsPrefabs;
	[SerializeField] private Texture[] unitsImgs;

	void Start(){
		GameController.OnEndTurn += CloseShop;
		image = GetComponentsInChildren<Image>()[0];
		if(m != null){
			SetImageColor();
		}
			
	}

	void SetImageColor(){
		image.color = m.lado.Equals("Dragon")? Color.red : Color.blue;
	}
		


	public void OnTriggerEnter(Collider other){
<<<<<<< HEAD
=======

>>>>>>> 28bcf1bf3931d3b7ae6b6d09798416d8a9e7050b
		if(m == null){
			Unit unit = other.GetComponent<Unit>();
			if(unit != null){
				m = unit.mark;
				SetImageColor();
			}
		}else{
			Mark mark = other.GetComponent<Mark>();
			if(m != null && (mark!=null)){
				if(m.lado.Equals(mark.lado))
					mark.OnSelectUnit += OpenShop;
			}
		}

	}

	public void OnTriggerExit(Collider other){
		Mark mark = other.GetComponent<Mark>();
		if(mark != null){
			mark.OnSelectUnit -= OpenShop;
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
					if(m.GetCoins() >= unitsPrefabs[i].price){
						Unit unit = (Instantiate(unitsPrefabs[i].gameObject, transform.position + spawnPosition, Quaternion.identity) as GameObject).GetComponent<Unit>();
						unit.mark = m;
						m.TakeCoins(unit.price);
						m.unidades.Add(unit);
					}
				}
			}

		}
	}
		
}

