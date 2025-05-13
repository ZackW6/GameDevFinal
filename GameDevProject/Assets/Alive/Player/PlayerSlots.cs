using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerSlot : DropSlot
{
    public int type;
    
    public Func<Item, bool> item;

    private Player player;
    public override void Start()
    {
        player = FindObjectOfType<Player>();
        base.Start();
        switch(type){
            case 0:
                item = weapon;
            break;
            case 1:
                item = head;
            break;
            case 2:
                item = consumable;
            break;
            case 3:
                item = extra;
            break;
            case 4:
                item = chest;
                break;
            case 5:
                item = legs;
                break;
            default:
                item = extra;
            break;
        }
        if (droppedItem != null){
            droppedItem.GetComponent<DragDrop>().SetContainer(this);
            droppedItem.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }
    public static readonly Func<Item, bool> weapon = (data) => data is Weapon;
    public static readonly Func<Item, bool> head = (data) => data is Head;
    public static readonly Func<Item, bool> chest = (data) => data is Chest;
    public static readonly Func<Item, bool> legs = (data) => data is Legs;
    public static readonly Func<Item, bool> consumable = (data) => data is Consumable;
    public static readonly Func<Item, bool> extra = (data) => data is Item;

    public override void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag && eventData.pointerDrag.GetComponent<Item>() && item.Invoke(eventData.pointerDrag.GetComponent<Item>())){
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            if (eventData.pointerDrag.GetComponent<DragDrop>()){
                eventData.pointerDrag.GetComponent<DragDrop>().SetContainer(this);
                droppedItem = eventData.pointerDrag.GetComponent<DragDrop>();
            }
        }
        player.PreformStatCheck();
    }
}
