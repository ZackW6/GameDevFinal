using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legs : Item
{
    public float addedJump = 0;
    public float movementIncrease = 0;
    public float GetAddedJump(){
        return addedJump;
    }   
    public float GetAddedMovement(){
        return movementIncrease;
    }
}
