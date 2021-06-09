using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_Component : MonoBehaviour
{
    public string cName;
    public float cPrice;
    
    public enum ComponentType
    {
        GPU,
        CPU,
        Motherboard,
        RAM
    }
}
