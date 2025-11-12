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

    [SerializeField] private GameObject inventoryScreenUI;

    [Header("OreCounters")]
    [SerializeField] private TextMeshProUGUI[] oreCounters;
    //private Dictionary<OreData, int> ores = new Dictionary<OreData, int>();

    [Header("OreInventorys")]
    public List<OreData> oreDatas = new List<OreData>();
    //public OreData IronOre;
    //public OreData GoldOre;
    //public OreData RubyOre;
    //public OreData RareOre;


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

    }

    private void TestWeaponSwap()
    {
        bool swapWeapon = Input.GetKeyDown(KeyCode.F);
        if (swapWeapon)
        {
            if (weaponSOInventory.Count > 0)
            {
                weaponSOIndex = (weaponSOIndex + 1) % weaponSOInventory.Count;
                OnEquipableSwapped?.Invoke(this, new OnEquipableSwappedArgs
                {
                    equipableSO = weaponSOInventory[weaponSOIndex]
                });
            }
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

    public void AddOre(int amount, string type)
    {
        for (int i = 0; i< oreDatas.Count; i++) { 
            if (type == oreDatas[i].oreType)
            {
                oreDatas[i].AmountInInventory += amount;
            } else if (type == oreDatas[i].oreType)
            {
                oreDatas[i].AmountInInventory += amount;
            } else if (type == oreDatas[i].oreType)
            {
                oreDatas[i].AmountInInventory += amount;
            } else if (type == oreDatas[i].oreType)
            {
                oreDatas[i].AmountInInventory += amount;
            }
        }
       /*
        if (ores.ContainsKey(ore))
        {
            ores[ore]++;
        }
        else
        {
            ores[ore] = 1;
        }
        Debug.Log($"Added {ore.name} to inv. Count: {ores[ore]}");
       */
    }

    public void RemoveOre(int amount, string type)
    {
        for (int i = 0; i < oreDatas.Count; i++)
        {
            if (type == "Iron")
            {
                oreDatas[i].AmountInInventory -= amount;
            }
            else if (type == "Gold")
            {
                oreDatas[i].AmountInInventory -= amount;
            }
            else if (type == "Ruby")
            {
                oreDatas[i].AmountInInventory -= amount;
            }
            else if (type == "Rare")
            {
                oreDatas[i].AmountInInventory -= amount;
            }
        }
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
        oreCounters[0].text = oreDatas[0].AmountInInventory.ToString();
        oreCounters[1].text = oreDatas[1].AmountInInventory.ToString();
        oreCounters[2].text = oreDatas[2].AmountInInventory.ToString();
        oreCounters[3].text = oreDatas[3].AmountInInventory.ToString();

        TestWeaponSwap();
    }
}
