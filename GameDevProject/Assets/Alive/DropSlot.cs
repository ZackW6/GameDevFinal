using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    private RectTransform rectTransform;
    public DragDrop droppedItem;
    public virtual void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null){
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            if (eventData.pointerDrag.GetComponent<DragDrop>()){
                eventData.pointerDrag.GetComponent<DragDrop>().SetContainer(this);
                droppedItem = eventData.pointerDrag.GetComponent<DragDrop>();
            }
        }
    }
}
