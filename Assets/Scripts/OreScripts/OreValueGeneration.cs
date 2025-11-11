using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "OreGenerationScript", menuName = "ScriptableObjects/OreGenerationScript")]
public class OreValueGeneration : ScriptableObject
{
    //public String oreType;
    public int totalAmount;
    //public GameObject[] oreObjects;

    public void GenerateOreValues(OreBlock [] oreObjects)
    {
        //GameObject[] oreObjects = GameObject.FindGameObjectsWithTag(oreType);
        //OreBlock[] oreObjects = oreType; //might need this one
        //if (oreObjects.Length == 0)
        //{
            //Debug.LogWarning("No objects with " + oreType + " tag.");
        //}

        int[] oreValues = WeightedRandomGen(totalAmount, oreObjects.Length);
        for (int i = 0; i < oreObjects.Length; i++)
        {
            var block = oreObjects[i].GetComponent<OreBlock>();
            if (block != null)
            {
                //block.oreAmount = oreValues[i];
                block.setOreAmount(oreValues[i]);
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
            result[i] = Mathf.Max(1, Mathf.RoundToInt(totalAmount * (weights[i]/weightSum)));
            assigned += result[i];
        }

        //last one
        result[count-1] = totalAmount - assigned;

        if (result[count - 1] <= 0)
        {
            result[count - 1] = 1;
            // reduce one randomly from others to keep total correct
            int reduceIndex = UnityEngine.Random.Range(0, count - 1);
            if (result[reduceIndex] > 1)
                result[reduceIndex]--;
        }
        

        return result;
    }
}
