using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(BoxCollider2D))]
public abstract class Character : MonoBehaviour
{
    public Movement movement;
    public Inventory inventory;
    public virtual void Awake()
    {
        this.movement = GetComponent<Movement>();
    }

    public virtual void Kill(){
        //Killed animation here
        inventory.Kill();
    }

    public abstract void Move(Vector2 vec);
}
