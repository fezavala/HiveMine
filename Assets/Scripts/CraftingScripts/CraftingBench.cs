using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static TreeEditor.TreeEditorHelper;

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
        GetAvalableRecipies();
        craftingUI.ShowCraftingMenu(avalableRecipes);
    }

    public void GetAvalableRecipies()
    {
        for (int i =0; i< inventory.recipes.Count; i++)
        {
            avalableRecipes[i] = inventory.recipes[i];
        }
    }

}
