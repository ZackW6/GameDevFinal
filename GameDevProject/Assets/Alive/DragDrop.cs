using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(RectTransform))]
public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    protected RectTransform rectTransform;
    protected CanvasGroup canvasGroup;
    protected DropSlot container = null;
    protected bool isDragged = false;
    protected PointerEventData lastData = null;

    public virtual void Update(){
        if (isDragged && lastData != null){
            Vector3 mousePos;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, lastData.position, lastData.pressEventCamera, out mousePos))
            {
                rectTransform.position = mousePos;
            }
        }
    }

    public virtual void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        isDragged = true;
        container = null;
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(FindObjectOfType<Canvas>().transform);
    }
    
    public virtual void OnDrag(PointerEventData eventData)
    {
        lastData = eventData;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        isDragged = false;
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public virtual void SetContainer(DropSlot slot){
        container = slot;
        transform.SetParent(slot.transform);
        transform.position = slot.transform.position;
        transform.rotation = Quaternion.identity;
    }
    
    public virtual void OnPointerDown(PointerEventData eventData)
    {

    }

    public void LateUpdate()
    {
        // transform.rotation = Quaternion.identity;
    }
}
