using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TileMap : MonoBehaviour {

	private byte[,] map;
	private Image[,] tilesMap;
	private bool[,] unitsMap;

	private string fileName = "tileMap.txt";

	[SerializeField] private Color rangeColor, noColor;
	[SerializeField] private GameObject[] marks;
	[SerializeField] private GameObject[] tilePrefabs;
	[SerializeField] private Material[] materials;
	[SerializeField] private int mapWidth = 16, mapHeight = 10;

	enum Tiles : byte{ land=0, forrest, mountain, water, tower }	


	void Awake(){


		StreamReader f = new StreamReader(fileName);

		mapWidth  = int.Parse(f.ReadLine());
		mapHeight = int.Parse(f.ReadLine());

		map = new byte[mapWidth, mapHeight];
		tilesMap = new Image[mapWidth, mapHeight];
		unitsMap = new bool[mapWidth, mapHeight];

		GameObject[] players = new GameObject[2];

		players[0] = Instantiate(marks[0], Vector3.zero, Quaternion.identity) as GameObject;
		players[1] = Instantiate(marks[1], new Vector3(mapWidth-1, mapHeight-1, 0), Quaternion.identity) as GameObject;

		GetComponent<GameController>().InitPlayers(players[0], players[1]);

		for(int j = mapHeight-1; j >= 0; j--){
			for (int i = 0; i < mapWidth; i++){
				byte t =  (byte)(f.Read() - '0');
				if(t > 4){
					map[i,j] = (byte) Tiles.tower;
					GameObject tower = (Instantiate(tilePrefabs[map[i,j]], new Vector3(i,j,0),Quaternion.identity) as GameObject); 
					tower.GetComponent<Tower>().m = players[t-5].GetComponent<Mark>();
					tilesMap[i, j] = tower.GetComponentsInChildren<Image>()[1];
				}else{
					map[i,j] = t;
					tilesMap[i, j] = (Instantiate(tilePrefabs[map[i,j]], new Vector3(i,j,0),Quaternion.identity) as GameObject).GetComponentInChildren<Image>();
				}

			}
			f.ReadLine();// pula o '\n'
		}
		
		f.Close();

	}

	public bool IsValid(Vector3 position){
		return (position.x >= 0) && (position.y >= 0) &&
			(position.x < mapWidth) && (position.y < mapHeight);
	}

	bool IsInMap(int x, int y){
		return x>=0 && y>=0 && x<map.GetLength(0) && y<map.GetLength(1);
	}

	Material GetMaterial(string tag){
		Material material = null;

		switch(tag){
		case "Forrest":
			material = materials[(int) Tiles.forrest];
			break;
		case "Land":
			material = materials[(int) Tiles.land];
			break;
		case "Water":
			material = materials[(int) Tiles.water];
			break;
		case "Mountain":
			material = materials[(int) Tiles.mountain];
			break;
		case "Tower":
			material = materials[(int) Tiles.tower];
			break;
		}
			
		return material;
	}

	public void DrawRange(Vector3 position, int range, Color rangeColor){
		int x = (int) position.x, y = (int) position.y;

		for(int i = x-range; i <= x+range; i++)
			for(int j = y - range + Mathf.Abs(x-i); j <= y + range - Mathf.Abs(x-i); j++)
				if(IsInMap(i,j)){
					tilesMap[i,j].color = rangeColor;
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
