using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
    
      double x =  statistics.Get("HI");
      print(x + "s");

    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
