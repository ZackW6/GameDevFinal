using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;


public class PathFinding2 : MonoBehaviour
{
    // Start is called before the first frame update


    public Tilemap Scene;
    public Cell[,] World;
    public GameObject targetObject;
    private int xPos = -33;
    private int yPos = 21;
    int scaleXlen = 10;
    int scaleYlen = 10;
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
        int playerPosX = (int)Mathf.Floor(targetObject.transform.position.x);
        int playerPosY = (int)Mathf.Ceil(targetObject.transform.position.y);

        Coordinate(playerPosX, playerPosY, 0);
    }
    List<int[]> cellsToVisit = new List<int[]>();
    public void Coordinate(int x, int y, int depth)
    {
        if (depth == 0)
        {
            int[] arr = new int[2];
            arr[0] = x;
            arr[1] = y;
            cellsToVisit.Add(arr);
            World[x, y].setVisited(true);
            World[x, y].setCost(0);
            Coordinate(cellsToVisit[0][0], cellsToVisit[0][1], depth + 1);
        }
        else if (cellsToVisit.Count > 0)
        {
            print("Loading: " + depth);
            updateCosts(x, y);
            Coordinate(cellsToVisit[0][0], cellsToVisit[0][1], depth + 1);
        }
        print("Finished: " + depth);
    }

    public void updateCosts(int x, int y)
    {
        List<int[]> neigbors = findNeightbors(x, y);
        for (var i = 0; i < neigbors.Count; i++)
        {
            cellsToVisit.Add(neigbors[i]);
            World[neigbors[i][0], neigbors[i][1]].setCost(World[x, y].getCost());
            World[neigbors[i][0], neigbors[i][1]].setVisited(true);
        }
        cellsToVisit.RemoveAt(0);
    }

    public List<int[]> findNeightbors(int x, int y)
    {
        List<int[]> neighbors = new List<int[]>();
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (i + j != 0)
                {
                    if (x + i >= 0 && x + i < World.GetLength(0))
                    {
                        if (y + j >= 0 && y + j < World.GetLength(1))
                        {
                            if (x + i >= targetObject.transform.position.x - scaleXlen && x + i < targetObject.transform.position.x + scaleXlen)
                            {
                                if (y + j >= targetObject.transform.position.y - scaleXlen && y + j < targetObject.transform.position.y + scaleYlen)
                                {
                                    // print(x+i + " "+ y+j);
                                    if (!World[x + i, y + j].visited())
                                    {
                                        int[] arr = new int[2];
                                        arr[0] = x + i;
                                        arr[1] = y + j;
                                        neighbors.Add(arr);

                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        return neighbors;
    }
    void Update()
    {

    }

}
