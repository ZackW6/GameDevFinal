using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Item
{
    public float durability = 0;
    public float protection = 0;
    public float bonusHealth = 0;
    public override void Awake()
    {
        base.Awake();
    }

    public float GetProtection(){
        return protection;
    }

    public float GetDurability(){
        return durability;
    }

    public float GetBonusHealth(){
        return bonusHealth;
    }

}
