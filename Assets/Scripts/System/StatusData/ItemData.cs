using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataBase", menuName = "Data/Create ItemDataBase")]
public class ItemData : ScriptableObject
{
    public List<ItemStatus> ITEMS = new List<ItemStatus>();
}
