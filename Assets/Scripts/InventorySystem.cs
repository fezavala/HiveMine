using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryScreenUI;
    private int ironCount = 0;
    private int goldCount = 0;
    private int rubyCount = 0;
    private int rareCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        //need to check when a oreBlock gets destroyed, before its destroyed add the amount to the counter
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
