using System.IO;
using UnityEngine;
using System.Collections;

public class TileMap : MonoBehaviour {

	private byte[,] map;
	private GameObject[,] tilesMap;

	private string fileName = "tileMap.txt";

	[SerializeField] private GameObject[] marks;
	[SerializeField] private GameObject[] tilePrefabs;
	[SerializeField] private int mapWidth = 16, mapHeight = 10;

	enum Tiles : byte{ land=0, forrest, water, mountain}	

	void Awake(){
		map = new byte[mapWidth, mapHeight];
		tilesMap = new GameObject[mapWidth, mapHeight];

		StreamReader f = new StreamReader(fileName);

		for (int i = 0; i < mapWidth; i++)
			for(int j = 0; j < mapHeight; j++){
				map[i,j] = (byte)(f.Read() - '0');
				tilesMap[i, j] = Instantiate(tilePrefabs[map[i,j]], new Vector3(i,j,0),Quaternion.identity) as GameObject;
			}
		
		f.Close();

		GetComponent<GameController>().InitPlayers(
			Instantiate(marks[0], Vector3.zero, Quaternion.identity) as GameObject,
			Instantiate(marks[1], new Vector3(mapWidth-1, mapHeight-1, 0), Quaternion.identity) as GameObject
		);
	}

	public bool IsValid(Vector3 position){
		return (position.x >= 0) && (position.y >= 0) &&
			(position.x < mapWidth) && (position.y < mapHeight);
	}
}
