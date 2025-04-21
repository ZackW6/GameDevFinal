using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using static Statistics;
public abstract class Item : MonoBehaviour
{
    private bool equipped = false;
    private bool dropped = false;
    protected readonly Statistics stats;
    public readonly string title;
    public GameObject prefab;

    public PhysicalItem physicalItem;

    public Item(string title, Statistics stats){
        this.title = title;
        this.stats = stats;
        Instantiate(prefab);
        // prefab.AddComponent(InventoryItem);
        physicalItem.Put(this.transform.position, Quaternion.identity);
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
            Vector2 prevVec = physicalItem.GetPosition();
            Quaternion prevRot = physicalItem.GetRotation();
            physicalItem.Destroy();
            Instantiate(prefab);
            physicalItem.Put(prevVec, prevRot);
        }else{
            Vector2 prevVec = physicalItem.GetPosition();
            Quaternion prevRot = physicalItem.GetRotation();
            physicalItem.Destroy();
            Instantiate(prefab);
            physicalItem.Put(prevVec, prevRot);
        }
    }

    public bool IsDropped(){
        return dropped;
    }
}
