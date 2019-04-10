using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralMap : MonoBehaviour {

	public GameObject NodePrefab;
	public GameObject TerrainHex;
	public GameObject BadHexPrefab;
	public GameObject BorderHex;

	int width  = StaticVariables.gridWidth;
	int height = StaticVariables.gridHeight;

	int iCoord;
	int radiusSize;
	float baseHeight = -0.45f;
	public int radius;

	List<float> xNodeList = new List<float>();
	List<float> yNodeList = new List<float>();


	void Awake () {
			
		GenerateMapData ();
		GenerateMapVisuals ();

	}

	void GenerateMapData(){

		// Placeholder on the map
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{

				float xPos = x * StaticVariables.xOffset;
				float yPos = y * StaticVariables.yOffset;

				// if we are on a odd row y axis, then offset x by xOffset
				if (y != 0)
				{
					xPos += ((StaticVariables.xOffset / 2f) * y);
				}

				//GameObject hex_go = (GameObject)Instantiate (NodePrefab, new Vector3 (xPos, -1f, yPos), Quaternion.identity);

				// Name each hexagon something sensible and display
				// hex_go.name = "Hex_" + x + "_" + y;
				// Debug.Log (hex_go.name);

				// Create all the hexagons under a parent in the heirarchy
				//hex_go.transform.SetParent(this.transform);
		
				xNodeList.Add (xPos);
				yNodeList.Add (yPos);
		
				//hex_go.isStatic = true; // ????

				//Debug.Log (xNodeList [x] + " : " + yNodeList [y]);
				//Debug.Log("Hex Name: " + hex_go.name);

			}
		} 
	}

		 
	void GenerateMapVisuals(){
		
		int centreHexCoordArray = xNodeList.Count / 2; // centre hexagons coordinates are in the array at this extent
		//int yCentre = yNodeList.Count / 2;

		radiusSize = radius;

		for (int x = 1; x <= radiusSize; x++)
		{
			for (int y = 1; y <= radiusSize; y++)
			{
				// centre
				iCoord = centreHexCoordArray; 
				Instantiate (TerrainHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

				// bottom left
				if (x + y <= radiusSize) { 
					iCoord = centreHexCoordArray - (x*height) - y; 
					Instantiate (TerrainHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
				} 

				if (x > y) { 
					// bottom right
					iCoord = centreHexCoordArray + (x*height) - y; 
					Instantiate (TerrainHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

					// top left
					iCoord = centreHexCoordArray - (x*height) + y; 
					Instantiate (TerrainHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
				}

				if (x < y) {
					// top right
					iCoord = centreHexCoordArray + (x*height) + (y - x); 
					Instantiate (TerrainHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

					// top middle
					iCoord = centreHexCoordArray - (x*height) + y; 
					Instantiate (TerrainHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

					// bottom middle
					iCoord = centreHexCoordArray + (x*height) - y; 
					Instantiate (TerrainHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
				} 

				// top right
				iCoord = centreHexCoordArray + y; 
				Instantiate (TerrainHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);	

				// bottom left
				iCoord = centreHexCoordArray - y; 
				Instantiate (TerrainHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

				// left
				iCoord = centreHexCoordArray - x*height; 
				Instantiate (TerrainHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

				// right
				iCoord = centreHexCoordArray + x*height; 
				Instantiate (TerrainHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

				if(x*x == y*y){
					// top left
					iCoord = centreHexCoordArray - x*height + y; 
					Instantiate (TerrainHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

					// botom right
					iCoord = centreHexCoordArray + x*height - y; 
					Instantiate (TerrainHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
				}
			}
		}

		// Border Unwalkables
		if(radius == 0)
		{
			iCoord = centreHexCoordArray - 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - height; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - height + 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + height; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + height - 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
		}

		if (radius == 1)
		{
			//top left
			iCoord = centreHexCoordArray - (2*height); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (2*height) + 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (2*height) + 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//top right
			iCoord = centreHexCoordArray - height + 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + height + 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//bottom right
			iCoord = centreHexCoordArray + (2*height); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (2*height) - 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (2*height) - 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//bottom left
			iCoord = centreHexCoordArray + height - 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - height - 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
		}

		if (radius == 2)
		{
			//top left
			iCoord = centreHexCoordArray - (3*height); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (3*height) + 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (3*height) + 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (3*height) + 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//top right
			iCoord = centreHexCoordArray - (2*height) + 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - height + 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + height + 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (2*height) + 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//bottom right
			iCoord = centreHexCoordArray + (3*height); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (3*height) - 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (3*height) - 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (3*height) - 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//bottom left
			iCoord = centreHexCoordArray + (2*height) - 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + height - 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - height - 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (2*height) - 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
		}

		if(radius == 3)
		{
			//top left
			iCoord = centreHexCoordArray - (4*height); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (4*height) + 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (4*height) + 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (4*height) + 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (4*height) + 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//top right
			iCoord = centreHexCoordArray - (3*height) + 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (2*height) + 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (height) + 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + height + 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (2*height) + 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (3*height) + 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (4*height); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//bottom right
			iCoord = centreHexCoordArray + (4*height); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (4*height) - 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (4*height) - 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (4*height) - 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (4*height) - 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//bottom left
			iCoord = centreHexCoordArray + (3*height) - 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (2*height) - 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + height - 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - height - 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (2*height) - 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);	
			iCoord = centreHexCoordArray - (3*height) - 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

		}

		if (radius == 4)
		{
			//top left
			iCoord = centreHexCoordArray - (5*height); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (5*height) + 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (5*height) + 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (5*height) + 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (5*height) + 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (5*height) + 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//top right
			iCoord = centreHexCoordArray - (4*height) + 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (3*height) + 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (2*height) + 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (height) + 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + height + 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (2*height) + 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (3*height) + 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (4*height) + 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//bottom right
			iCoord = centreHexCoordArray + (5*height); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (5*height) - 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (5*height) - 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (5*height) - 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (5*height) - 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (5*height) - 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//bottom left
			iCoord = centreHexCoordArray + (4*height) - 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (3*height) - 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (2*height) - 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (height) - 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - height - 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (2*height) - 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);	
			iCoord = centreHexCoordArray - (3*height) - 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (4*height) - 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

		}

		if (radius == 5)
		{
			//top left
			iCoord = centreHexCoordArray - (6*height); 
			Instantiate (TerrainHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (6*height) + 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (6*height) + 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (6*height) + 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (6*height) + 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (6*height) + 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (6*height) + 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//top right
			iCoord = centreHexCoordArray - (5*height) + 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (4*height) + 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (3*height) + 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (2*height) + 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (height) + 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + height + 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (2*height) + 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (3*height) + 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (4*height) + 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (5*height) + 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//bottom right
			iCoord = centreHexCoordArray + (6*height); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (6*height) - 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (6*height) - 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (6*height) - 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (6*height) - 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (6*height) - 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (6*height) - 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//bottom left
			iCoord = centreHexCoordArray + (5*height) - 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (4*height) - 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (3*height) - 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (2*height) - 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (height) - 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - height - 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (2*height) - 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);	
			iCoord = centreHexCoordArray - (3*height) - 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (4*height) - 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (5*height) - 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

		}

		if (radius == 6)
		{
			//top left
			iCoord = centreHexCoordArray - (7*height); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (7*height) + 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (7*height) + 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (7*height) + 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (7*height) + 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (7*height) + 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (7*height) + 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (7*height) + 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//top right
			iCoord = centreHexCoordArray - (6*height) + 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (5*height) + 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (4*height) + 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (3*height) + 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (2*height) + 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (height) + 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + height + 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (2*height) + 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (3*height) + 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (4*height) + 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (5*height) + 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (6*height) + 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//bottom right
			iCoord = centreHexCoordArray + (7*height); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (7*height) - 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (7*height) - 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (7*height) - 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (7*height) - 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (7*height) - 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (7*height) - 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (7*height) - 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//bottom left
			iCoord = centreHexCoordArray + (6*height) - 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (5*height) - 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (4*height) - 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (3*height) - 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (2*height) - 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (height) - 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - height - 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (2*height) - 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);	
			iCoord = centreHexCoordArray - (3*height) - 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (4*height) - 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (5*height) - 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (6*height) - 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

		}

		if (radius == 7)
		{
			//top left
			iCoord = centreHexCoordArray - (8*height); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (8*height) + 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (8*height) + 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (8*height) + 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (8*height) + 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (8*height) + 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (8*height) + 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (8*height) + 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (8*height) + 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//top right
			iCoord = centreHexCoordArray - (7*height) + 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (6*height) + 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (5*height) + 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (4*height) + 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (3*height) + 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (2*height) + 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (height) + 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + height + 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (2*height) + 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (3*height) + 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (4*height) + 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (5*height) + 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (6*height) + 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (7*height) + 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//bottom right
			iCoord = centreHexCoordArray + (8*height); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (8*height) - 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (8*height) - 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (8*height) - 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (8*height) - 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (8*height) - 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (8*height) - 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (8*height) - 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (8*height) - 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//bottom left
			iCoord = centreHexCoordArray + (7*height) - 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (6*height) - 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (5*height) - 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (4*height) - 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (3*height) - 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (2*height) - 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (height) - 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - height - 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (2*height) - 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);	
			iCoord = centreHexCoordArray - (3*height) - 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (4*height) - 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (5*height) - 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (6*height) - 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (7*height) - 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

		}

		if (radius == 8)
		{
			//top left
			iCoord = centreHexCoordArray - (9*height); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (9*height) + 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (9*height) + 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (9*height) + 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (9*height) + 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (9*height) + 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (9*height) + 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (9*height) + 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (9*height) + 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (9*height) + 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//top right
			iCoord = centreHexCoordArray - (8*height) + 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (7*height) + 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (6*height) + 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (5*height) + 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (4*height) + 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (3*height) + 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (2*height) + 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (height) + 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + height + 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (2*height) + 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (3*height) + 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (4*height) + 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (5*height) + 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (6*height) + 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (7*height) + 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (8*height) + 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//bottom right
			iCoord = centreHexCoordArray + (9*height); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (9*height) - 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (9*height) - 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (9*height) - 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (9*height) - 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (9*height) - 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (9*height) - 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (9*height) - 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (9*height) - 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (9*height) - 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//bottom left
			iCoord = centreHexCoordArray + (8*height) - 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (7*height) - 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (6*height) - 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (5*height) - 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (4*height) - 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (3*height) - 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (2*height) - 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (height) - 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - height - 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (2*height) - 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);	
			iCoord = centreHexCoordArray - (3*height) - 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (4*height) - 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (5*height) - 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (6*height) - 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (7*height) - 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (8*height) - 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

		} 

		if (radius == 9)
		{
			//top left
			iCoord = centreHexCoordArray - (10*height); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (10*height) + 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (10*height) + 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (10*height) + 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (10*height) + 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (10*height) + 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (10*height) + 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (10*height) + 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (10*height) + 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (10*height) + 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (10*height) + 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//top right
			iCoord = centreHexCoordArray - (9*height) + 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (8*height) + 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (7*height) + 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (6*height) + 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (5*height) + 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (4*height) + 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (3*height) + 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (2*height) + 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (height) + 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + height + 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (2*height) + 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (3*height) + 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (4*height) + 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (5*height) + 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (6*height) + 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (7*height) + 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (8*height) + 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (9*height) + 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//bottom right
			iCoord = centreHexCoordArray + (10*height); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (10*height) - 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (10*height) - 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (10*height) - 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (10*height) - 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (10*height) - 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (10*height) - 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (10*height) - 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (10*height) - 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (10*height) - 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (10*height) - 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//bottom left
			iCoord = centreHexCoordArray + (9*height) - 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (8*height) - 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (7*height) - 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (6*height) - 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (5*height) - 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (4*height) - 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (3*height) - 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (2*height) - 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (height) - 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - height - 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (2*height) - 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);	
			iCoord = centreHexCoordArray - (3*height) - 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (4*height) - 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (5*height) - 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (6*height) - 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (7*height) - 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (8*height) - 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (9*height) - 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
		} 

		if (radius == 10)
		{
			radiusSize = 11;

			//top left
			iCoord = centreHexCoordArray - (radiusSize*height); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 11; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//top right
			iCoord = centreHexCoordArray - (10*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (9*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (8*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (7*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (6*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (5*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (4*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (3*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (2*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + height + radiusSize - 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (2*height) + radiusSize - 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (3*height) + radiusSize - 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (4*height) + radiusSize - 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (5*height) + radiusSize - 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (6*height) + radiusSize - 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (7*height) + radiusSize - 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (8*height) + radiusSize - 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (9*height) + radiusSize - 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (10*height) + radiusSize - 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//bottom right
			iCoord = centreHexCoordArray + (radiusSize*height); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 11; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//bottom left
			iCoord = centreHexCoordArray + (10*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (9*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (8*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (7*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (6*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (5*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (4*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (3*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (2*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - height - 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (2*height) - 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);	
			iCoord = centreHexCoordArray - (3*height) - 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (4*height) - 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (5*height) - 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (6*height) - 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (7*height) - 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (8*height) - 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (9*height) - 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (10*height) - 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
		} 

		if (radius == 11)
		{
			//top left
			iCoord = centreHexCoordArray - (12*height); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (12*height) + 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (12*height) + 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (12*height) + 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (12*height) + 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (12*height) + 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (12*height) + 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (12*height) + 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (12*height) + 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (12*height) + 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (12*height) + 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (12*height) + 11; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (12*height) + 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//top right
			iCoord = centreHexCoordArray - (11*height) + 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (10*height) + 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (9*height) + 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (8*height) + 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (7*height) + 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (6*height) + 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (5*height) + 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (4*height) + 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (3*height) + 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (2*height) + 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (height) + 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + height + 11; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (2*height) + 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (3*height) + 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (4*height) + 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (5*height) + 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (6*height) + 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (7*height) + 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (8*height) + 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (9*height) + 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (10*height) + 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (11*height) + 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//bottom right
			iCoord = centreHexCoordArray + (12*height); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (12*height) - 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (12*height) - 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (12*height) - 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (12*height) - 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (12*height) - 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (12*height) - 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (12*height) - 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (12*height) - 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (12*height) - 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (12*height) - 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (12*height) - 11; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (12*height) - 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//bottom left
			iCoord = centreHexCoordArray + (11*height) - 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (10*height) - 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (9*height) - 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (8*height) - 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (7*height) - 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (6*height) - 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (5*height) - 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (4*height) - 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (3*height) - 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (2*height) - 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (height) - 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - height - 11; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (2*height) - 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);	
			iCoord = centreHexCoordArray - (3*height) - 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (4*height) - 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (5*height) - 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (6*height) - 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (7*height) - 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (8*height) - 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (9*height) - 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (10*height) - 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (11*height) - 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
		} 

		if (radius == 12)
		{
			//top left
			iCoord = centreHexCoordArray - (13*height); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (13*height) + 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (13*height) + 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (13*height) + 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (13*height) + 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (13*height) + 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (13*height) + 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (13*height) + 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (13*height) + 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (13*height) + 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (13*height) + 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (13*height) + 11; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (13*height) + 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (13*height) + 13; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//top right
			iCoord = centreHexCoordArray - (12*height) + 13; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (11*height) + 13; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (10*height) + 13; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (9*height) + 13; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (8*height) + 13; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (7*height) + 13; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (6*height) + 13; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (5*height) + 13; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (4*height) + 13; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (3*height) + 13; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (2*height) + 13; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (height) + 13; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + 13; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + height + 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (2*height) + 11; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (3*height) + 10;
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (4*height) + 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (5*height) + 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (6*height) + 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (7*height) + 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (8*height) + 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (9*height) + 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (10*height) + 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (11*height) + 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (12*height) + 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//bottom right
			iCoord = centreHexCoordArray + (13*height); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (13*height) - 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (13*height) - 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (13*height) - 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (13*height) - 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (13*height) - 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (13*height) - 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (13*height) - 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (13*height) - 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (13*height) - 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (13*height) - 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (13*height) - 11; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (13*height) - 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (13*height) - 13; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//bottom left
			iCoord = centreHexCoordArray + (12*height) - 13; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (11*height) - 13; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (10*height) - 13; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (9*height) - 13; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (8*height) - 13; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (7*height) - 13; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (6*height) - 13; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (5*height) - 13; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (4*height) - 13; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (3*height) - 13; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (2*height) - 13; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (height) - 13; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - 13; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - height - 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (2*height) - 11; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);	
			iCoord = centreHexCoordArray - (3*height) - 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (4*height) - 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (5*height) - 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (6*height) - 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (7*height) - 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (8*height) - 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (9*height) - 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (10*height) - 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (11*height) - 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (12*height) - 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
		} 

		if (radius == 13)
		{
			radiusSize = 14;

			//top left
			iCoord = centreHexCoordArray - (radiusSize*height); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 11; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 13; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 14; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//top right
			iCoord = centreHexCoordArray - (13*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (12*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (11*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (10*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (9*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (8*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (7*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (6*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (5*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (4*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (3*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (2*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + height + (radiusSize - 1); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (2*height) + (radiusSize - 2); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (3*height) + (radiusSize - 3);
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (4*height) + (radiusSize - 4); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (5*height) + (radiusSize - 5); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (6*height) + (radiusSize - 6); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (7*height) + (radiusSize - 7); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (8*height) + (radiusSize - 8); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (9*height) + (radiusSize - 9); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (10*height) + (radiusSize - 10); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (11*height) + (radiusSize - 11); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (12*height) + (radiusSize - 12); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (13*height) + (radiusSize - 13); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//bottom right
			iCoord = centreHexCoordArray + (radiusSize*height); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 11; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 13; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 14; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//bottom left
			iCoord = centreHexCoordArray + (13*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (12*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (11*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (10*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (9*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (8*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (7*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (6*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (5*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (4*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (3*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (2*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - height - (radiusSize - 1); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (2*height) - (radiusSize - 2); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);	
			iCoord = centreHexCoordArray - (3*height) - (radiusSize - 3); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (4*height) - (radiusSize - 4); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (5*height) - (radiusSize - 5); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (6*height) - (radiusSize - 6); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (7*height) - (radiusSize - 7); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (8*height) - (radiusSize - 8); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (9*height) - (radiusSize - 9); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (10*height) - (radiusSize - 10); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (11*height) - (radiusSize - 11); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (12*height) - (radiusSize - 12); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (13*height) - (radiusSize - 13); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
		}

		if (radius == 14)
		{
			radiusSize = 15;

			//top left
			iCoord = centreHexCoordArray - (radiusSize*height); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 11; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 13; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 14; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 15; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//top right
			iCoord = centreHexCoordArray - (14*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (13*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (12*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (11*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (10*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (9*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (8*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (7*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (6*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (5*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (4*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (3*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (2*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + height + (radiusSize - 1); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (2*height) + (radiusSize - 2); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (3*height) + (radiusSize - 3);
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (4*height) + (radiusSize - 4); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (5*height) + (radiusSize - 5); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (6*height) + (radiusSize - 6); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (7*height) + (radiusSize - 7); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (8*height) + (radiusSize - 8); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (9*height) + (radiusSize - 9); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (10*height) + (radiusSize - 10); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (11*height) + (radiusSize - 11); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (12*height) + (radiusSize - 12); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (13*height) + (radiusSize - 13); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (14*height) + (radiusSize - 14); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//bottom right
			iCoord = centreHexCoordArray + (radiusSize*height); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 11; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 13; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 14; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 15; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//bottom left
			iCoord = centreHexCoordArray + (14*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (13*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (12*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (11*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (10*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (9*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (8*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (7*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (6*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (5*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (4*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (3*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (2*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - height - (radiusSize - 1); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (2*height) - (radiusSize - 2); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);	
			iCoord = centreHexCoordArray - (3*height) - (radiusSize - 3); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (4*height) - (radiusSize - 4); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (5*height) - (radiusSize - 5); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (6*height) - (radiusSize - 6); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (7*height) - (radiusSize - 7); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (8*height) - (radiusSize - 8); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (9*height) - (radiusSize - 9); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (10*height) - (radiusSize - 10); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (11*height) - (radiusSize - 11); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (12*height) - (radiusSize - 12); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (13*height) - (radiusSize - 13); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (14*height) - (radiusSize - 14); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
		}

		if (radius == 15)
		{
			radiusSize = 16;

			//top left
			iCoord = centreHexCoordArray - (radiusSize*height); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 11; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 13; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 14; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 15; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 16; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//top right
			iCoord = centreHexCoordArray - (15*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (14*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (13*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (12*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (11*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (10*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (9*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (8*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (7*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (6*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (5*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (4*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (3*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (2*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + height + (radiusSize - 1); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (2*height) + (radiusSize - 2); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (3*height) + (radiusSize - 3);
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (4*height) + (radiusSize - 4); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (5*height) + (radiusSize - 5); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (6*height) + (radiusSize - 6); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (7*height) + (radiusSize - 7); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (8*height) + (radiusSize - 8); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (9*height) + (radiusSize - 9); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (10*height) + (radiusSize - 10); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (11*height) + (radiusSize - 11); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (12*height) + (radiusSize - 12); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (13*height) + (radiusSize - 13); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (14*height) + (radiusSize - 14); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (15*height) + (radiusSize - 15); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//bottom right
			iCoord = centreHexCoordArray + (radiusSize*height); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 11; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 13; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 14; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 15; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 16; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//bottom left
			iCoord = centreHexCoordArray + (15*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (14*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (13*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (12*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (11*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (10*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (9*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (8*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (7*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (6*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (5*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (4*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (3*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (2*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - height - (radiusSize - 1); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (2*height) - (radiusSize - 2); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);	
			iCoord = centreHexCoordArray - (3*height) - (radiusSize - 3); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (4*height) - (radiusSize - 4); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (5*height) - (radiusSize - 5); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (6*height) - (radiusSize - 6); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (7*height) - (radiusSize - 7); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (8*height) - (radiusSize - 8); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (9*height) - (radiusSize - 9); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (10*height) - (radiusSize - 10); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (11*height) - (radiusSize - 11); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (12*height) - (radiusSize - 12); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (13*height) - (radiusSize - 13); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (14*height) - (radiusSize - 14); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (15*height) - (radiusSize - 15); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
		}

		if (radius == 16)
		{
			radiusSize = 17;	
			
			//top left
			iCoord = centreHexCoordArray - (radiusSize*height); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 11; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 13; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 14; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 15; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 16; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (radiusSize*height) + 17; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//top right
			iCoord = centreHexCoordArray - (16*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (15*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (14*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (13*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (12*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (11*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (10*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (9*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (8*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (7*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (6*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (5*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (4*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (3*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (2*height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (height) + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + height + (radiusSize - 1); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (2*height) + (radiusSize - 2); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (3*height) + (radiusSize - 3);
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (4*height) + (radiusSize - 4); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (5*height) + (radiusSize - 5); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (6*height) + (radiusSize - 6); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (7*height) + (radiusSize - 7); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (8*height) + (radiusSize - 8); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (9*height) + (radiusSize - 9); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (10*height) + (radiusSize - 10); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (11*height) + (radiusSize - 11); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (12*height) + (radiusSize - 12); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (13*height) + (radiusSize - 13); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (14*height) + (radiusSize - 14); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (15*height) + (radiusSize - 15); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (16*height) + (radiusSize - 16); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//bottom right
			iCoord = centreHexCoordArray + (radiusSize*height); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 1; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 2; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 3; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 4; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 5; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 6; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 7; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 8; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 9; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 10; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 11; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 12; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 13; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 14; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 15; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 16; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (radiusSize*height) - 17; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);

			//bottom left
			iCoord = centreHexCoordArray + (16*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (15*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (14*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (13*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (12*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (11*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (10*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (9*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (8*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (7*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (6*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (5*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (4*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (3*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (2*height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray + (height) - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - radiusSize; 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - height - (radiusSize - 1); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (2*height) - (radiusSize - 2); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);	
			iCoord = centreHexCoordArray - (3*height) - (radiusSize - 3); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (4*height) - (radiusSize - 4); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (5*height) - (radiusSize - 5); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (6*height) - (radiusSize - 6); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (7*height) - (radiusSize - 7); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (8*height) - (radiusSize - 8); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (9*height) - (radiusSize - 9); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (10*height) - (radiusSize - 10); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (11*height) - (radiusSize - 11); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (12*height) - (radiusSize - 12); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (13*height) - (radiusSize - 13); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (14*height) - (radiusSize - 14); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (15*height) - (radiusSize - 15); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
			iCoord = centreHexCoordArray - (16*height) - (radiusSize - 16); 
			Instantiate (BorderHex, new Vector3 (xNodeList [iCoord], baseHeight, yNodeList [iCoord]), Quaternion.identity);
		} 
	}
}





/* some thought needed
//5 mid
	    int startRowSize = 10;

	    for (int i = 0; i < startRowSize; i++)
	    {
	        GameObject newHex = HexCreator();
			hexWidth = newHex.gameObject.GetComponent<Renderer>().bounds.size.z;
	        newHex.transform.position = new Vector3(i * hexWidth, 0, 0);
	    }

	    for (int row = 0; row < startRowSize; row++) {
	       for (int i = 0; i < startRowSize-1-row; i++)
	       {
	           GameObject newHex = HexCreator();

	           if (row == 0)
	              newHex.transform.position = new Vector3(i*hexWidth + hexWidth/2, 0, -(hexWidth/1.17f));

               else
	              newHex.transform.position = new Vector3(i*hexWidth + ((row+1)*(hexWidth/2)), 0,(row+1)*-(hexWidth/1.17f));
	       }
	    }

	    for (int row = 0; row < startRowSize; row++) {
			for (int i = 0; i < startRowSize-1-row; i++)
		    {
		        GameObject newHex = HexCreator();

		        if (row == 0)
		            newHex.transform.position = new Vector3(i*hexWidth+ hexWidth/2, 0, hexWidth/1.17f);

		        else
		            newHex.transform.position = new Vector3(i*hexWidth + ((row+1)*(hexWidth/2)), 0, (row+1)*(hexWidth/1.17f));
		    }
		}

	}


	private GameObject HexCreator()
	{
		GameObject newHex = (GameObject)Instantiate(BorderHex);
	    newHex.transform.Rotate(new Vector3(0,30,0));

	    return newHex;
	}
*/