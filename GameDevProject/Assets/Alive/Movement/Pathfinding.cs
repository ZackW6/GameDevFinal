using System;
using UnityEngine;
using UnityEngine.Tilemaps;
[RequireComponent(typeof(Movement))]
public class Pathfinding: MonoBehaviour
{
    public Tilemap scene;
    public Vector2[,] WorldView;
    public Movement movement;
    public void Start()
    {
        setup();
        // print("world view" + WorldView[0,1]);
     //   WorldView = new Vector2[scene.size.x][scene.size.y];
        
      //  Debug.Log("Scene" + scene.size.y);
    }

    private void setup(){
        if (!scene){
            return;
        }
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


    GameObject player;
    void Awake()
    {
        player = FindObjectOfType<Player>().gameObject;
        movement = GetComponent<Movement>();
    }
    //Poor mans pathfinding
    void FixedUpdate(){
        Vector2 vec = player.transform.position - transform.position;
        if (vec.magnitude > 10){
            return;
        }
        vec.y = vec.y > 3 ? 1 : 0;
        movement.Move(vec);
    }
}
