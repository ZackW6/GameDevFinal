using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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

    private bool isFalling = false;
    private float jumpCount = 0f;
    public Movement(Statistics stats){
         playerStats = stats;
    }
    public void Awake(){
       ;
         rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        float a = Input.GetAxis("Horizontal");
        float b = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(a, 0);

        Move(dir * 0.5f);
      if(Input.GetKeyDown(KeyCode.Space)){
        Jump();
      }
    }

    public void Move(Vector2 input){

      if(!isFalling && math.abs(rb.velocity.x) < 10){
        rb.velocity += input;
      //rb.AddForce( input, ForceMode2D.Impulse);
      }
    }

    public void Jump(){
        Vector2 dir = new Vector2(0, 75);
        if(!isFalling){
        rb.AddForce(dir, ForceMode2D.Impulse);

        }
    }

 
        
      private void OnCollisionEnter2D(Collision2D other)
    {

       ContactPoint2D[] cP = new ContactPoint2D[other.contactCount];
       other.GetContacts(cP);
       var y = 0;
       foreach(ContactPoint2D c in cP){
          if(c.point.y < transform.position.y){
            y ++;
            break;
          }
       }
       // for( int i = 0; i <  ; i++)
        if (other.gameObject.CompareTag("Cave") && y > 0){
            isFalling = false;
        }
        print(y);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Cave"))
        {
            isFalling = true;
        }
        print(isFalling);
    }
    /* 
    * Get inputs 
            movment 

        Add constraitnes based on the stats of the character
        change the position and rotation of the character based on stats



    */
}
