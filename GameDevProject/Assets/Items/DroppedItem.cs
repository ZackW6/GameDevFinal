using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using static Statistics;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class DroppedItem : MonoBehaviour, PhysicalItem
{
    public void Destroy()
    {
        Destroy(this);
    }

    public void Put(Vector2 place, Quaternion rotation)
    {
        transform.position = place;
        transform.rotation = rotation;
    }
    
    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public Quaternion GetRotation()
    {
        return transform.rotation;
    }
}
