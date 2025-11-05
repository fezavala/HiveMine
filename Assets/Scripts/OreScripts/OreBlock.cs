using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreBlock : MonoBehaviour
{
    public OreData oreData;
    private Inventory inventory;
    [SerializeField] private int oreAmount;
    //[SerializeField] private bool isBroken = false;

    public int getOreAmount()
    {
        return oreAmount;
    }

    public void setOreAmount(int amount)
    {
        oreAmount = amount;
    }

    public void setBroken()
    {
        //isBroken = true;
        inventory.AddOre(oreAmount, oreData.oreType); 
        //Debug.Log("IM BEING DESTROYED");

    }

    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }

}
