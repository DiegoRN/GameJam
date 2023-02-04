using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour //, IDropHandler
{
    /*
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        Slot draggableItem = dropped.GetComponent<Slot>(); 
        Slot slotTarget = this.GetComponentInChildren<Slot>();

        print("drop");

        if (draggableItem != null && slotTarget != null)
        {
            print("Combinamos: " + draggableItem.myItem.ItemName + " con "+draggableItem.myItem.ItemName);
            Item result = GameManager.Instance.CombineItems(draggableItem.myItem, slotTarget.myItem);

            if (result != null)
            {
                print("tenemos una combinación, el resultado es: "+result.name);
                InventoryContorller.Instance.DeleteItem(draggableItem.myItem);
                InventoryContorller.Instance.DeleteItem(slotTarget.myItem);
                InventoryContorller.Instance.AddItem(result);
            }
        }
        
    }
    */
}
