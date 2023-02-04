using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image imageItem;
    [SerializeField] private Image imageGroup;
    [SerializeField] private int index;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject ItemObjectPrefab;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private Canvas canvas;

    public Item myItem;

    public Transform parentAfterDrag;

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        imageItem.raycastTarget = false;
        imageGroup.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        imageItem.raycastTarget = true;
        imageGroup.raycastTarget = true;
    }

    public void SetItem(Item theItem)
    {
        myItem = theItem;
        imageItem.sprite = theItem.ItemImage;
        text.text = theItem.ItemName;
        description.text = myItem.ItemDescription;
    }

    public void DropItem()
    {
        GameObject theObject = Instantiate(ItemObjectPrefab, Vector3.zero, Quaternion.identity);
        theObject.GetComponent<ItemObject>().SetItem(myItem);
        InventoryContorller.Instance.DeleteItem(this, index);
    }

    public void ShowDescription()
    {
        description.enabled = true;
    }

    public void HideDescription()
    {
        description.enabled = false;
    }
}