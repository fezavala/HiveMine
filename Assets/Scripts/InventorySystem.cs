using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
using Unity.VisualScripting;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject inventoryScreenUI;
    
    [Header("WeaponInventory")]
    [SerializeField] private List<WeaponSO> weapons = new List<WeaponSO>();

    [Header("OreCounters")]
    [SerializeField] private TextMeshProUGUI[] oreCounters;
    //private Dictionary<OreData, int> ores = new Dictionary<OreData, int>();

    [Header("OreInventorys")]
    public List<OreData> oreDatas = new List<OreData>();
    //public OreData IronOre;
    //public OreData GoldOre;
    //public OreData RubyOre;
    //public OreData RareOre;

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
        weapons.Add(weaponSO);
    }

    public void removeWeapon(WeaponSO weaponSO) 
    {
        weapons.Remove(weaponSO); 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Update counter on UI
        oreCounters[0].text = oreDatas[0].AmountInInventory.ToString();
        oreCounters[1].text = oreDatas[1].AmountInInventory.ToString();
        oreCounters[2].text = oreDatas[2].AmountInInventory.ToString();
        oreCounters[3].text = oreDatas[3].AmountInInventory.ToString();
    }
}
