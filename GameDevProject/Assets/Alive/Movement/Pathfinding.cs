using UnityEngine;
using UnityEngine.Tilemaps;
[RequireComponent(typeof(Movement))]
public class Pathfinding: MonoBehaviour
{
    public Tilemap scene;
    public Vector2[,] WorldView;
    public void Start()
    {
        setup();
        print("world view" + WorldView[0,1]);
     //   WorldView = new Vector2[scene.size.x][scene.size.y];
        
      //  Debug.Log("Scene" + scene.size.y);
    }

    private void setup(){
        WorldView = new Vector2[scene.size.x, scene.size.y];
        for(int x = 0; x < scene.size.x; x++){
            for(int y = 0; y < scene.size.y; y++){
                Vector3Int pos = new Vector3Int(x, y, 1);
                if(!scene.HasTile(pos)){
                    WorldView[x,y] = new Vector2(1, 1);
                }
            }
        }
    }
  
    //Scene With and Size
    // Build a 2d array or air boxes
    // place a vector within each index
    // apply vector to enemy
}
