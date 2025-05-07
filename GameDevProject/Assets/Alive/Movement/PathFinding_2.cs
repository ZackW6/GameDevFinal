using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class PathFinding2 : MonoBehaviour
{
    public Tilemap Scene;
    public Cell[,] World;
    public Player player;
    public GameObject prefab;
    private int xPos = -33;
    private int yPos = 21;
    void Start()
    {
        
        World = new Cell[Scene.size.x, Scene.size.y];

        for (int x = xPos; x < World.GetLength(0) + xPos; x++)
        {
            for (int y = yPos; y > -World.GetLength(1) + yPos; y--)
            {
                Vector3Int pos = new Vector3Int(x, y-1, 0);
                TileBase tile = Scene.GetTile(pos);
                
                World[x - xPos, -(y) + yPos] = new Cell(x - xPos, -(y) + yPos);
                if (tile){
                    World[x - xPos, -(y) + yPos].defaultCost = 255;
                }
                if (prefab){
                    World[x - xPos, -(y) + yPos].InstantiateArrow(prefab, new Vector2(x+.5f,y-.5f), Quaternion.identity);
                }
            }
        }
        player = FindObjectOfType<Player>();
        AlotNeighbors();
        // thread = new Thread(PathingUpdate);
        // thread.Start();
    }
    // private void OnDestroy()  
    // {  
    //     thread.Abort();
    // }
    // private void OnApplicationQuit()  
    // {
    //     thread.Abort();
    // }
    // void Update(){
    //     playerX = player.transform.position.x;
    //     playerY = player.transform.position.y;
    // }
    private void LateUpdate(){
        int playerPosX = (int)Mathf.Floor(player.transform.position.x);
        int playerPosY = (int)Mathf.Ceil(player.transform.position.y);

        Cell start = null;
        for (int x = xPos; x < World.GetLength(0) + xPos; x++)
        {
            for (int y = yPos; y > -World.GetLength(1) + yPos; y--)
            {
                Cell c = World[x - xPos, -(y) + yPos];
                c.cost = 0;
                c.parent = null;
                if (c.x == (playerPosX-xPos) && c.y == (-playerPosY+yPos)){
                    start = c;
                }
            }
        }

        if (start == null || start.adjacentCells == null){
            return;
        }
        List<Cell> nextCells = new List<Cell>();
        for (int i = 0; i < start.adjacentCells.Length; i++){
            if (start.adjacentCells[i] == null){
                continue;
            }
            nextCells.Add(start.adjacentCells[i]);
            start.adjacentCells[i].parent = start;
            start.adjacentCells[i].cost = Cell.GetCostDirectionPair(i).a + start.adjacentCells[i].defaultCost;
            start.adjacentCells[i].direction = Cell.GetCostDirectionPair(i).b;
        }
        IterateThroughMap(start, nextCells);
    }
    private void IterateThroughMap(Cell start, List<Cell> cleanCells){
        List<Cell> nextCells = new List<Cell>();
        for (int i = 0; i < cleanCells.Count; i++){
            for (int j = 0; j < cleanCells[i].adjacentCells.Length; j++){
                if (cleanCells[i].adjacentCells[j] == null || cleanCells[i].adjacentCells[j].parent != null || cleanCells[i].cost > 50 || cleanCells[i].adjacentCells[j].Equals(start)){
                    if (cleanCells[i].adjacentCells[j] != null && cleanCells[i].cost + Cell.GetCostDirectionPair(j).a < cleanCells[i].adjacentCells[j].cost){
                        cleanCells[i].adjacentCells[j].parent = cleanCells[i];
                        cleanCells[i].adjacentCells[j].cost = cleanCells[i].cost + Cell.GetCostDirectionPair(j).a + cleanCells[i].adjacentCells[j].defaultCost;
                        cleanCells[i].adjacentCells[j].direction = Cell.GetCostDirectionPair(j).b;
                    }
                    continue;
                }
                // if (Cell.GetCostDirectionPair(j).a > 1){
                //     nextCells.Insert(0,cleanCells[i].adjacentCells[j]);
                // }else{
                //     nextCells.Add(cleanCells[i].adjacentCells[j]);
                // }
                nextCells.Insert(0,cleanCells[i].adjacentCells[j]);
                // nextCells.Add(cleanCells[i].adjacentCells[j]);
                cleanCells[i].adjacentCells[j].parent = cleanCells[i];
                cleanCells[i].adjacentCells[j].cost = cleanCells[i].cost + Cell.GetCostDirectionPair(j).a + cleanCells[i].adjacentCells[j].defaultCost;
                cleanCells[i].adjacentCells[j].direction = Cell.GetCostDirectionPair(j).b;
            }
        }
        if (nextCells.Count == 0){
            return;
        }
        IterateThroughMap(start, nextCells);
    }

    public MultiType<float, Vector2> DirectionOfFollower(Vector2 pos){
        int posX = (int)Mathf.Floor(pos.x);
        int posY = (int)Mathf.Floor(pos.y);
        for (int x = xPos; x < World.GetLength(0) + xPos; x++)
        {
            for (int y = yPos; y > -World.GetLength(1) + yPos; y--)
            {
                Cell c = World[x - xPos, -(y) + yPos];
                if (c.x == (posX-xPos) && c.y == (-posY+yPos)){
                    return new MultiType<float, Vector2>(c.cost,new Vector2(c.direction.x, c.direction.y));
                }
            }
        }
        int i = 0;
        while(i < 10000){
            i++;
        }
        return new MultiType<float, Vector2>(0, new Vector2(0,0));
    }


    // public List<int[]> findNeightbors(int x, int y)
    // {
    //     List<int[]> neighbors = new List<int[]>();
    //     for (int i = -1; i < 2; i++)
    //     {
    //         for (int j = -1; j < 2; j++)
    //         {
    //             if (i + j != 0 && x + i >= 0 && x + i < World.GetLength(0) && y + j >= 0 && y + j < World.GetLength(1)
    //             && x + i >= targetObject.transform.position.x - scaleXlen && x + i < targetObject.transform.position.x + scaleXlen
    //             && y + j >= targetObject.transform.position.y - scaleXlen && y + j < targetObject.transform.position.y + scaleYlen
    //             && !World[x + i, y + j].visited())
    //             {
    //                 int[] arr = new int[2];
    //                 arr[0] = x + i;
    //                 arr[1] = y + j;
    //                 neighbors.Add(arr);
    //             }
    //         }
    //     }
    //     return neighbors;
    // }
    private void AlotNeighbors()
    {
        for (int x = xPos; x < World.GetLength(0) + xPos; x++)
        {
            for (int y = yPos; y > -World.GetLength(1) + yPos; y--)
            {
                Cell[] neighbors = new Cell[8];
                int additive = 4;
                for (int a = -1; a <= 1; a++){
                    for (int b = -1; b <= 1; b++){
                        if (a == 0 && b == 0){
                            additive--;
                            continue;
                        }
                        try
                        {
                            neighbors[b+a*3+additive] = World[x - xPos + b, -y + yPos + a];
                        }
                        catch (System.Exception)
                        {
                            neighbors[b+a*3+additive] = null;
                        }
                    }
                }
                World[x - xPos, -y + yPos].adjacentCells = neighbors;
            }
        }
    }
}
