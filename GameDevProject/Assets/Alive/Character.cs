using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

[RequireComponent(typeof(Movement))]
 [RequireComponent(typeof(Statistics))]
 [RequireComponent(typeof(Rigidbody2D))]
  [RequireComponent(typeof(BoxCollider2D))]
public class Character : MonoBehaviour
{
    public Statistics stats;
    public Movement movement;
    public Inventory inventory;
    public virtual void Awake()
    {
        this.stats = GetComponent<Statistics>();
        this.movement = GetComponent<Movement>();
    }

    
    public virtual void Kill(){
        inventory.Kill();
    }
}
