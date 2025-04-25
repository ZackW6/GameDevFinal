using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Statistics))]
[RequireComponent(typeof(Inventory))]
public abstract class Character : MonoBehaviour
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
