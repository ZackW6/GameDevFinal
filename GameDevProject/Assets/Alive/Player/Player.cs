using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.Collections;
using UnityEditor.Callbacks;
using UnityEngine;

  /* 
       1. Find a way to input stats into movment
   
   
    */
public class Player : Character
{


    void Start()
    {
       this.stats = new  Statistics();
       this.movement = new Movement(stats);
    }

    // Update is called once per frame
    void Update()
    {
        float a = Input.GetAxis("Horizontal");
        float b = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(a, b);

      movement.Move(dir);
    }
}
