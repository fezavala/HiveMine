using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreManager : MonoBehaviour
{
    public OreValueGeneration ironOreGenerator;
    public OreValueGeneration GoldOreGenerator;
    public OreValueGeneration RubyOreGenerator;
    public OreValueGeneration RareOreGenerator;
    // Start is called before the first frame update
    void Start()
    {
        ironOreGenerator.GenerateOreValues();
        GoldOreGenerator.GenerateOreValues();
        RubyOreGenerator.GenerateOreValues();
        RareOreGenerator.GenerateOreValues();
    }
}
