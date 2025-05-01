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
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float health = 0;
    [SerializeField] private float protection = 0;
    [SerializeField] private float attackSpeed = 1;
    [SerializeField] private float damage = 3;
    [SerializeField] private float healRate = 1;

    private float addedMaxHealth;

    public AttackRange defaultAttackRange;
    private Weapon lastUsed;

    public bool isAbleToAttack = true;
    public virtual void Awake()
    {
        addedMaxHealth = maxHealth;
        this.movement = GetComponent<Movement>();
        this.inventory = GetComponent<Inventory>();

        inventory.checkEquipped();
        PreformStatCheck();
    }

    public virtual void Update()
    {
        if (health + Time.deltaTime * healRate > addedMaxHealth){
            health = addedMaxHealth;
        }
        health+=Time.deltaTime * healRate;
    }

    public virtual void PreformStatCheck(){
        addedMaxHealth = maxHealth + inventory.GetAddedHealth();
        protection = inventory.GetAddedProtection();
    }

    public void ResetAttack(){
        isAbleToAttack = true;
    }

    public virtual void Attack(string tag){
        foreach (Weapon i in inventory.weapons){
            if (i != lastUsed && i.attackRange){
                Attack(i.attackSpeed, i.damage, i.attackRange, tag);
                lastUsed = i;
                return;
            }
        }
        //Run attack animation
        if (defaultAttackRange){
            lastUsed = null;
            Attack(attackSpeed, damage, defaultAttackRange, tag);
        }
        // foreach (Item i in inventory.equippedItems){
        //     if (i is Weapon){
        //         ((Weapon)i).attackSpeed
        //     }
        // }
    }
    
    public void Attack(float attackSpeed, float damage, AttackRange attackRange, string tag){
        if (isAbleToAttack){
            isAbleToAttack = false;
            Invoke(nameof(ResetAttack), attackSpeed);
            attackRange.transform.SetPositionAndRotation(transform.position, transform.rotation);
            foreach (GameObject i in attackRange.CheckCollider()){
                if (i.CompareTag(tag))
                {
                    i.GetComponent<Character>().Damage(damage);
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
