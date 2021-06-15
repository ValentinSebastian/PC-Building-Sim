using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PC Components/Motherboard")]
public class MotherboardSO : ScriptableObject
{
    public string cName;
    public float cPrice;
    public GameObject motherboardModel;
}
