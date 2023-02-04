using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0) { //si no está ocupado
            GameObject dropped = eventData.pointerDrag;
            Slot draggableItem = dropped.GetComponent<Slot>();
            draggableItem.parentAfterDrag = transform;
        } else {
            //Está ocupado
        }
    }
}
