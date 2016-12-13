using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TileMap : MonoBehaviour {

	private byte[,] map;
	private Image[,] tilesMap;
	private bool[,] unitsMap;

	private string fileName = "tileMap.txt";

	[SerializeField] private Color rangeColor, noColor, atkColor;
	[SerializeField] private GameObject[] marks;
	[SerializeField] private GameObject[] tilePrefabs;
	[SerializeField] private int mapWidth = 16, mapHeight = 10;

	enum Tiles : byte{ land=0, forrest, mountain, water, tower }	


	void Awake(){
		Debug.Log("awake");
		StreamReader f = new StreamReader(fileName);

		mapWidth  = int.Parse(f.ReadLine());
		mapHeight = int.Parse(f.ReadLine());

		map = new byte[mapWidth, mapHeight];
		tilesMap = new Image[mapWidth, mapHeight];
		unitsMap = new bool[mapWidth, mapHeight];

		GameObject[] players = new GameObject[2];

		players[0] = Instantiate(marks[0], Vector3.zero, Quaternion.identity) as GameObject;
		players[1] = Instantiate(marks[1], new Vector3(mapWidth-1, mapHeight-1, 0), Quaternion.identity) as GameObject;

		GameController gc = GetComponent<GameController>();

		gc.InitPlayers(players[0], players[1]);

		int towerCount = 0;

		for(int j = mapHeight-1; j >= 0; j--){
			for (int i = 0; i < mapWidth; i++){
				byte t =  (byte)(f.Read() - '0');
				if(t >= 4){
					map[i,j] = (byte) Tiles.tower;
					GameObject tower = (Instantiate(tilePrefabs[map[i,j]], new Vector3(i,j,0),Quaternion.identity) as GameObject); 
					if(t>4)
						tower.GetComponent<Tower>().m = players[t-5].GetComponent<Mark>();
					Image[] imgs = tower.GetComponentsInChildren<Image>();
					tilesMap[i, j] = (imgs[0].name == "Image") ? imgs[0] : imgs[1]; 
					towerCount++;
				}else{
					map[i,j] = t;
					tilesMap[i, j] = (Instantiate(tilePrefabs[map[i,j]], new Vector3(i,j,0),Quaternion.identity) as GameObject).GetComponentInChildren<Image>();
				}

			}
			f.ReadLine();// pula o '\n'
		}
		
		f.Close();
		gc.SetTowers(towerCount);
	}

	public bool IsValid(Vector3 position){
		return (position.x >= 0) && (position.y >= 0) &&
			(position.x < mapWidth) && (position.y < mapHeight);
	}

	bool IsInMap(int x, int y){
		return x>=0 && y>=0 && x<map.GetLength(0) && y<map.GetLength(1);
	}


	public void DrawRange(Vector3 position, int range, bool isAtk){
		int x = (int) position.x, y = (int) position.y;

		for(int i = x-range; i <= x+range; i++)
			for(int j = y - range + Mathf.Abs(x-i); j <= y + range - Mathf.Abs(x-i); j++)
				if(IsInMap(i,j)){
					tilesMap[i,j].color = isAtk? atkColor :rangeColor ;
				}
	}

	public bool IsInRange(Vector3 unitPosition, Vector3 tilePosition, int range){
		int dx = (int) Mathf.Abs(unitPosition.x - tilePosition.x), dy = (int) Mathf.Abs(unitPosition.y - tilePosition.y);
		return (dx+dy) <= range;
	}

	public void UnDrawRange(Vector3 position, int range){
		int x = (int) position.x, y = (int) position.y;

		for(int i = x-range; i <= x+range; i++)
			for(int j = y - range + Mathf.Abs(x-i); j <= y + range - Mathf.Abs(x-i); j++)
				if(IsInMap(i,j)){
					tilesMap[i,j].color = noColor;
				}
	}

	public void LockPosition(Vector3 position){
		unitsMap[(int)position.x, (int)position.y] = true;
	}

	public void UnLockPosition(Vector3 position){
		unitsMap[(int)position.x, (int)position.y] = false;
	}

	public bool IsLockedPosition(Vector3 position){
		return unitsMap[(int)position.x, (int)position.y];
	}

}
