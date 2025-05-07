using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class Item : DragDrop
{
    private bool dropped = false;
    public string title;
    private Rigidbody2D rb;
    private Collider2D col;
    private PlayerInventory playerInventory;

    public float durability = 0;
    public float bonusHealth = 0;
    public float protection = 0;
    public override void Awake()
    {
        base.Awake();
        this.rb = this.GetComponent<Rigidbody2D>();
        this.col = this.GetComponent<Collider2D>();
        rb.simulated = true;
        col.enabled = true;
        playerInventory = FindObjectOfType<PlayerInventory>();
    }

    public override void OnBeginDrag(PointerEventData data){
        base.OnBeginDrag(data);
        playerInventory.addDropped(this);
        Drop(false);
    }

    public override void OnEndDrag(PointerEventData data){
        base.OnEndDrag(data);
        if (!container){
            Drop(true);
        }else{
            Drop(false);
            Equip();
        }
    }

    public void Drop(bool dropped){
        this.dropped = dropped;
        if (dropped){
            rb.simulated = true;
            col.enabled = true;
        }else{
            transform.rotation = Quaternion.identity;
            rb.velocity = new Vector2();
            rb.angularVelocity = 0;
            rb.simulated = false;
            col.enabled = false;
        }
    }

    public void Equip(){
        playerInventory.checkEquipped();
    }
    public bool IsDropped(){
        return dropped;
    }

    public void Hide(bool hide){
        GetComponent<UnityEngine.UI.Image>().enabled = !hide;
        rb.simulated = !hide;
        col.enabled = !hide;
    }

    public float GetDurability(){
        return durability;
    }
    public float GetBonusHealth(){
        return bonusHealth;
    }
        public float GetProtection(){
        return protection;
    }
}