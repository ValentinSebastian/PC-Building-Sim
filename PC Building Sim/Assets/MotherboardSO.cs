using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PC Components/Motherboard")]
public class MotherboardSO : ScriptableObject
{
    public string cName;
    public string manufacturer;
    public float cPrice;
    public string cpuSocket;
    public string formFactor;
    public int memorySlots;
    public string memoryType;
    public string memoryMaxFrequency;
    public int memoryMax;
    public string audioChip;
    public int m2Slots;
    public string pci_eSlots;
    public GameObject motherboardModel;
    public string url;
}
