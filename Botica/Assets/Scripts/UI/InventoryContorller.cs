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

    [SerializeField] GameObject SlotPrefab;
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
        Slots = new List<GameObject>();

        print("Creado el inventario, tiene "+maxSlots+" slots");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(Item ItemObject)
    {
        if (!theInventory.IsFull())
        {
            GameObject SlotObject = Instantiate(SlotPrefab, SlotHolder.transform);
            SlotObject.GetComponent<Slot>().SetItem(ItemObject);
            Slots.Add(SlotObject);
            theInventory.AddItem(ItemObject);
        }
    }

    public void DeleteItem(Slot SlotToRemove)
    {
        if (theInventory.GetAmount() != 0)
        {
            Slots.Remove(SlotToRemove.gameObject);
            Destroy(SlotToRemove.gameObject);
            theInventory.RemoveItem(SlotToRemove.myItem);
            print("Quitamos el item");
        }
    }
}
