using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GPU_Component : PC_Component
{
    public float vRam;

    public GPU_Component(string _cName, float _cPrice,  float _vRam)
    {
        vRam = _vRam;
        cName = _cName;
        cPrice = _cPrice;     
    }
}
