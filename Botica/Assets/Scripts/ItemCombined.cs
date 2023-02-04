using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemCombined", menuName = "ScriptableObjects/ItemCombined")]
public class ItemCombined : ScriptableObject
{
    public Item item1;
    public Item item2;
    public Item itemResult;
}
