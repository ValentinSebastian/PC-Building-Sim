using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGpu" , menuName ="PC Components/GPU")]
public class GpuSO : ScriptableObject
{
    public string cName;
    public string manufacturer;
    public float cPrice;
    public string pci_eSlot;
    public int coreClock;
    public int shaderCount;
    [System.Serializable]
    public struct memorySpecs
    {
        public int size;
        public string type;
        public int bandwidth;
        public int clock;
        public memorySpecs(int _size, string _type, int _bandwidth, int _clock)
        {
            size = _size;
            type = _type;
            bandwidth = _bandwidth;
            clock = _clock;
        }//memorySpecs(6, "GDDR6", 192, 1300)
    }

    public memorySpecs memory;
    public GameObject gpuModel;
    public string url;

    public void UpdateValues(string cName, float cPrice, int coreClock , int shaderCount , memorySpecs memory, GameObject gpuModel)
    {
        this.cName = cName;
        this.cPrice = cPrice;
        this.memory = memory;
        this.gpuModel = gpuModel;
        this.coreClock = coreClock;
        this.shaderCount = shaderCount;
    }
}
