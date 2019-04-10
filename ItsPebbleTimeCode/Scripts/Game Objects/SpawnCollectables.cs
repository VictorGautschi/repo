using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollectables : MonoBehaviour {
    
    public GameObject[] collectableTypes;

    /* Object Pooling attempt *************************************
    //public int pooledAmount = 500;
    //List<GameObject> collectables;

    //private void Awake()
    //{
    //    collectables = new List<GameObject>();
    //    collectables.Capacity = pooledAmount;
    //    for (int i = 0; i < pooledAmount; i++)
    //    {
    //        GameObject obj = Instantiate(collectableTypes[Random.Range(0, collectableTypes.Length)]);
    //        obj.SetActive(false);
    //        collectables.Add(obj);
    //    }

    //    Debug.Log(collectables.Capacity);
    //}
    ******************************************************/

    public void CreateGridStart(int gridSizeX, int gridSizeY, int xCoord, int yCoord)
    {
        for (int x = xCoord; x < gridSizeX + xCoord; x++)
        {
            for (int y = yCoord; y < gridSizeY + yCoord; y++)
            {
                /* Object Pooling attempt *************************************
                //for (int i = 0; i < collectables.Count; i++)
                //{
                //    if(!collectables[i].activeInHierarchy)
                //    {
                //        collectables[i].transform.position = new Vector2(x, y);
                //        collectables[i].transform.rotation = Quaternion.identity;
                //        collectables[i].SetActive(true);
                //        break;
                //    }
                //}
                *************************************************************/

                // The grid created at the start cannot have the collectables appear on the ball or directly under it

                if ((x == 0 && y == 0) || (x == 0 && y == 1) || (x == 0 && y == -1) || (x == 0 && y == -2) || (x == 0 && y == -3))
                {
                    // do nothing
                }
                else
                {
                    // create a collectable
                    Instantiate(collectableTypes[Random.Range(0, collectableTypes.Length)], new Vector2(x, y), Quaternion.identity);
                    //Gizmos.DrawCube(new Vector2(x, y), new Vector2(gridSizeX,gridSizeY)); 
                }
            }
        }
    }

    public void CreateGrid(int gridSizeX, int gridSizeY, int xCoord, int yCoord)
    {
        for (int x = xCoord; x < gridSizeX + xCoord; x++)
        {
            for (int y = yCoord; y < gridSizeY + yCoord; y++)
            {
                /* Object Pooling attempt *************************************
                //for (int i = 0; i < collectables.Count; i++)
                //{
                //    if(!collectables[i].activeInHierarchy)
                //    {
                //        collectables[i].transform.position = new Vector2(x, y);
                //        collectables[i].transform.rotation = Quaternion.identity;
                //        collectables[i].SetActive(true);
                //        break;
                //    }
                //}
                *************************************************************/
                
                Instantiate(collectableTypes[Random.Range(0, collectableTypes.Length)], new Vector2(x, y), Quaternion.identity);
                //Gizmos.DrawCube(new Vector2(x, y), new Vector2(gridSizeX,gridSizeY)); 
            }
        }
    }

    //void OnDrawGizmosSelected()
    //{
    //    // Draw a semitransparent blue cube at the transforms position
    //    Gizmos.color = new Color(1, 0, 0, 0.5f);
    //    Gizmos.DrawCube(new Vector2(gridSizeX/2, gridSizeY/2), new Vector2(gridSizeX,gridSizeY)); 
    //}
}
