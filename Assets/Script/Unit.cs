using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;

    public int maxHP = 20;
    public int currentHP;

    public bool Attack;
    public bool Defense;
    public bool Counter;

    public bool turnSkip;

    public bool PCBehavior;
}