using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject ItemObjectPrefab;

    public Item myItem;

    public void SetItem(Item theItem)
    {
        myItem = theItem;
        image.sprite = theItem.ItemImage;
        text.text = theItem.ItemName;
    }

    public void DropItem()
    {
        GameObject theObject = Instantiate(ItemObjectPrefab, Vector3.zero, Quaternion.identity);
        theObject.GetComponent<ItemObject>().SetItem(myItem);
        InventoryContorller.Instance.DeleteItem(this);
    }
}
