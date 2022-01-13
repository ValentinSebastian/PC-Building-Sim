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
    [System.NonSerialized] public Motherboard_Component mountedMotherboard;
    [System.NonSerialized] public GPU_Component mountedGpu;
    [System.NonSerialized] public RAM_Component mountedRam;
    [System.NonSerialized] public RAM_Component mountedRam1;
    [System.NonSerialized] public RAM_Component mountedRam2;
    [System.NonSerialized] public RAM_Component mountedRam3;
    [System.NonSerialized] public RAM_Component mountedRam4;
    [System.NonSerialized] public CPU_Component mountedCpu;
    public Cooler_Component mountedCooler;
    private GpuSO averageGpu;
    private CpuSO averageCpu;
    private RamSO averageRam;
    [System.NonSerialized] public float gpuPerformance;
    [System.NonSerialized] public float cpuPerformance;
    [System.NonSerialized] public float ramPerformance;
    [System.NonSerialized] public float totalPerformance;
    [System.NonSerialized] public float valuePerformance;
    private const float averageComponentPerformancePercent = 50f;
    public Image ComputerStatusDisplay;
    public Image BuildingStepsDisplay;
    public bool CSD_Hidden = true;
    private bool computerRunning = false;
    public float totalPrice = 0;
    private int counter = 0;
    public TMPro.TextMeshProUGUI cpuStatusTextbox;
    public TMPro.TextMeshProUGUI gpuStatusTextbox;
    public TMPro.TextMeshProUGUI moboStatusTextbox;
    public TMPro.TextMeshProUGUI ramStatusTextbox;
    public TMPro.TextMeshProUGUI fanStatusTextbox;

    public TMPro.TextMeshProUGUI buildingStepsCpuTBox;
    public TMPro.TextMeshProUGUI buildingStepsGpuTBox;
    public TMPro.TextMeshProUGUI buildingStepsRamTBox;
    public TMPro.TextMeshProUGUI buildingStepsMoboTBox;
    public TMPro.TextMeshProUGUI buildingStepsFanTBox;

    public bool HasGpu { get => hasGpu; set => hasGpu = value; }
    public bool HasMotherboard { get => hasMotherboard; set => hasMotherboard = value; }
    public bool HasRam1 { get => hasRam1; set => hasRam1 = value; }
    public bool HasRam2 { get => hasRam2; set => hasRam2 = value; }
    public bool HasRam3 { get => hasRam3; set => hasRam3 = value; }
    public bool HasRam4 { get => hasRam4; set => hasRam4 = value; }
    public bool HasCpu { get => hasCpu; set => hasCpu = value; }
    public bool HasCooler { get => hasCooler; set => hasCooler = value; }

    public GameObject monitorScreen;

    FontStyle tempFont;
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
            if(counter == 0)
            {
                ComputerStatusDisplay.transform.LeanMoveX(ComputerStatusDisplay.transform.position.x - ComputerStatusDisplay.rectTransform.rect.width * 2, 0.3f);
                CSD_Hidden = false;
            }
            else if (counter == 1)
            {
                BuildingStepsDisplay.transform.LeanMoveX(BuildingStepsDisplay.transform.position.x + BuildingStepsDisplay.rectTransform.rect.width * 2, 0.3f);
                CSD_Hidden = false;
            }
            else if(counter == 2)
            {
                BuildingStepsDisplay.transform.LeanMoveX(BuildingStepsDisplay.transform.position.x - BuildingStepsDisplay.rectTransform.rect.width * 2, 0.3f);
                CSD_Hidden = true;
            }
            else if(counter == 3)
            {
                ComputerStatusDisplay.transform.LeanMoveX(ComputerStatusDisplay.transform.position.x + ComputerStatusDisplay.rectTransform.rect.width * 2, 0.3f);
            }
            counter++;
            if (counter > 3)
                counter = 0;
        }
        if (HasCpu)
        {
            cpuStatusTextbox.text = "Connected";
            cpuStatusTextbox.color = Color.green;
            buildingStepsCpuTBox.color = Color.grey;
            buildingStepsCpuTBox.fontStyle = TMPro.FontStyles.Strikethrough;

        }
        else
        {
            cpuStatusTextbox.text = "NOT CONNECTED";
            cpuStatusTextbox.color = Color.red;
            buildingStepsCpuTBox.color = Color.white;
            buildingStepsCpuTBox.fontStyle = TMPro.FontStyles.Bold;
        }
        if (HasGpu)
        {
            gpuStatusTextbox.text = "Connected";
            gpuStatusTextbox.color = Color.green;
            buildingStepsGpuTBox.color = Color.grey;
            buildingStepsGpuTBox.fontStyle = TMPro.FontStyles.Strikethrough;
        }
        else
        {
            gpuStatusTextbox.text = "NOT CONNECTED";
            gpuStatusTextbox.color = Color.red;
            buildingStepsGpuTBox.color = Color.white;
            buildingStepsGpuTBox.fontStyle = TMPro.FontStyles.Bold;
        }
        if (HasMotherboard)
        {
            moboStatusTextbox.text = "Connected";
            moboStatusTextbox.color = Color.green;
            buildingStepsMoboTBox.color = Color.grey;
            buildingStepsMoboTBox.fontStyle = TMPro.FontStyles.Strikethrough;
        }
        else
        {
            moboStatusTextbox.text = "NOT CONNECTED";
            moboStatusTextbox.color = Color.red;
            buildingStepsMoboTBox.color = Color.white;
            buildingStepsMoboTBox.fontStyle = TMPro.FontStyles.Bold;
        }
        if (HasRam1 || HasRam2 || HasRam3 || HasRam4)
        {
            ramStatusTextbox.text = "Connected";
            ramStatusTextbox.color = Color.green;
            buildingStepsRamTBox.color = Color.grey;
            buildingStepsRamTBox.fontStyle = TMPro.FontStyles.Strikethrough;
        }
        else
        {
            ramStatusTextbox.text = "NOT CONNECTED";
            ramStatusTextbox.color = Color.red;
            buildingStepsRamTBox.color = Color.white;
            buildingStepsRamTBox.fontStyle = TMPro.FontStyles.Bold;
        }
        if (HasCooler)
        {
            fanStatusTextbox.text = "Connected";
            fanStatusTextbox.color = Color.green;
            buildingStepsFanTBox.color = Color.grey;
            buildingStepsFanTBox.fontStyle = TMPro.FontStyles.Strikethrough;
        }
        else
        {
            fanStatusTextbox.text = "NOT CONNECTED";
            fanStatusTextbox.color = Color.red;
            buildingStepsFanTBox.color = Color.white;
            buildingStepsFanTBox.fontStyle = TMPro.FontStyles.Bold;
        }
    }
    public bool TryStart()
    {
        //temporary , need to add component compatibility check
        if (hasCpu && hasGpu && hasMotherboard && HasCooler && (hasRam1 || hasRam2 || hasRam3 || hasRam4))
        {
            CalculatePerformance();
            monitorScreen.GetComponent<MonitorDisplay>().SetStartComputerMaterial();
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
        monitorScreen.GetComponent<MonitorDisplay>().SetMissingPartsMaterial();
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

    public int GetMountedRamNumber()
    {
        int mountedRamNr = 0;
        if (mountedRam1 != null)
            mountedRamNr++;
        if (mountedRam2 != null)
            mountedRamNr++;
        if (mountedRam3 != null)
            mountedRamNr++;
        if (mountedRam4 != null)
            mountedRamNr++;
        return mountedRamNr;
    }   
    
    public float GetMountedRamScoreMultiplier()
    {
        int temp = GetMountedRamNumber();
        if (temp == 1)
            return 1;
        if (temp == 2)
            return 1.75f;
        if (temp == 3)
            return 1.85f;
        if (temp == 4)
            return 2.2f;
        return 1;
    }

    public void CalculatePerformance()
    {
        CalculateCpuPerformance();
        CalculateGpuPerformance();
        CalculateRamPerformance();
        totalPerformance = (ramPerformance + cpuPerformance + gpuPerformance);
        CalculateValuePerformance();
    }

    public void CalculateRamPerformance()
    {
        ramPerformance = CalculatePercentWithMagnitude(mountedRam.ramSpecs.frequency, averageRam.frequency, 1);
        ramPerformance -= CalculatePercentWithMagnitude(mountedRam.ramSpecs.latency, averageRam.latency, 1);
        ramPerformance = averageComponentPerformancePercent + ramPerformance * averageComponentPerformancePercent;
        ramPerformance *= GetMountedRamScoreMultiplier();
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

    public void CalculateValuePerformance()
    {
        CalculateTotalPrice();
        valuePerformance = (totalPerformance * (float)100) / totalPrice;
    }

    public void CalculateTotalPrice()
    {
        totalPrice = 0;
        totalPrice += mountedGpu.gpuSpecs.cPrice;
        totalPrice += mountedCpu.cpuSpecs.cPrice;
        totalPrice += mountedMotherboard.mbSpecs.cPrice;
        if (mountedRam1 != null)
            totalPrice += mountedRam1.ramSpecs.cPrice;
        if (mountedRam2 != null)
            totalPrice += mountedRam2.ramSpecs.cPrice;
        if (mountedRam3 != null)
            totalPrice += mountedRam3.ramSpecs.cPrice;
        if (mountedRam4 != null)
            totalPrice += mountedRam4.ramSpecs.cPrice;
    }
    public float CalculatePercentWithMagnitude(float val1 , float val2 , float magnitude)
    {
        return ((val1 - val2) / val2) * magnitude;
    }
}
