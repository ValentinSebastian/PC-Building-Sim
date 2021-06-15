using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerStatus : MonoBehaviour
{
    private bool hasGpu;
    private bool hasMotherboard;
    private bool hasRam1;
    private bool hasRam2;
    private bool hasRam3;
    private bool hasRam4;
    private bool hasCpu;
    public Motherboard_Component mountedMotherboard;
    public GPU_Component mountedGpu;
    public RAM_Component mountedRam1;
    public RAM_Component mountedRam2;
    public RAM_Component mountedRam3;
    public RAM_Component mountedRam4;
    public CPU_Component mountedCpu;

    public bool HasGpu { get => hasGpu; set => hasGpu = value; }
    public bool HasMotherboard { get => hasMotherboard; set => hasMotherboard = value; }
    public bool HasRam1 { get => hasRam1; set => hasRam1 = value; }
    public bool HasRam2 { get => hasRam2; set => hasRam2 = value; }
    public bool HasRam3 { get => hasRam3; set => hasRam3 = value; }
    public bool HasRam4 { get => hasRam4; set => hasRam4 = value; }

    public bool HasCpu { get => hasCpu; set => hasCpu = value; }

    public bool TryStart()
    {
        //temporary , need to add component compatibility check
        if (hasCpu && hasGpu && hasMotherboard && hasRam1)
            return true;
        else
            return false;
    }

    public bool MotherboardHasComponents()
    {
        if (hasCpu || hasGpu || hasRam1 || hasRam2 || hasRam3 || hasRam4)
            return true;
        else
            return false;
    }
}
