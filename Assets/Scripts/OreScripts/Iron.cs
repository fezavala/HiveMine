using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iron : OreBlock
{
    [SerializeField] private string type = "Iron";

    public string getType()
    {
        return type;
    }
}
