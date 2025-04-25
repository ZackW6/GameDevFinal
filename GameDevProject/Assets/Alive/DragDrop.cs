using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
[RequireComponent(typeof(CanvasGroup))]
public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    protected DropSlot container = null;

    public virtual void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        container = null;
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }
    
    public virtual void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += (eventData.delta/(64.71071f))*(Camera.main.orthographicSize/5);
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public virtual void SetContainer(DropSlot slot){
        container = slot;
    }
    
    public virtual void OnPointerDown(PointerEventData eventData)
    {

    }
}
