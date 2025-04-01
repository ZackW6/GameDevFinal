using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    protected List<Item> unequippedItems;

    protected List<Item> equippedItems;

    public Inventory(){
        unequippedItems = new List<Item>();
        equippedItems = new List<Item>();
    }
}
