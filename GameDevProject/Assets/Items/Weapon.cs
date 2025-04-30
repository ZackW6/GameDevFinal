using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Weapon : Item
{
    public float durability = 0;
    public float attackSpeed = 0;
    public float damage = 0;
    public float bonusHealth = 0;
    public AttackRange attackRange;
    public override void Awake(){
        base.Awake();
    }
    public float GetDurability(){
        return durability;
    }

    public float GetAttackSpeed(){
        return attackSpeed;
    }

    public float GetDamage(){
        return damage;
    }

    public float GetBonusHealth(){
        return bonusHealth;
    }

    public void Interact(Character enactingCharacter){
        //TODO use the character position and orientation to decide what and how to attack
        //using attackRange
    }
}
