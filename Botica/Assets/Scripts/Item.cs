using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
public class Item : ScriptableObject
{
    //public Mesh ItemMesh;
    //public Material ItemMaterial;
    public GameObject ItemGameObject;
    public Sprite ItemImage;
    public string ItemName;
    public string ItemDescription;
}
