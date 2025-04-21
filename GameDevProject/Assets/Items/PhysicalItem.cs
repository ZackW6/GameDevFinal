using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using static Statistics;

public interface PhysicalItem
{
    public void Destroy();
    public void Put(Vector2 place, Quaternion rotation);
    public Vector2 GetPosition();
    public Quaternion GetRotation();
}
