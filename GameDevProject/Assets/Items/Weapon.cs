using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Weapon : Item
{
    public float attackSpeed = 0;
    public float damage = 0;
    public AttackRange attackRange;
    public override void Start(){
        base.Start();
    }

    public float GetAttackSpeed(){
        return attackSpeed;
    }

    public float GetDamage(){
        return damage;
    }
    public override void WriteStats(){
        stats.text = "AttackSpeed: "+attackSpeed+"\n"+"Damage: "+damage;
    }
}