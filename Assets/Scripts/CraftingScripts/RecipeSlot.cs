using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class RecipeSlot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UnityEngine.UI.Image recipeIcon;
    [SerializeField] private TextMeshProUGUI recipeName;
    [SerializeField] private List<GameObject> materialSlotUI;
    [SerializeField] private int[] neededAmout;
    [SerializeField] private GameObject materialSlotPrefab;
    [SerializeField] private Transform materialContainer;
    private UnityEngine.UI.Button craftButton;
    private Action onCraftClicked;

    public void SetReceipe(CraftingRecipe recipe)
    {
        //set name
        recipeName.text = recipe.ItemName;
        
        //set icon
        
        if (recipe.itemIcon != null) 
            recipeIcon.sprite = recipe.itemIcon;
        else
            recipeIcon.sprite = null;

        //Refresh materials
        foreach (Transform child in materialContainer)
        {
            Destroy(child.gameObject);
        }
        materialSlotUI.Clear();

        //set ore and amount needed
        for (int i = 0; i< recipe.OreTypesNeeded.Length; i++)
        {
            GameObject newSlot = Instantiate(materialSlotPrefab, materialContainer);
            materialSlotUI.Add(newSlot);

            TMP_Text text = newSlot.GetComponentInChildren<TextMeshProUGUI>();
            text.text = $"{recipe.OreTypesNeeded[i]} x{recipe.AmountNeeded[i]}";

        }
    }

    public void SetCraftAction(Action craftAction)
    {
        // Find the Button automatically only once
        if (craftButton == null)
        {
            craftButton = GetComponentInChildren<UnityEngine.UI.Button>();
            if (craftButton == null)
            {
                Debug.LogError("No Button found in RecipeSlot prefab!");
                return;
            }
        }

        // Refresh
        craftButton.onClick.RemoveAllListeners();

        onCraftClicked = craftAction;

        craftButton.onClick.AddListener(() =>
        {
            onCraftClicked?.Invoke();
        });
    }
}
