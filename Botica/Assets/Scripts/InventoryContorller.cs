using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryContorller : MonoBehaviour
{
    //Creamoss Singleton
    public static InventoryContorller Instance { get; private set; }

    private Inventory theInventory;
    //public GameObject[] Slots;
    public List<GameObject> Slots;

    //[SerializeField] GameObject SlotPrefab;
    [SerializeField] int maxSlots;
    public GameObject SlotHolder;

    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        theInventory = new Inventory(maxSlots);
        //Slots = new List<GameObject>();
    }

    public void AddItem(Item ItemObject)
    {
        if (!theInventory.IsFull())
        {
            //GameObject SlotObject = Instantiate(SlotPrefab, SlotHolder.transform);
            GameObject SlotObject = Slots[theInventory.GetAmount()];
            SlotObject.SetActive(true);
            SlotObject.GetComponent<Slot>().SetItem(ItemObject);
            //Slots.Add(SlotObject);
            theInventory.AddItem(ItemObject);
        }
    }

    public void DeleteItem(Slot SlotToRemove, int index)
    {
        if (theInventory.GetAmount() != 0)
        {
            Slots.Remove(SlotToRemove.gameObject);
            //Destroy(SlotToRemove.gameObject);
            Slots[index].SetActive(false);
            theInventory.RemoveItem(SlotToRemove.myItem);
            print("Quitamos el item");
        }
    }
}
