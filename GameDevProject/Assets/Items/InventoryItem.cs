using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using static Statistics;

[RequireComponent(typeof(RectTransform))]

public class InventoryItem : DragDrop, PhysicalItem
{

    RectTransform position;
    void Awake()
    {
        position = this.GetComponent<RectTransform>();
    }
    public void Destroy()
    {
        Destroy(this);
    }

    public void Put(Vector2 place, Quaternion rotation)
    {
        position.anchoredPosition = place;
        position.rotation = rotation;
    }

    public Vector2 GetPosition()
    {
        return position.anchoredPosition;
    }

    public Quaternion GetRotation()
    {
        return position.rotation;
    }
}
