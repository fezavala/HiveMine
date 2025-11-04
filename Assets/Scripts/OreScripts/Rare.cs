using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rare : OreBlock
{
    [SerializeField] private string type = "Rare";

    public string getType()
    {
        return type;
    }

}
