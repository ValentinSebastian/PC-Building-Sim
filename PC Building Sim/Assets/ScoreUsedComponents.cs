using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUsedComponents : MonoBehaviour
{
    public GameObject computer;

    public TMPro.TextMeshProUGUI usedCpuTBox;
    public TMPro.TextMeshProUGUI usedGpuTBox;
    public TMPro.TextMeshProUGUI usedRamTBox;
    public TMPro.TextMeshProUGUI usedMoboTBox;
    public TMPro.TextMeshProUGUI totalPrice;

    public void UpdateScoreComponentsText()
    {
        ComputerStatus cs = computer.GetComponent<ComputerStatus>();
        usedCpuTBox.text = cs.mountedCpu.cpuSpecs.name;
        usedGpuTBox.text = cs.mountedGpu.gpuSpecs.name;
        usedRamTBox.text = cs.mountedRam.ramSpecs.name + " x" + cs.GetMountedRamNumber().ToString();
        usedMoboTBox.text = cs.mountedMotherboard.mbSpecs.name;
        totalPrice.text += "  " + cs.totalPrice.ToString() + "$";
    }
}
