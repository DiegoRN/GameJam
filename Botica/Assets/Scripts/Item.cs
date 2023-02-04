using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
public class Item : ScriptableObject
{
    public Mesh ItemMesh;
    public Sprite ItemImage;
    public string ItemName;
    public string ItemDescription;
}
