using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element
{
    NORMAL,
    BOSS
};

[CreateAssetMenu(menuName = "Data/Create EnemyData")]
public class EnemyStatus : ScriptableObject
{
    public float MAXHP;
    public float ATK;
    public float SPEED;
    public float SPAN;
    public int EXP;
    public int SCORE;
    public Element ELEMENT;
}
