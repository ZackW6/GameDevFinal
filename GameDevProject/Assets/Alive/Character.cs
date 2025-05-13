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
    [SerializeField] protected float maxHealth = 100;
    [SerializeField] protected float health = 0;
    [SerializeField] protected float protection = 0;
    [SerializeField] protected float attackSpeed = 1;
    [SerializeField] protected float damage = 3;
    [SerializeField] protected float healRate = 1;
    [SerializeField] protected float jumpForce = 1500;

    protected float addedMaxHealth;
    protected float addedMaxJump;

    public AttackRange defaultAttackRange;
    protected Weapon lastUsed;

    public bool isAbleToAttack = true;
    public virtual void Awake()
    {
        addedMaxHealth = maxHealth;
        addedMaxJump = jumpForce;
        this.movement = GetComponent<Movement>();
        this.inventory = GetComponent<Inventory>();

        inventory.checkEquipped();
        PreformStatCheck();
    }

    public virtual void Update()
    {
        if (health + Time.deltaTime * healRate > addedMaxHealth){
            health = addedMaxHealth;
            return;
        }
        health+=Time.deltaTime * healRate;
    }

    public virtual void PreformStatCheck(){
        addedMaxHealth = maxHealth + inventory.GetAddedHealth();
        // addedMaxJump = jumpForce + inventory.GetAddedJump();
        // movement.jumpForce = addedMaxJump;
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
    
    public virtual void Attack(float attackSpeed, float damage, AttackRange attackRange, string tag){
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
            Invoke(nameof(Kill), .01f);
        }
    }

    public virtual void Kill(){

        //Killed animation here
        inventory.Kill();
        Destroy(gameObject);
    }
}
