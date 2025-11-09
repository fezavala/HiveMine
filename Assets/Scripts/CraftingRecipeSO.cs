using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OreList
{
    Iron,
    Gold,
    Ruby,
    Rare
}

[CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObjects/CraftingRecipe")]
public class CraftingRecipe : ScriptableObject
{
    public string ItemName;
    public Sprite itemIcon;
    public OreList[] OreTypes;
    public int[] AmountNeeded;
}
