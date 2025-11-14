using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    [SerializeField] private Inventory inventory;

    public bool HasOresNeeded(CraftingRecipe recipe)
    {
        // Link type and amount
        Dictionary<OreType, int> totals = new Dictionary<OreType, int>();

        for (int i = 0; i < inventory.oreCounts.Count; i++)
        {
            OreType type = inventory.oreTypeList[i];
            int amount = inventory.oreCounts[i];

            if (!totals.ContainsKey(type))
                totals[type] = 0;

            totals[type] += amount;
        }

        // Check each recipe requirement
        for (int i = 0; i < recipe.OreTypesNeeded.Length; i++)
        {
            OreType requiredType = recipe.OreTypesNeeded[i];
            int requiredAmount = recipe.AmountNeeded[i];

            totals.TryGetValue(requiredType, out int availableAmount);

            if (availableAmount < requiredAmount)
            {
                Debug.Log("Missing or insufficient" + requiredType + ". Need " + requiredAmount + ", have " + availableAmount);
                return false;
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