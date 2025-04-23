using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Statistics;

public class Armor : Item
{
    public override void Awake()
    {
        base.Awake();
        stats = new Statistics(SetMarkers.Durability, SetMarkers.Protection, SetMarkers.BonusHealth);
    }

    public float GetProtection(){
        return stats.Get(SetMarkers.Protection);
    }

    public float GetDurability(){
        return stats.Get(SetMarkers.Durability);
    }

    public float GetBonusHealth(){
        return stats.Get(SetMarkers.BonusHealth);
    }

}
