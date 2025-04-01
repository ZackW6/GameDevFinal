using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
[RequireComponent(typeof(Movement))]
public class Player : Character
{
    /* Add an inventory
        Define health, stats, and movment
        */


    void Start()
    {
       this.stats = new  Statistics();
       this.movement = new Movement(stats);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
