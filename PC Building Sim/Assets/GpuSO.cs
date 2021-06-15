using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGpu" , menuName ="PC Components/GPU")]
public class GpuSO : ScriptableObject
{
    public string cName;
    public float cPrice;
    [System.Serializable]
    public struct memorySpecs
    {
        public int size;
        public string type;
        public int bandwidth;
        public int clock;
    }

    public memorySpecs memory;
    public GameObject gpuModel;

    public GpuSO(string cName, float cPrice, memorySpecs memory, GameObject gpuModel)
    {
        this.cName = cName;
        this.cPrice = cPrice;
        this.memory = memory;
        this.gpuModel = gpuModel;
    }
}
