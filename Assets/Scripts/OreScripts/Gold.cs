using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : OreBlock
{
    [SerializeField] private string type = "Gold";

    public string getType()
    {
        return type;
    }

}
