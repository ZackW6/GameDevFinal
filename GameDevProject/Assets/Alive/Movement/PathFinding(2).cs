using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathFinding_2 : MonoBehaviour
{

    public Tilemap Scene;
    public Cell[,] World;
    public GameObject targetObject;
    private int xPos = -33;
    private int yPos = 21;

    void Start()
    {
        World = new Cell[Scene.size.x, Scene.size.y];
        for (int x = xPos; x < World.GetLength(0) + xPos; x++)
        {
            for (int y = yPos; y > -World.GetLength(1) + yPos; y--)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                TileBase tile = Scene.GetTile(pos);
                World[x - xPos, -(y) + yPos] = new Cell(x - xPos, -(y) + yPos);
            }
        }
    }

    public void Coordinate(){

    }

}
