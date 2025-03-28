using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static Statistics;

public class Weapon : Item
{
    public Weapon(string name)
        : base(name, new Statistics(SetMarkers.Durability, SetMarkers.AttackSpeed, SetMarkers.Damage, SetMarkers.Range, SetMarkers.BonusHealth))
    {
        
    }

    public float GetDurability(){
        return stats.Get(SetMarkers.Durability);
    }

    public float GetAttackSpeed(){
        return stats.Get(SetMarkers.AttackSpeed);
    }

    public float GetDamage(){
        return stats.Get(SetMarkers.Range);
    }

    public float GetBonusHealth(){
        return stats.Get(SetMarkers.BonusHealth);
    }

    public void Interact(Character enactingCharacter){
        //TODO use the character position and orientation to decide what and how to attack
    }
}
