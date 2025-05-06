
using System;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Cell
{
    // Start is called before the first frame update
    
    public float cost;
    public float defaultCost = 0;
    public Vector2 m_direction;
    public Vector2 direction{
        get{
            return m_direction;
        }
        set{
            m_direction = value;
            if (arrow){
                arrow.transform.rotation = Quaternion.Euler(0,0,Mathf.Atan(value.y/value.x)*180/Mathf.PI + (value.x < 0 ? 180 : 0));
            }
        }
    }
    public static readonly Dictionary<int,MultiType<float,Vector2>> adjacentMatches = new Dictionary<int, MultiType<float, Vector2>>();
    public bool isVisited;
    public int x;
    public int y;
    public Cell parent;
    private Cell[] m_adjacentCells;
    public Cell[] adjacentCells{
        get{
            return m_adjacentCells;
        }
        set{
            if (adjacentCells != null && adjacentCells.Length != 8){
                throw new ArgumentException("You have an incorrect number of elements in \"adjacentCells\"");
            }
            m_adjacentCells = value;
        }
    }
    public GameObject arrow;
    static Cell(){
        adjacentMatches.Add(0,new MultiType<float, Vector2>(1.41f,new Vector2(1,-1)));
        adjacentMatches.Add(1,new MultiType<float, Vector2>(1,new Vector2(0,-1)));
        adjacentMatches.Add(2,new MultiType<float, Vector2>(1.41f,new Vector2(-1,-1)));
        adjacentMatches.Add(3,new MultiType<float, Vector2>(1,new Vector2(1,0)));
        adjacentMatches.Add(4,new MultiType<float, Vector2>(1,new Vector2(-1,0)));
        adjacentMatches.Add(5,new MultiType<float, Vector2>(1.41f,new Vector2(1,1)));
        adjacentMatches.Add(6,new MultiType<float, Vector2>(1,new Vector2(0,1)));
        adjacentMatches.Add(7,new MultiType<float, Vector2>(1.41f,new Vector2(-1,1)));
    }
    public static MultiType<float, Vector2> GetCostDirectionPair(int i){
        return Cell.adjacentMatches.GetValueOrDefault<int, MultiType<float, Vector2>>(i);
    }
    public void InstantiateArrow(GameObject prefab, Vector2 position, Quaternion rotation){
        arrow = GameObject.Instantiate(prefab, position, rotation);
    }
    
    public Cell(int i, int j){
        x = i;
        y = j;
    }
}
