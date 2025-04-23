using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    public List<Item> unequippedItems;
    public List<Item> equippedItems;

    public Inventory(){
        unequippedItems = new List<Item>();
        equippedItems = new List<Item>();
    }

    void Update()
    {
        foreach (Item i in unequippedItems){
            print(i.title);
        }
    }

    public void addUnequipped(Item unequipped){
        this.unequippedItems.Add(unequipped);
    }

    public void addEquipped(Item equipped){
        this.equippedItems.Add(equipped);
    }

    public bool removeUnequipped(Item unequipped){
        return this.unequippedItems.Remove(unequipped);
    }

    public bool removeEquipped(Item equipped){
        return this.equippedItems.Remove(equipped);
    }
}
