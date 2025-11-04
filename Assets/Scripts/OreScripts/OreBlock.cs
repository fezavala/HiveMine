using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreBlock : MonoBehaviour
{
    [SerializeField] private int oreAmount;
    private bool isBroken = false;

    public int getOreAmount()
    {
        return oreAmount;
    }

    public void setOreAmount(int amount)
    {
        oreAmount = amount;
    }

}
