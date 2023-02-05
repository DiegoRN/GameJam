using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] Item myItem;

    public void Interactuate()
    {
        InventoryContorller.Instance.AddItem(myItem);
        Destroy(this.gameObject, 0.1f);
    }

    public void SetItem(Item item)
    {
        myItem = item;
    }
}
