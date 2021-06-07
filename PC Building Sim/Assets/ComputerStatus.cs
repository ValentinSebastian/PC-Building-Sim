using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerStatus : MonoBehaviour
{
    private bool hasGpu;
    private bool hasMotherboard;
    private bool hasRam;
    private bool hasCpu;
    public Motherboard_Component mountedMotherboard;
    public GPU_Component mountedGpu;

    public bool HasGpu { get => hasGpu; set => hasGpu = value; }
    public bool HasMotherboard { get => hasMotherboard; set => hasMotherboard = value; }
    public bool HasRam { get => hasRam; set => hasRam = value; }
    public bool HasCpu { get => hasCpu; set => hasCpu = value; }

    public bool TryStart()
    {
        //temporary , need to add component compatibility check
        if (hasCpu && hasGpu && hasMotherboard && hasRam)
            return true;
        else
            return false;
    }

    public bool MotherboardHasComponents()
    {
        if (hasCpu || hasGpu || hasRam)
            return true;
        else
            return false;
    }
}
