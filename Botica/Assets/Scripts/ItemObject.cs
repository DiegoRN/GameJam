using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] Item myItem;

    void Start()
    {
        if (myItem) 
        {
            GetComponent<MeshFilter>().mesh = myItem.ItemMesh;
            GetComponent<MeshRenderer>().material = myItem.ItemMaterial;
        }
    }

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
