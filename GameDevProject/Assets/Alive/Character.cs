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

    public AttackRange defaultAttackRange;
    private Weapon lastUsed;

    public bool isAbleToAttack = true;
    public virtual void Awake()
    {
        this.movement = GetComponent<Movement>();
        this.inventory = GetComponent<Inventory>();
    }

    public void ResetAttack(){
        isAbleToAttack = true;
    }

    public virtual void Attack(){
        foreach (Weapon i in inventory.weapons){
            if (i != lastUsed && i.attackRange){
                Attack(i.attackSpeed, i.damage, i.attackRange);
                lastUsed = i;
                return;
            }
        }
        //Run attack animation
        if (defaultAttackRange){
            lastUsed = null;
            Attack(attackSpeed, damage, defaultAttackRange);
        }
        // foreach (Item i in inventory.equippedItems){
        //     if (i is Weapon){
        //         ((Weapon)i).attackSpeed
        //     }
        // }
    }
    
    public void Attack(float attackSpeed, float damage, AttackRange attackRange){
        if (isAbleToAttack){
            isAbleToAttack = false;
            Invoke(nameof(ResetAttack), attackSpeed);
            attackRange.transform.SetPositionAndRotation(transform.position, transform.rotation);
            foreach (GameObject i in attackRange.CheckCollider()){
                if (i.CompareTag("Enemy"))
                {
                    i.GetComponent<Enemy>().Damage(damage);
                }
            }
        }
    }

    public virtual void Damage(float amount){
        //Run hurt animation
        health -= amount;
        if (health <= 0){
            Invoke(nameof(Kill), .1f);
        }
    }

    public virtual void Kill(){

        //Killed animation here
        inventory.Kill();
        Destroy(gameObject);
    }
}
