using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] private Image imageItem;
    [SerializeField] private Image backgroundImage;
    //[SerializeField] private Image imageGroup;
    [SerializeField] private int index;
    [SerializeField] private TextMeshProUGUI text;
    //[SerializeField] private GameObject ItemObjectPrefab;
    [SerializeField] private TextMeshProUGUI description;
    //[SerializeField] private Canvas canvas;

    public Item myItem;

    public Transform parentAfterDrag;

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        imageItem.raycastTarget = false;
        //imageGroup.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        imageItem.raycastTarget = true;
        //imageGroup.raycastTarget = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        Slot draggableItem = dropped.GetComponent<Slot>();

        print("drop");

        if (draggableItem != null && this != null)
        {
            //print("Combinamos: " + draggableItem.myItem.ItemName + " con "+ myItem.ItemName);
            Item result = InventoryContorller.Instance.CombineItems(draggableItem.myItem, this.myItem);

            if (result != null)
            {
                print("tenemos una combinación, el resultado es: "+result.name);
                InventoryContorller.Instance.DeleteItem(draggableItem.myItem);
                InventoryContorller.Instance.DeleteItem(this.myItem);
                InventoryContorller.Instance.AddItem(result);
            }
        }
    }

    public void SetItem(Item theItem)
    {
        myItem = theItem;
        imageItem.sprite = theItem.ItemImage;
        imageItem.preserveAspect = true;
        text.text = theItem.ItemName;
        description.text = myItem.ItemDescription;
    }

    public void DropItem()
    {
        if(InventoryContorller.Instance.canDropItem){
            GameObject theObject = Instantiate(myItem.ItemGameObject, Vector3.zero, Quaternion.identity);

            //theObject.Component<ItemObject>().SetItem(myItem);
            InventoryContorller.Instance.DeleteItem(myItem);
            InventoryContorller.Instance.canDropItem = false;
        }
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
