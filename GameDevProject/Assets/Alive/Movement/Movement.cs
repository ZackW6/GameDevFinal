using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.Callbacks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Animations;


/* 1. Need to create Constriants on inputs

*/

public class Movement : MonoBehaviour
{

    private Statistics playerStats; 
    private Rigidbody2D rb;
    public Movement(Statistics stats){
         playerStats = stats;
    }
    public void Awake(){
       ;
         rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 input){
       print("1");
      rb.AddForce( input * 1, ForceMode2D.Impulse);

    }

    public Vector2 ConstraintSourceCheck(){

        return new Vector2(0, 0);
    }
        
    
    /* 
    * Get inputs From wearever
        Rotation, and movment direction

        Add constraitnes based on the stats of the character
        change the position and rotation of the character based on stats



    */
}
