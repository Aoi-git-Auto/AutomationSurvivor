using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataBase",menuName = "Data/Create EnemyDataBase")]
public class EnemyDataBase : ScriptableObject
{
    public List<EnemyStatus> enemies = new List<EnemyStatus>();
}
