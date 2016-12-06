using System.IO;
using UnityEngine;
using System.Collections;

public class TileMap : MonoBehaviour {

	private byte[,] map;
	private MeshRenderer[,] tilesMap;

	private string fileName = "tileMap.txt";

	[SerializeField] private Color rangeColor;
	[SerializeField] private GameObject[] marks;
	[SerializeField] private GameObject[] tilePrefabs;
	[SerializeField] private Material[] materials;
	[SerializeField] private int mapWidth = 16, mapHeight = 10;

	enum Tiles : byte{ land=0, forrest, mountain, water}	


	void Awake(){
		
		StreamReader f = new StreamReader(fileName);

		mapWidth  = int.Parse(f.ReadLine());
		mapHeight = int.Parse(f.ReadLine());

		map = new byte[mapWidth, mapHeight];
		tilesMap  = new MeshRenderer[mapWidth, mapHeight];

		for(int j = mapHeight-1; j >= 0; j--){
			for (int i = 0; i < mapWidth; i++){
				map[i,j] = (byte)(f.Read() - '0');
				tilesMap[i, j] = (Instantiate(tilePrefabs[map[i,j]], new Vector3(i,j,0),Quaternion.identity) as GameObject).GetComponent<MeshRenderer>();
			}
			f.ReadLine();// pula o '\n'
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
		}
			
		return material;
	}

	public void DrawRange(Vector3 position, int range){
		int x = (int) position.x, y = (int) position.y;

		for(int i = x-range; i <= x+range; i++)
			for(int j = y - range + Mathf.Abs(x-i); j <= y + range - Mathf.Abs(x-i); j++)
				if(IsInMap(i,j)){
					tilesMap[i,j].material.color = rangeColor;
				}
	}


	public void UnDrawRange(Vector3 position, int range){
		int x = (int) position.x, y = (int) position.y;

		for(int i = x-range; i <= x+range; i++)
			for(int j = y - range + Mathf.Abs(x-i); j <= y + range - Mathf.Abs(x-i); j++)
				if(IsInMap(i,j)){
					tilesMap[i,j].material = GetMaterial(tilesMap[i,j].tag);
				}
	}
}
