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
    public GameObject inventoryScreenUI;
    [Header("OreCounters")] 
    public TextMeshProUGUI[] oreCounters;
    //private Dictionary<OreData, int> ores = new Dictionary<OreData, int>();
    //private int ironCount = 0;
    //private int goldCount = 0;
    //private int rubyCount = 0;
    //private int rareCount = 0;
    [Header("OreInventorys")]
    public OreData IronOre;
    public OreData GoldOre;
    public OreData RubyOre;
    public OreData RareOre;
    
    public void AddOre(int amount, string type)
    {
        if (type == "Iron")
        {
            IronOre.AmountInInventory += amount;
        }else if (type == "Gold")
        {
            GoldOre.AmountInInventory += amount;
        }else if (type == "Ruby")
        {
            RubyOre.AmountInInventory += amount;
        }else if (type == "Rare")
        {
            RareOre.AmountInInventory += amount;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Update counter on UI
        oreCounters[0].text = IronOre.AmountInInventory.ToString();
        oreCounters[1].text = GoldOre.AmountInInventory.ToString();
        oreCounters[2].text = RubyOre.AmountInInventory.ToString();
        oreCounters[3].text = RareOre.AmountInInventory.ToString();
    }
}
