using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour
{
    // Singleton pattern for inventory since there will only be 1 inventory
    public static Inventory Instance { get; private set; }

    [SerializeField] private GameInput gameInput;
    [SerializeField] private GameObject inventoryScreenUI;
    public List<OreType> oreTypeList = new List<OreType>
{
    OreType.Iron,
    OreType.Gold,
    OreType.Ruby,
    OreType.Rare
};

    [Header("OreCounters")]
    [SerializeField] private TextMeshProUGUI[] oreCounters;

    [Header("OreInventorys")]
    public List<int> oreCounts = new List<int>();

    [Header("CraftingRecipies")]
    public List<CraftingRecipe> recipes = new List<CraftingRecipe>();

    // Weapon inventory fields:
    [Header("WeaponInventory")]
    [SerializeField] private List<WeaponSO> weaponSOInventory;
    private int weaponSOIndex = 0;  // Indicates currently equipped weapon
    private bool frameOneWeaponSwap = true;  // Swaps weapons on frame 1 to currently equipped weapon

    // Event triggered by swapping out currently equipped weapon
    public event EventHandler<OnEquipableSwappedArgs> OnEquipableSwapped;
    public class OnEquipableSwappedArgs : EventArgs
    {
        public WeaponSO equipableSO;
    }

    private void Start()
    {
        gameInput.OnScrollAction += GameInput_OnScrollAction;  // Scroll input used for changing weapons
    }

    // Scroll Input now set to weapon swap
    private void GameInput_OnScrollAction(object sender, GameInput.OnScrollActionEventArgs e)
    {
        if (weaponSOInventory.Count > 0)
        {
            weaponSOIndex = (weaponSOIndex + e.scrollSign) % weaponSOInventory.Count;
            if (weaponSOIndex < 0) weaponSOIndex = weaponSOInventory.Count - 1;
            OnEquipableSwapped?.Invoke(this, new OnEquipableSwappedArgs
            {
                equipableSO = weaponSOInventory[weaponSOIndex]
            });
        }
    }

    private void Awake()
    {
        if (Instance !=null)
        {
            Debug.LogError("There is more than one Inventory instance!");
        }
        Instance = this;
    }

    public void AddOre(int amount, OreType type)
    {
        switch (type)
        {
            case OreType.Iron:
                oreCounts[0] += amount;
                break;
            case OreType.Gold:
                oreCounts[1] += amount;
                break;
            case OreType.Ruby:
                oreCounts[2] += amount;
                break;
            case OreType.Rare:
                oreCounts[3] += amount;
                break;
        }
    }

    public void RemoveOre(int amount, OreType type)
    {
        switch (type)
        {
            case OreType.Iron:
                oreCounts[0] -= amount;
                break;
            case OreType.Gold:
                oreCounts[1] -= amount;
                break;
            case OreType.Ruby:
                oreCounts[2] -= amount;
                break;
            case OreType.Rare:
                oreCounts[3] -= amount;
                break;
        }
    }

    public void AddRecipie(CraftingRecipe recipe)
    {
        recipes.Add(recipe);
    }

    public void RemoveRecipie(CraftingRecipe recipe)
    {
        recipes.Remove(recipe);
    }

    public void addWeapon(WeaponSO weaponSO)
    {
        weaponSOInventory.Add(weaponSO);
    }

    public void removeWeapon(WeaponSO weaponSO) 
    {
        // If the weapon being removed is the currently equipped weapon, then update the players weapon to be empty
        if (weaponSOInventory[weaponSOIndex] == weaponSO)
        {
            OnEquipableSwapped?.Invoke(this, new OnEquipableSwappedArgs
            {
                equipableSO = null
            });
        }
        weaponSOInventory.Remove(weaponSO); 
    }

    // Update is called once per frame
    void Update()
    {
        // Temporary weapon swap at start of game to show equipable object immediately
        if (frameOneWeaponSwap)
        {
            // If the weapon inventory has tools, let the player equip it
            if (weaponSOInventory.Count > 0)
            {
                OnEquipableSwapped?.Invoke(this, new OnEquipableSwappedArgs
                {
                    equipableSO = weaponSOInventory[weaponSOIndex]
                });
            }
            frameOneWeaponSwap = false;
        }

        //Update counter on UI
        //TODO: This would benefit from using an event to update the visuals when an ore is collected, instead of every frame
        oreCounters[0].text = oreCounts[0].ToString();
        oreCounters[1].text = oreCounts[1].ToString(); 
        oreCounters[2].text = oreCounts[2].ToString(); 
        oreCounters[3].text = oreCounts[3].ToString();
    }
}
