using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static Statistics;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class Item : DragDrop
{
    private bool equipped = false;
    private bool dropped = false;
    protected Statistics stats;
    public string title;
    private Rigidbody2D rb;
    private Collider2D col;
    public override void Awake()
    {
        base.Awake();
        this.rb = this.GetComponent<Rigidbody2D>();
        this.col = this.GetComponent<Collider2D>();
        rb.simulated = true;
        col.enabled = true;
    }

    public override void OnBeginDrag(PointerEventData data){
        base.OnBeginDrag(data);
        Drop(false);
    }

    public override void OnEndDrag(PointerEventData data){
        base.OnEndDrag(data);
        if (!container){
            Drop(true);
        }else{
            Equip(true);
            Drop(false);
        }
    }

    public bool IsEquipped(){
        return equipped;
    }

    public void Equip(bool equipped){
        this.equipped = equipped;
    }

    public void Drop(bool dropped){
        this.dropped = dropped;
        if (dropped){
            rb.simulated = true;
            col.enabled = true;
        }else{
            rb.simulated = false;
            col.enabled = false;
        }
    }

    public bool IsDropped(){
        return dropped;
    }

    public void Hide(bool hide){
        rb.simulated = !hide;
        col.enabled = !hide;
        this.enabled = !hide;
    }

    public Statistics GetStatistics(){
        return stats;
    }
}
