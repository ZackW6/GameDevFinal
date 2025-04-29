using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Inventory))]
public abstract class Character : MonoBehaviour
{
    public Movement movement;
    public Inventory inventory;

    [Header("Stats")]
    [SerializeField] private float health = 100;
    [SerializeField] private float bonusHealth = 0;
    [SerializeField] private float protection = 0;
    [SerializeField] private float attackSpeed = 1;
    [SerializeField] private float damage = 3;

    public Collider2D defaultAttackRange;

    public bool isAbleToAttack = true;
    public virtual void Awake()
    {
        this.movement = GetComponent<Movement>();
        this.inventory = GetComponent<Inventory>();
        defaultAttackRange.enabled = false;
    }

    public void ResetAttack(){
        isAbleToAttack = true;
    }

    public virtual void Attack(float amount){
        //Run attack animation
        if (defaultAttackRange && isAbleToAttack){
            isAbleToAttack = false;
            Invoke("ResetAttack", attackSpeed);

            defaultAttackRange.enabled = true;
            // defaultAttackRange.IsTouching()
        }
        // foreach (Item i in inventory.equippedItems){
        //     if (i is Weapon){
        //         ((Weapon)i).attackSpeed
        //     }
        // }
    }

    public virtual void Damage(float amount){
        //Run hurt animation
        health -= amount;
        if (health <= 0){
            Kill();
        }
    }

    public virtual void Kill(){
        //Killed animation here
        inventory.Kill();
    }
}
