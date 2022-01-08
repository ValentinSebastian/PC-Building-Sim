using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerStatus : MonoBehaviour
{
    private bool hasGpu;
    private bool hasMotherboard;
    private bool hasRam1;
    private bool hasRam2;
    private bool hasRam3;
    private bool hasRam4;
    private bool hasCpu;
    private bool hasCooler;
    public Motherboard_Component mountedMotherboard;
    public GPU_Component mountedGpu;
    public RAM_Component mountedRam;
    public RAM_Component mountedRam1;
    public RAM_Component mountedRam2;
    public RAM_Component mountedRam3;
    public RAM_Component mountedRam4;
    public CPU_Component mountedCpu;
    public Cooler_Component mountedCooler;
    private GpuSO averageGpu;
    private CpuSO averageCpu;
    private RamSO averageRam;
    public float gpuPerformance;
    public float cpuPerformance;
    public float ramPerformance;
    public float totalPerformance;
    private const float averageComponentPerformancePercent = 50f;
    public Image ComputerStatusDisplay;
    public bool CSD_Hidden = true;
    private bool computerRunning = false;
    public TMPro.TextMeshProUGUI cpuStatusTextbox;
    public TMPro.TextMeshProUGUI gpuStatusTextbox;
    public TMPro.TextMeshProUGUI moboStatusTextbox;
    public TMPro.TextMeshProUGUI ramStatusTextbox;
    public TMPro.TextMeshProUGUI fanStatusTextbox;

    public bool HasGpu { get => hasGpu; set => hasGpu = value; }
    public bool HasMotherboard { get => hasMotherboard; set => hasMotherboard = value; }
    public bool HasRam1 { get => hasRam1; set => hasRam1 = value; }
    public bool HasRam2 { get => hasRam2; set => hasRam2 = value; }
    public bool HasRam3 { get => hasRam3; set => hasRam3 = value; }
    public bool HasRam4 { get => hasRam4; set => hasRam4 = value; }

    public bool HasCpu { get => hasCpu; set => hasCpu = value; }

    public bool HasCooler { get => hasCooler; set => hasCooler = value; }

    private void Start()
    {
        averageCpu = ScriptableObject.CreateInstance<CpuSO>();
        averageGpu = ScriptableObject.CreateInstance<GpuSO>();
        averageRam = ScriptableObject.CreateInstance<RamSO>();
        averageCpu.UpdateValues(6, 12, 4, 3, "any", 14, 12, 65);
        averageGpu.UpdateValues("averageGpu", 69, 1300, 1500, new GpuSO.memorySpecs(6, "GDDR6", 192, 1300), new GameObject());
        averageRam.UpdateValues(50, "DDR4", 4, 16, 1.35f, 2666);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (CSD_Hidden)
            {
                ComputerStatusDisplay.transform.LeanMoveX(ComputerStatusDisplay.transform.position.x - ComputerStatusDisplay.rectTransform.rect.width * 2, 0.3f);
                CSD_Hidden = false;
                Debug.Log("WTF1");
            }
            else
            {
                ComputerStatusDisplay.transform.LeanMoveX(ComputerStatusDisplay.transform.position.x + ComputerStatusDisplay.rectTransform.rect.width * 2, 0.3f);
                CSD_Hidden = true;
                Debug.Log("WTF2");
            }
        }
        if (HasCpu)
        {
            cpuStatusTextbox.text = "Connected";
            cpuStatusTextbox.color = Color.green;
        }
        else
        {
            cpuStatusTextbox.text = "NOT CONNECTED";
            cpuStatusTextbox.color = Color.red;
        }
        if (HasGpu)
        {
            gpuStatusTextbox.text = "Connected";
            gpuStatusTextbox.color = Color.green;
        }
        else
        {
            gpuStatusTextbox.text = "NOT CONNECTED";
            gpuStatusTextbox.color = Color.red;
        }
        if (HasMotherboard)
        {
            moboStatusTextbox.text = "Connected";
            moboStatusTextbox.color = Color.green;
        }
        else
        {
            moboStatusTextbox.text = "NOT CONNECTED";
            moboStatusTextbox.color = Color.red;
        }
        if (HasRam1 || HasRam2 || HasRam3 || HasRam4)
        {
            ramStatusTextbox.text = "Connected";
            ramStatusTextbox.color = Color.green;
        }
        else
        {
            ramStatusTextbox.text = "NOT CONNECTED";
            ramStatusTextbox.color = Color.red;
        }
        if (HasCooler)
        {
            fanStatusTextbox.text = "Connected";
            fanStatusTextbox.color = Color.green;
        }
        else
        {
            fanStatusTextbox.text = "NOT CONNECTED";
            fanStatusTextbox.color = Color.red;
        }
    }
    public bool TryStart()
    {
        //temporary , need to add component compatibility check
        if (hasCpu && hasGpu && hasMotherboard && HasCooler && (hasRam1 || hasRam2 || hasRam3 || hasRam4))
        {
            CalculatePerformance();
            return true;
        }
        else
            return false;
    }

    public void StartComputerAnimations(bool state)
    {
        if (computerRunning && state)
            return;
        else
        {
            Animator[] fans = mountedGpu.GetComponentsInChildren<Animator>();
            foreach(Animator fan in fans)
            {
                fan.SetBool("StartRotation", state);
            }
            mountedCooler.GetComponentInChildren<Animator>().SetBool("StartRotation", state);
            computerRunning = true;
        }
    }
    public void StartComputerSounds(bool state)
    {
        if (mountedCooler.GetComponent<AudioSource>().isPlaying && state)
            return;
        else
        {
            if(state)
                mountedCooler.GetComponent<AudioSource>().Play(0);
            else
                mountedCooler.GetComponent<AudioSource>().Stop();
        }
    }
    public void StopComputer()
    {
        StartComputerAnimations(false);
        StartComputerSounds(false);
        computerRunning = false;
    }

    public bool ComputerIsRunning()
    {
        return computerRunning;
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
        ramPerformance = CalculatePercentWithMagnitude(mountedRam.ramSpecs.frequency, averageRam.frequency, 1);
        ramPerformance -= CalculatePercentWithMagnitude(mountedRam.ramSpecs.latency, averageRam.latency, 1);
        ramPerformance = averageComponentPerformancePercent + ramPerformance * averageComponentPerformancePercent;
    }
    public void CalculateCpuPerformance()
    {
        cpuPerformance = CalculatePercentWithMagnitude(mountedCpu.cpuSpecs.cores * mountedCpu.cpuSpecs.botClock, averageCpu.cores * averageCpu.botClock , 0.5f);
        cpuPerformance += CalculatePercentWithMagnitude(mountedCpu.cpuSpecs.cores * mountedCpu.cpuSpecs.topClock, averageCpu.cores * averageCpu.topClock, 0.5f);
        cpuPerformance += CalculatePercentWithMagnitude(mountedCpu.cpuSpecs.l3Cache , averageCpu.l3Cache, 0.3f);
        cpuPerformance = averageComponentPerformancePercent + cpuPerformance * averageComponentPerformancePercent;
    }

    public void CalculateGpuPerformance()
    {
        float memoryDiminishingReturns = 0.5f;
        if (mountedGpu.gpuSpecs.memory.size > 8)
            memoryDiminishingReturns = 0.2f;
        gpuPerformance = CalculatePercentWithMagnitude(mountedGpu.gpuSpecs.memory.size, averageGpu.memory.size, memoryDiminishingReturns);
        gpuPerformance += CalculatePercentWithMagnitude(mountedGpu.gpuSpecs.shaderCount, averageGpu.shaderCount, 0.3f);
        gpuPerformance += CalculatePercentWithMagnitude(mountedGpu.gpuSpecs.coreClock, averageGpu.coreClock, 0.3f);
        gpuPerformance = averageComponentPerformancePercent + gpuPerformance * averageComponentPerformancePercent;
    }

    public float CalculatePercentWithMagnitude(float val1 , float val2 , float magnitude)
    {
        return ((val1 - val2) / val2) * magnitude;
    }
}
