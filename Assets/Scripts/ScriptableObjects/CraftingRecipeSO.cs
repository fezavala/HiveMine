using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObjects/CraftingRecipe")]
public class CraftingRecipe : ScriptableObject
{
    public string ItemName;
    public Sprite itemIcon;
    public OreType[] OreTypesNeeded;
    public int[] AmountNeeded;
    public WeaponSO WeaponSO; //this is weapon that will be crafted and added to inventory
}
