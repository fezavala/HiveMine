using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruby : OreBlock
{
    [SerializeField] private string type = "Ruby";

    public string getType()
    {
        return type;
    }

}
