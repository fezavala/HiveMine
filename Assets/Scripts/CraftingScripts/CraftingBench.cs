using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CraftingBench : MonoBehaviour, IUsable
{
    public static CraftingBench Instance { get; private set; }
    [SerializeField] private CraftingUI craftingUI;
    [SerializeField] private CraftingRecipe[] avalableRecipes;
    [SerializeField] private Inventory inventory;

    private void Start()
    {
        craftingUI = FindObjectOfType<CraftingUI>();
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Interact()
    {
        craftingUI.ShowCraftingMenu(inventory.recipes);
    }

    public void GetAvalableRecipies()
    {
        for (int i =0; i< inventory.recipes.Count; i++)
        {
            avalableRecipes[i] = inventory.recipes[i];
        }
    }

}
