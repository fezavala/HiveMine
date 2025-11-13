using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class RecipeSlot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image recipeIcon;
    [SerializeField] private TextMeshProUGUI recipeName;
    [SerializeField] private List<GameObject> materialSlotUI;
    [SerializeField] private int[] neededAmout;
    [SerializeField] private GameObject materialSlotPrefab;
    [SerializeField] private Button craftButton;
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
        
        //set ore and amount needed
        for (int i = 0; i< recipe.OreTypesNeeded.Length; i++)
        {
            GameObject newSlot = materialSlotPrefab;
            materialSlotUI.Add(newSlot);
            TMP_Text text = materialSlotUI[i].GetComponentInChildren<TextMeshProUGUI>();
            text.text = $"{recipe.OreTypesNeeded[i]} x{recipe.AmountNeeded[i]}";
        }
    }

    public void SetCraftAction(Action craftAction)
    {
        //craftButton.onClick.RemoveAllListeners();
        onCraftClicked = craftAction;
        //craftButton.onClick.AddListener(() => onCraftClicked?.Invoke());
    }
}
