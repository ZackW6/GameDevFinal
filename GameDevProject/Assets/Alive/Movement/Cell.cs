
using Microsoft.Unity.VisualStudio.Editor;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Cell : MonoBehaviour
{
    // Start is called before the first frame update
    
    private double cost;
    private Vector2 Dir;
    private bool isVisited;

    private int Xindex;
    private int Yindex;

    public Cell(int i, int j){
        Xindex = i;
        Yindex = j;
    }
    public void setCost(double preCost)
    {
        this.cost = preCost + 1;
    }

    public bool visited(){
        return isVisited;
    }

    public void setVisited(bool v){
        isVisited = v;
    }
    

    public double getCost(){
       return cost; 
    }
    public void calcVec(Cell[,] world){
        Vector2 sumDir = new Vector2(0, 0);
        int numNeigh = 0;
        for(var i = -1; i <= 1; i++){
            for(var j = -1; j <= 1; j++){
                if(i + j != 0){
                    if(i + Xindex < world.GetLength(0) && i + Xindex >= 0){
                        if(j + Yindex < world.GetLength(1) && j + Yindex >= 0){
                            numNeigh ++;
                            Vector2 dir = new Vector2( i, j);
                            sumDir += dir * (float)world[Xindex + i, Yindex + j].getCost();
                        }   
                    }
                }
            }
        }
        sumDir/= numNeigh;
        Dir = sumDir;
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
