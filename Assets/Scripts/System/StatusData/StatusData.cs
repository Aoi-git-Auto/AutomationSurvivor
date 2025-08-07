using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/Create StatusData")]
public class StatusData : ScriptableObject
{
    public float MAXHP;
    public float ATK;
    public float SPEED;
    public float NockBack;
    public float SPAN;
    public int EXP;
    public bool BOSS;
}
