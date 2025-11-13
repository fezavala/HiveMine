using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OreType
{
    Iron,
    Gold,
    Ruby,
    Rare
}

public class OreBlock : MonoBehaviour
{
    //public OreData oreData;
    private Inventory inventory;
    [SerializeField] protected OreType oreType;
    [SerializeField] private int oreAmount;

    public int getOreAmount()
    {
        return oreAmount;
    }

    public void setOreAmount(int amount)
    {
        oreAmount = amount;
    }
    public OreType GetOreType()
    {
        return oreType;
    }

    public void setBroken()
    { 
        inventory.AddOre(oreAmount, oreType);
        //Debug.Log("IM BEING DESTROYED");

    }

    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }

}
