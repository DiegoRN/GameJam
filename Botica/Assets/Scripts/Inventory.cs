using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> theInventory;
    private int maxCapacity;
    private int amount;

    public Inventory(int Capacity)
    {
        theInventory = new List<Item>(Capacity);
        maxCapacity = Capacity;
        amount = 0;
    }

    public bool AddItem(Item theItem)
    {
        if (amount < maxCapacity)
        {
            amount++;
            theInventory.Add(theItem);
            return true;
        }
        else 
        {
            return false;
        }
    }

    public bool RemoveItem(Item theItem)
    {
        if (theInventory.Contains(theItem) && amount > 0)
        {
            theInventory.Remove(theItem);
            amount--;
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetAmount()
    {
        return amount;
    }

    public bool IsFull()
    {
        return amount >= maxCapacity;
    }
}
