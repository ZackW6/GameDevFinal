using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Statistics playerStats;
    public Movement(Statistics stats){
        playerStats = stats;

    }

    public void Move(Vector2 input, Quaternion angle){
        

    }
        
    
    /* 
    * Get inputs From wearever
        Rotation, and movment direction

        Add constraitnes based on the stats of the character
        change the position and rotation of the character based on stats



    */
}
