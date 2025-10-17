using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreValueGeneration : MonoBehaviour
{
    public String oreType;
    public int totalAmount;
    //public GameObject[] oreObjects;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] oreObjects = GameObject.FindGameObjectsWithTag(oreType);

        if (oreObjects.Length == 0)
        {
            Debug.LogWarning("No objects with " + oreType + " tag.");
        }

        int[] oreValues = WeightedRandomGen(totalAmount, oreObjects.Length);
        for (int i = 0; i < oreObjects.Length; i++)
        {
            var block = oreObjects[i].GetComponent<OreBlock>();
            if (block != null)
            {
                block.oreAmount = oreValues[i];
            }
            Debug.Log(oreObjects[i].name + "gets " + oreValues[i]);
        }
    }

    private int[] WeightedRandomGen(int totalAmount, int count)
    {
        float[] weights = new float[count];
        float weightSum = 0f;

        for (int i =0; i<count; i++)
        {
            weights[i] = UnityEngine.Random.value;
            weightSum += weights[i];
        }
        
        int[] result = new int[count];
        int assigned = 0;

        for (int i =0; i<count-1;i++)
        {
            result[i] = Mathf.RoundToInt(totalAmount * (weights[i]/weightSum));
            assigned += result[i];
        }
        //last one
        result[count-1] = totalAmount - assigned;
        return result;
    }
}
