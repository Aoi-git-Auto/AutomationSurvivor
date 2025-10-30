using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    NORMAL,
    ELITE,
    BOSS
};

[CreateAssetMenu(menuName = "Data/Create EnemyData")]
public class EnemyStatus : ScriptableObject
{
    public float MAXHP;
    public float ATK;
    public float SPEED;
    public int EXP;
    public int SCORE;
    public Type TYPE;
    public GameObject PREHUB;
    public AudioClip BGM;
    public String NAME;
}
