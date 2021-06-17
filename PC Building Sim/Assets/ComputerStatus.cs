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
    public RAM_Component mountedRam;
    public RAM_Component mountedRam1;
    public RAM_Component mountedRam2;
    public RAM_Component mountedRam3;
    public RAM_Component mountedRam4;
    public CPU_Component mountedCpu;
    private GpuSO averageGpu;
    private CpuSO averageCpu;
    private RamSO averageRam;
    public float gpuPerformance;
    public float cpuPerformance;
    public float ramPerformance;
    public float totalPerformance;

    public bool HasGpu { get => hasGpu; set => hasGpu = value; }
    public bool HasMotherboard { get => hasMotherboard; set => hasMotherboard = value; }
    public bool HasRam1 { get => hasRam1; set => hasRam1 = value; }
    public bool HasRam2 { get => hasRam2; set => hasRam2 = value; }
    public bool HasRam3 { get => hasRam3; set => hasRam3 = value; }
    public bool HasRam4 { get => hasRam4; set => hasRam4 = value; }

    public bool HasCpu { get => hasCpu; set => hasCpu = value; }

    private void Start()
    {
        averageCpu = new CpuSO(6, 12, 4, 3, "any", 14, 12, 65);
        averageGpu = new GpuSO("averageGpu", 69, new GpuSO.memorySpecs(6, "GDDR6", 192, 1300), new GameObject());
        averageRam = new RamSO(50, "DDR4", 4, 16, 1.35f, 2666);
    }
    public bool TryStart()
    {
        //temporary , need to add component compatibility check
        if (hasCpu && hasGpu && hasMotherboard && hasRam1)
        {
            CalculatePerformance();
            return true;
        }
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

    public void CalculatePerformance()
    {
        CalculateCpuPerformance();
        CalculateGpuPerformance();
        CalculateRamPerformance();
        totalPerformance = (ramPerformance + cpuPerformance + gpuPerformance);
        Debug.Log("mountedcpuname:" + mountedCpu.cpuSpecs.cName);
        Debug.Log("moutedcpuscore:" + cpuPerformance);
    }

    public void CalculateRamPerformance()
    {
        //float channelA = mountedRam1.ramSpecs.frequency * mountedRam3.ramSpecs.frequency;
        //float channelB = mountedRam2.ramSpecs.frequency * mountedRam4.ramSpecs.frequency;
        //ramPerformance = ((channelA + channelB)*mountedRam.ramSpecs.memorySize) / mountedRam.ramSpecs.latency;
        ramPerformance = (mountedRam.ramSpecs.frequency - averageRam.frequency)/100;
        ramPerformance += averageRam.latency -  mountedRam.ramSpecs.latency; 
    }
    public void CalculateCpuPerformance()
    {
        float ramInfluence = 0;
        if (mountedRam.ramSpecs.frequency < 3200 && mountedRam.ramSpecs.manufacturer == "AMD")
            ramInfluence = (3200 - mountedRam.ramSpecs.frequency) / 10;
        cpuPerformance = mountedCpu.cpuSpecs.cores - averageCpu.cores * 10;
        cpuPerformance += mountedCpu.cpuSpecs.topClock - averageCpu.topClock;
        cpuPerformance += ramInfluence + 60;

    }

    public void CalculateGpuPerformance()
    {
        gpuPerformance = (mountedGpu.gpuSpecs.memory.size - averageGpu.memory.size) * 10;
        gpuPerformance += (mountedGpu.gpuSpecs.coreClock - averageGpu.coreClock) / 100;
    }
}
