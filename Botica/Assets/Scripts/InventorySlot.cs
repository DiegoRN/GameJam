using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        Slot draggableItem = dropped.GetComponent<Slot>();

        if (transform.childCount == 0) { //si no está ocupado
            draggableItem.parentAfterDrag = transform;
        } else {
            //Está ocupado
            Slot slotTarget = this.GetComponentInChildren<Slot>();

            Item result = GameManager.Instance.CombineItems(draggableItem.myItem, slotTarget.myItem);

            if(result != null)
            {
                print("tenemos una combinación, el resultado es: "+result.name);
                InventoryContorller.Instance.DeleteItem(draggableItem.myItem);
                InventoryContorller.Instance.DeleteItem(slotTarget.myItem);
                InventoryContorller.Instance.AddItem(result);
            }
        }
    }
}
