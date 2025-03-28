using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    List<Item> unequippedItems;

    List<Item> equippedItems;

    public Canvas inventory;

    public Inventory(){
        unequippedItems = new List<Item>();
        equippedItems = new List<Item>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }
    
    public void OnDrag(PointerEventData eventData)
    {
        //TODO not done yet
        unequippedItems[0].rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

    
}
