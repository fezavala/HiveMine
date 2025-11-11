using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static TreeEditor.TreeEditorHelper;

public class CraftingBench : MonoBehaviour
{
    public static CraftingBench Instance;
    [SerializeField] private GameObject craftingMenuUI;
    [SerializeField] private GameObject recipeSlotUI;
    [SerializeField] private CraftingRecipe[] craftingRecipes;
    [SerializeField] private Inventory inventory;

    //[Header("RecipieUI")]
    //[SerializeField] private GameObject craftingMenuUI;

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

    public void OpenCraftingMenu()
    {
        craftingMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseCraftingMenu()
    {
        craftingMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public bool isMenuOpen()
    {
        return Time.timeScale == 0f;
    }

    //check if player had a crafting recipe
    public bool hasRecipe(CraftingRecipe recipe)
    {
        bool check = false;
        for (int i =0; i< craftingRecipes.Length; i++)
        {
            if (craftingRecipes[i] == recipe)
            {
                check = true;
            }
        }
        return check;
    }

    //check if player has enough of one ore type in inventory
    public bool hasOre(string oreType, int minAmount)
    {
        bool check = false;
        
        for (int i =0; i<inventory.oreDatas.Count; i++)
        {
            if (inventory.oreDatas[i].AmountInInventory >= minAmount)
            {
                check = true;
                Debug.Log("Inventory has enough "  + inventory.oreDatas[i].oreType + ": "+ inventory.oreDatas[i].oreType);
            }
        }
        return check;
    }

    //add item to player incentory and remove materials from inventory
    public void craftRecipe(CraftingRecipe recipe)
    {
        if (!hasRecipe(recipe))
        {
            Debug.Log("Player does not have crafting recipe for " + recipe.ItemName);
            return;
        }
        bool allOresAvailable = true;

        for (int i =0; i< recipe.OreTypes.Length; i++)
        {
            OreList requiredOre = recipe.OreTypes[i];
            int requiredAmount = recipe.AmountNeeded[i];

            if (!hasOre(requiredOre.ToString(), requiredAmount))
            {
                allOresAvailable = false;
                Debug.Log("Player does not have enough ore " + recipe.AmountNeeded[i]);
            }
        }

        if (!allOresAvailable)
        {
            return;
        }

        //add item to inventory
        //inventory.addWeapon(recipe.ItemName);
        Debug.Log("weapon has been added to inventory");
        for (int i =0; i<recipe.OreTypes.Length; i++)
        {
            OreList reqiredOre = recipe.OreTypes[i];
            int requiredAmount = recipe.AmountNeeded[i];

            inventory.RemoveOre(requiredAmount, reqiredOre.ToString());
        }
        Debug.Log("Crafted: " + recipe.ItemName);
        /*
        bool checkRecipe = false;
        bool checkAllOres = false;
        if (hasRecipe(recipe) == true) //if the player has this recipe
        {
            checkRecipe = true;
            for (int i =0; i< inventory.oreDatas.Count; i++) //go through each ore type in the inventory
            {
                if (inventory.oreDatas[i].oreType == recipe.OreTypes[i].ToString()) //if the recipe calls for this ore
                {
                    if(hasOre(recipe.OreTypes[i].ToString(), recipe.AmountNeeded[i])) //check if there is enough of it
                    {
                        //as long as all ores needed in recipe are avalable, item is craftable
                    }
                }
            }
            if (checkRecipe && checkAllOres)
            {
                //inventory.addWeapon(recipe.ItemName); TODO: need to make sure names are final for this to work

                //remove ore from inventory
                inventory.RemoveOre(recipe.AmountNeeded[i]); //how to do this for every item? already out of for loop

            }
        }
        */
    }

    public void createRecipieListUI()
    {
        if (craftingRecipes.Length == 0) //if player has no recipes
        {
            GameObject textBox = new GameObject("NoRecipesText");
            textBox.transform.SetParent(craftingMenuUI.transform, false);

            TMP_Text text = textBox.AddComponent<TextMeshProUGUI>();
            text.text = "No crafting recipes found";
            text.fontSize = 32;
            text.alignment = TextAlignmentOptions.Center;
        }
        else
        {
            Debug.Log("Player has recipies");
            List<GameObject> rSlots = new List<GameObject>();
            //Update UI
            for (int i = 0; i < craftingRecipes.Length; i++)
            {
                //create a new slot in the Menu
                //rSlots.Add(recipeSlotUI);
                
                GameObject slotObject = Instantiate(recipeSlotUI, craftingMenuUI.transform);
                RecipeSlot slot = slotObject.GetComponent<RecipeSlot>();
                slot.SetReceipe(craftingRecipes[i]);
                
            }
        }
    }

    // Update is called once per frame
    //void Update()
    //{
    //    bool click = Input.GetKeyDown(KeyCode.Mouse0);
    //    if (click && !isMenuOpen())
    //    {
    //        createRecipieListUI();
    //        OpenCraftingMenu(); //COMMENT THIS OUT TO TEST OTHER STUFF FOR NOW
    //    }


    //}
}
