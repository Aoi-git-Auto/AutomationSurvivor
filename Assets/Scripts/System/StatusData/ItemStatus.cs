using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "Data/Create ItemStatus")]
public class ItemStatus : ScriptableObject
{
    public String NAME;
    public String INFO;
    public Sprite SPRITE;
    public GameObject PREHUB;
}
