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
    [SerializeField] private TextMeshProUGUI description;

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

    public void ShowDescription()
    {
        description.text = myItem.ItemDescription;
        description.enabled = true;
    }

    public void HideDescription()
    {
        description.enabled = false;
    }
}
