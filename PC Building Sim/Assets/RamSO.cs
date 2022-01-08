using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PC Components/RAM")]
public class RamSO : ScriptableObject
{
    public string cName;
    public string manufacturer;
    public float cPrice;
    public string memoryType;
    public int memorySize;
    public float latency;
    public float voltage;
    public float frequency;
    public GameObject ramModel;

    public void UpdateValues(float cPrice, string memoryType, int memorySize, float latency, float voltage, float frequency)
    {
        this.cPrice = cPrice;
        this.memoryType = memoryType;
        this.memorySize = memorySize;
        this.latency = latency;
        this.voltage = voltage;
        this.frequency = frequency;
    }
}
