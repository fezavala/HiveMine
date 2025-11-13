using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    [SerializeField] private Inventory inventory;

    public bool HasOresNeeded(CraftingRecipe recipe)
    {
        bool[] satisfied = new bool[recipe.OreTypesNeeded.Length];

        for (int i=0; i< inventory.oreCounts.Count; i++) //go through every ore in inventory
        {
            OreType currentOre = inventory.oreTypeList[i];
            int currentAmount = inventory.oreCounts[i];

            for (int j=0; j < recipe.OreTypesNeeded.Length; j++) //go through every ore needed in recipe
            {
                if (!satisfied[j] && recipe.OreTypesNeeded[j] == currentOre)
                {
                    if (currentAmount >= recipe.AmountNeeded[j])
                    {
                        satisfied[j] = true; // requirement met
                        break; // stop checking this inventory ore
                    }
                }
            }

            // Check if all recipe ores are satisfied
            for (int j = 0; j < satisfied.Length; j++)
            {
                if (!satisfied[j])
                {
                    Debug.Log("Missing or insufficient" + recipe.OreTypesNeeded[j] + ", need " + recipe.AmountNeeded[j]);
                    return false;
                }
            }
        }
        return true;
    }

    public void CraftWeapon(CraftingRecipe recipe)
    {
        if (!HasOresNeeded(recipe))
        {
            Debug.Log("Note enough ores to craft " + recipe.ItemName);
            return;
        }

        for (int i =0; i< recipe.OreTypesNeeded.Length; i++)
        {
            OreType oreType = recipe.OreTypesNeeded[i];
            int amountNeeded = recipe.AmountNeeded[i];
            inventory.RemoveOre(amountNeeded, oreType);
        }

        inventory.addWeapon(recipe.WeaponSO);
        Debug.Log("Weapon " + recipe.WeaponSO.weaponName + "has been crafted.");
    }
}