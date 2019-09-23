using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColorControl : MonoBehaviour 
{
    [SerializeField]
    public ColorData[] data;
    int count = 0;

    void Start()
    {
        count = Random.Range(0, data.Length);
    }

    public ColorData GetColorData(int index)
    {
        return data[index];
    }

    public ColorData GetRandColorData()
    {
        int index = count % data.Length;
        count++;
        return data[index];
    } 

    public ColorData GetNextColorData()
    {
        int index = count % data.Length;
        return data[index];
    } 
}
