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
    
      // stats = new Statistics();

      movement = gameObject.GetComponent<Movement>();


    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
