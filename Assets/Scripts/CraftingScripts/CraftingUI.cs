using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingUI : MonoBehaviour
{
    [SerializeField] private GameObject craftingMenuUI;
    [SerializeField] private GameObject recipeSlotPrefab;
    [SerializeField] private Transform recipeContainer;
    private CraftingSystem craftingSystem;

    // Start is called before the first frame update
    void Start()
    {
        craftingSystem = FindObjectOfType<CraftingSystem>();   
    }

    public void ShowCraftingMenu(List<CraftingRecipe> recipes)
    {
        craftingMenuUI.SetActive(true);
        Time.timeScale = 0f;
        ClearOldSlots();

        foreach (CraftingRecipe recipe in recipes)
        {
            GameObject slotObj = Instantiate(recipeSlotPrefab, recipeContainer);
            RecipeSlot slot = slotObj.GetComponent<RecipeSlot>();
            slot.SetReceipe(recipe);
            slot.SetCraftAction(() => craftingSystem.CraftWeapon(recipe));
        }
    }

    private void ClearOldSlots()
    {
        foreach (Transform child in recipeContainer)
        {
            Destroy(child.gameObject);
        }
    }

    public void CloseCraftingMenu()
    {
        craftingMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public bool IsMenuOpen()
    {
        return Time.timeScale == 0f;
    }
}
