using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrades")]
public class Upgrade : ScriptableObject
{
    public new string name;
    public float strength;
    public float cost;
    public float baseCost;
    public float amount;
}
