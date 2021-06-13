using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GPU_Component : PC_Component
{
    [System.Serializable]
    public struct memorySpecs
    {
        public int size;
        public string type;
        public int bandwidth;
        public int clock;
    }

    public memorySpecs memory;
    public GPU_Component(string _cName, float _cPrice, int _size , string _type , int _bandwidth, int _clock)
    {      
        cName = _cName;
        cPrice = _cPrice;
        memory.size = _size;
        memory.type = _type;
        memory.bandwidth = _bandwidth;
        memory.clock = _clock;
    }
}
