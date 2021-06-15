using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PC Components/RAM")]
public class RamSO : ScriptableObject
{
    public string cName;
    public float cPrice;
    public GameObject ramModel;
}
