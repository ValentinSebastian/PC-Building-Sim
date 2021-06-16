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
}
