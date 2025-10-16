using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataBase",menuName = "Data/Create EnemyDataBase")]
public class EnemyDataBase : ScriptableObject
{
    public List<GameObject> enemies = new List<GameObject>();
}
