using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public TMPro.TextMeshProUGUI gpuScoreText;
    public TMPro.TextMeshProUGUI ramScoreText;
    public TMPro.TextMeshProUGUI cpuScoreText;
    public TMPro.TextMeshProUGUI valueScoreText;
    public TMPro.TextMeshProUGUI totalScoreText;
    private ComputerStatus cs;
    private const int step = 50;
    private float gpuScore;
    private float cpuScore;
    private float ramScore;
    private bool startScaling = false;
    private void Start()
    {
        cs = GameObject.Find("MotherboardLocation").transform.parent.gameObject.GetComponent<ComputerStatus>();
    }

    public void UpdateScores()
    {
        cs = GameObject.Find("MotherboardLocation").transform.parent.gameObject.GetComponent<ComputerStatus>();
        gpuScore = 0;
        cpuScore = 0;
        ramScore = 0;
        startScaling = true;
        totalScoreText.text = "Final Score: ";
    }

    private void FixedUpdate()
    {
        if(startScaling)
        {
            gpuScore += step * Time.deltaTime;
            cpuScore += step * Time.deltaTime;
            ramScore += step * Time.deltaTime;
            if (gpuScore <= cs.gpuPerformance)
            {
                gpuScoreText.text = gpuScore.ToString("0.00");
                gpuScoreText.color = ValueTextColor(gpuScore, "gpu");
            }
            if (cpuScore <= cs.cpuPerformance)
            {
                cpuScoreText.text = cpuScore.ToString("0.00");
                cpuScoreText.color = ValueTextColor(cpuScore, "cpu");
            }
            if (ramScore <= cs.ramPerformance)
            {
                ramScoreText.text = ramScore.ToString("0.00");
                ramScoreText.color = ValueTextColor(ramScore, "ram");
            }
            if (gpuScore >= cs.gpuPerformance && cpuScore >= cs.cpuPerformance && ramScore >= cs.ramPerformance)
            {
                startScaling = false;
                totalScoreText.text = "Final Score: " + cs.totalPerformance.ToString("0.00");
                totalScoreText.color = ValueTextColor(cs.totalPerformance, "final");
                valueScoreText.text = cs.valuePerformance.ToString("0.00");
                valueScoreText.color = ValueTextColor(cs.valuePerformance, "avg");
            }
        }      
    }


    public Color ValueTextColor(float value, string type)
    {
        //80 50 80
        float avCpu = 65;
        float avGpu = 50;
        float avRam = 80;
        float avPrice = 8f; // 800 dar impartit la 100
        if (type == "cpu")
        {
            if (value < avCpu - avCpu/2)
                return Color.red;
            if (value < avCpu - avCpu/4 && value > avCpu - avCpu / 2)
                return Color.yellow;
            if (value <= avCpu && value > avCpu - avCpu / 4)
                return Color.white;
            if (value > avCpu + avCpu / 4)
                return Color.green;
        }
        else if (type == "gpu")
        {
            if (value < avGpu - avGpu/2)
                return Color.red;
            if (value < avGpu - avGpu / 4 && value > avGpu - avGpu / 2)
                return Color.yellow;
            if (value <= avGpu && value > avGpu - avGpu / 4)
                return Color.white;
            if (value > avGpu + avGpu / 4)
                return Color.green;
        }
        else if (type == "ram")
        {
            if (value < avRam - avRam/2)
                return Color.red;
            if (value < avRam - avRam / 4 && value > avRam - avRam / 2)
                return Color.yellow;
            if (value <= avRam && value > avRam - avRam / 4)
                return Color.white;
            if (value > avRam + avRam / 4)
                return Color.green;
        }
        else if (type == "final")
        {
            if (value < avCpu - avCpu / 2 + avGpu - avGpu / 2 + avRam - avRam / 2)
                return Color.red;
            if (value < avCpu - avCpu / 4 + avGpu - avGpu / 4 + avRam - avRam / 4 && value > avCpu - avCpu / 2 + avGpu - avGpu / 2 + avRam - avRam / 2)
                return Color.yellow;
            if (value <= avCpu + avGpu + avRam && value > avCpu - avCpu / 4 + avGpu - avGpu / 4 + avRam - avRam / 4)
                return Color.white;
            if (value > avCpu + avCpu / 4 + avGpu + avGpu / 4 + avRam + avRam / 4)
                return Color.green;
        }
        else if (type == "avg")
        {
            if (value < (avCpu - avCpu / 2 + avGpu - avGpu / 2 + avRam - avRam / 2)/avPrice)
                return Color.red;
            if (value < (avCpu - avCpu / 4 + avGpu - avGpu / 4 + avRam - avRam / 4)/avPrice && value > (avCpu - avCpu / 2 + avGpu - avGpu / 2 + avRam - avRam / 2)/avPrice)
                return Color.yellow;
            if (value <= (avCpu + avGpu + avRam)/avPrice && value > (avCpu - avCpu / 4 + avGpu - avGpu / 4 + avRam - avRam / 4)/avPrice)
                return Color.white;
            if (value > (avCpu + avCpu / 4 + avGpu + avGpu / 4 + avRam + avRam / 4)/avPrice)
                return Color.green;
        }
        return Color.white;
    }
}
