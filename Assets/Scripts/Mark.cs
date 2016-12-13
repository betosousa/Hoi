using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Mark : MonoBehaviour {

	private int coins = 20;

	public List<Unit> unidades;
	public string lado;//Dragon ou Snake

	public Text coinText;
	private static string COINS = "Coins: ";
	
	public delegate void SelectEventHandler();
	public delegate void MoveEventHandler(Vector3 position);
	public event SelectEventHandler OnTowerSelect;
	public event MoveEventHandler OnTileSelect;

	void Start(){
		unidades = new List<Unit>();
	}

	public int GetCoins(){
		return coins;
	}

	public void SetCoinsText(Text cText){
		coinText = cText;
		coinText.text = COINS + coins;
	}
		

	public void TakeCoins(int amount){
		coins -= amount;
		coinText.text = COINS + coins;
	}

	public void ReceiveCoins(int amount){
		coins += amount;

		if(coinText!=null)
			coinText.text = COINS + coins;
	}

	void SetUnitsBools(bool havePlayed){
		if(unidades != null){
			foreach(Unit unit in unidades){
				unit.haveMoved = havePlayed;
			}
		}
	}

	void OnEnable(){
		SetUnitsBools(false);	
		GameController.OnEndTurn += EndTurn;
	}

	void OnDisable(){
		SetUnitsBools(true);
		GameController.OnEndTurn -= EndTurn;
	}

	void EndTurn(){
		if(OnTowerSelect != null){
			OnTowerSelect = null;
		}
		if(OnTileSelect != null){
			OnTileSelect = null;
		}
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.C)){
			if(OnTowerSelect!=null){
				OnTowerSelect();
			}
		}
		if(Input.GetKeyDown(KeyCode.X)){
			if(OnTileSelect!=null){
				OnTileSelect(transform.position);
			}
		}
	}
}
