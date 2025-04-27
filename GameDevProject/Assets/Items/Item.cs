using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class Item : DragDrop
{
    private bool dropped = false;
    public string title;
    private Rigidbody2D rb;
    private Collider2D col;
    private PlayerInventory playerInventory;
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
        rb.simulated = !hide;
        col.enabled = !hide;
        this.enabled = !hide;
    }
}