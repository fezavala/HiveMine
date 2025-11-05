using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ore", menuName = "ScriptableObjects/OreInventory")]
public class OreData : ScriptableObject
{
    public string oreType;
    public int AmountInInventory;

}
