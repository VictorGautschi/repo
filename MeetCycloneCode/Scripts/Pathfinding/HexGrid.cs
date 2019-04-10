using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HexGrid : MonoBehaviour {

	[Header("Testing")]
	public bool displayGridGizmos;

	[Header("Terrain Weights")]
	public LayerMask unwalkableMask;
	public TerrainType[] walkableRegions;

	LayerMask walkableMask;
	Dictionary<int, int> walkableRegionsDictionary = new Dictionary<int, int>();

	private float nodeRadius = 0.5f; 
	Vector2 gridWorldSize = new Vector2(StaticVariables.gridWidth,StaticVariables.gridHeight);

	Node[,] grid;

	float nodeDiameter;
	int gridSizeX, gridSizeY;

	void Awake(){
		nodeDiameter = nodeRadius*2;
		gridSizeX = Mathf.RoundToInt((gridWorldSize.x)/nodeDiameter);
		gridSizeY = Mathf.RoundToInt((gridWorldSize.y)/nodeDiameter);

		foreach(TerrainType region in walkableRegions) {
			walkableMask.value = walkableMask |= region.terrainMask.value; // |= means bitwise equals 
			walkableRegionsDictionary.Add((int)Mathf.Log(region.terrainMask.value,2),region.terrainPenalty);
		}

		CreateGrid();
	}

	public int MaxSize {
		get{
			return gridSizeX * gridSizeY;
		}
	}

	void CreateGrid(){
		grid = new Node[gridSizeX,gridSizeY];

		for(int x = 0; x < gridSizeX; x++){
			for(int y = 0; y < gridSizeY; y++){

				float xPos = x * StaticVariables.xOffset;
				float yPos = y * StaticVariables.yOffset;

				if (y != 0)
				{
					xPos += ((StaticVariables.xOffset / 2f) * y);
				}

				Vector3 worldPoint = Vector3.right * (xPos * nodeDiameter) + Vector3.forward * ( yPos * nodeDiameter);
				bool walkable = !(Physics.CheckSphere(worldPoint,(nodeRadius - 0.05f),unwalkableMask)); // 0.05f below is to stop the hexagons around being picked up by the unwalkable layer mask

				int movementPenalty = 0;

				if (walkable) {
					Ray ray = new Ray(worldPoint + Vector3.up * 50, Vector3.down);
					RaycastHit hit;
					if (Physics.Raycast(ray,out hit, 100, walkableMask)){
						walkableRegionsDictionary.TryGetValue(hit.collider.gameObject.layer, out movementPenalty);
					}
				}

				grid[x,y] = new Node(walkable, worldPoint, x, y, movementPenalty);
			}
		}
	}

	public List<Node> GetNeighbours(Node node){
		List<Node> neighbours = new List<Node>();

		for(int x = -1; x <= 1; x++){
			for(int y = -1; y <= 1; y++){
				if(x == 0 && y == 0)	// ignores the current node. I.e. don't add the current node into the set
					continue;
			 	if(x == -1 && y == -1)  // must ignore this as its neighbour
					continue;
				if(x == 1 && y == 1)    // must ignore this as its neighbour
					continue;
			
				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY){
					neighbours.Add(grid[checkX,checkY]);
				}
			}
		}
		return neighbours;
	}

	 public Node GetNodeFromWorldPoint(Vector3 worldPosition){
		
		float percentX = worldPosition.x / (Mathf.RoundToInt((gridWorldSize.x)/nodeDiameter) * StaticVariables.xOffset);
		float percentY = worldPosition.z / (Mathf.RoundToInt((gridWorldSize.y)/nodeDiameter) * StaticVariables.yOffset); // world position is z because of world space y is up and down, grid x and y because Vector2

		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		int x = Mathf.RoundToInt(((gridSizeX) * percentX));
		int y = Mathf.RoundToInt(((gridSizeY) * percentY));

		if (y > 0){
			percentX = (worldPosition.x - ((StaticVariables.xOffset/2f)*y)) / (Mathf.RoundToInt((gridWorldSize.x)/nodeDiameter) * StaticVariables.xOffset);
			percentX = Mathf.Clamp01(percentX);
			x = Mathf.RoundToInt(((gridSizeX) * percentX));
		}

		// Clamp x not to go above the gridWorldSize.x - x starts at 0, not 1
		if(x >= (gridWorldSize.x)){
			x = (int)(gridWorldSize.x - 1);
		}  

		// Clamp y not to go above the gridWorldSize.y - y starts at 0, not 1
		if(y >= (gridWorldSize.y)){
			y = (int)(gridWorldSize.y - 1);
		}

		return grid[x,y]; //node position on then grid - need to return the grid where the nodes are

	}

	// Just for show
	void OnDrawGizmos(){
		if (grid != null && displayGridGizmos) {
			foreach(Node n in grid) {
				Gizmos.color = (n.walkable) ? Color.white : Color.red;
				Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.9f));
			}
		}
	}

	[System.Serializable]
	public class TerrainType {
		public LayerMask terrainMask;
		public int terrainPenalty;
	}
}