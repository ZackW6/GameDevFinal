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
    public List<Weapon> weapons;
    public MultiType<Head, Chest, Legs> armors;
    public List<Consumable> consumables;

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

    public virtual void checkEquipped(){
        weapons.Clear();
        armors = new MultiType<Head, Chest, Legs>(null, null, null);
        consumables.Clear();
        equippedItems.ForEach((data)=>{
            if (data is Weapon){
                weapons.Add((Weapon)data);
            }
            if (data is Head){
                armors.a = (Head)data;
            }
            if (data is Chest){
                armors.b = (Chest)data;
            }
            if (data is Legs){
                armors.c = (Legs)data;
            }
            if (data is Consumable){
                consumables.Add((Consumable)data);
            }
        });
    }

    public void Kill(){
        PlayerInventory p = FindObjectOfType<PlayerInventory>();
        unequippedItems.ForEach((data)=>{
            data.transform.position = transform.position;
            data.Hide(false);
            p.addDropped(data);
            data.transform.SetParent(FindObjectOfType<Canvas>().transform);
        });
        equippedItems.ForEach((data)=>{
            data.transform.position = transform.position;
            data.Hide(false);
            p.addDropped(data);
            data.transform.SetParent(FindObjectOfType<Canvas>().transform);
        });
        weapons.Clear();
        armors = new MultiType<Head, Chest, Legs>(null, null, null);
        consumables.Clear();
        unequippedItems.Clear();
        equippedItems.Clear();
    }

    public float GetAddedHealth(){
        float addedFromWeapons = 0;
        foreach (Weapon w in weapons){
            addedFromWeapons += w.bonusHealth;
        }
        return (armors.a ? armors.a.bonusHealth : 0) + (armors.b ? armors.b.bonusHealth : 0)  + (armors.c ? armors.c.bonusHealth : 0)  + addedFromWeapons;
    }

    public float GetAddedProtection(){
        float addedFromWeapons = 0;
        foreach (Weapon w in weapons){
            addedFromWeapons += w.bonusHealth;
        }
        return (armors.a ? armors.a.protection : 0) + (armors.b ? armors.b.protection : 0)  + (armors.c ? armors.c.protection : 0)  + addedFromWeapons;
    }
}
