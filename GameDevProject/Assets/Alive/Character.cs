using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Movement))]

 [RequireComponent(typeof(Statistics))]
 [RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
   
    protected Health health;
    protected Statistics stats;
    protected Movement movement;

 
}
