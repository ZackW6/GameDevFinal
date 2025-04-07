using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using static Statistics;

public abstract class Item : DragDrop
{
    private bool equipped = false;
    private bool dropped = false;
    protected readonly Statistics stats;
    public readonly string title;

    public Item(string title, Statistics stats){
        this.title = title;
        this.stats = stats;
    }

    public bool Equipped(){
        return equipped;
    }

    public void Equip(bool equipped){
        this.equipped = equipped;
    }
}
