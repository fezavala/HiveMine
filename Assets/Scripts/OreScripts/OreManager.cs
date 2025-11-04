using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreManager : MonoBehaviour
{
    public OreValueGeneration ironOreGenerator;
    public OreValueGeneration goldOreGenerator;
    public OreValueGeneration rubyOreGenerator;
    public OreValueGeneration rareOreGenerator;

    private OreBlock[] ironBlocks;
    private OreBlock[] goldBlocks;
    private OreBlock[] rubyBlocks;
    private OreBlock[] rareBlocks;
    void Start()
    {
        ironBlocks = FindObjectsOfType<Iron>();
        Debug.Log("number of iron blocks is: " + ironBlocks.Length);
        ironOreGenerator.GenerateOreValues(ironBlocks);

        goldBlocks = FindObjectsOfType<Gold>();
        Debug.Log("number of gold blocks is: " + goldBlocks.Length);
        goldOreGenerator.GenerateOreValues(goldBlocks);

        rubyBlocks = FindObjectsOfType<Ruby>();
        Debug.Log("number of ruby blocks is: " + rubyBlocks.Length);
        rubyOreGenerator.GenerateOreValues(rubyBlocks);

        rareBlocks = FindObjectsOfType<Rare>();
        Debug.Log("number of rare blocks is: " + rareBlocks.Length);
        rareOreGenerator.GenerateOreValues(rareBlocks);
    }
}
