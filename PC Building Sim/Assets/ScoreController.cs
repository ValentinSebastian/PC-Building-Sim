using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public TMPro.TextMeshProUGUI gpuScoreText;
    public TMPro.TextMeshProUGUI ramScoreText;
    public TMPro.TextMeshProUGUI cpuScoreText;
    public TMPro.TextMeshProUGUI totalScoreText;
    private ComputerStatus cs;
    private void Start()
    {
        cs = GameObject.Find("MotherboardLocation").transform.parent.gameObject.GetComponent<ComputerStatus>();
    }

    public void UpdateScores()
    {
        cs = GameObject.Find("MotherboardLocation").transform.parent.gameObject.GetComponent<ComputerStatus>();
        gpuScoreText.text = "Gpu Score: " + cs.gpuPerformance.ToString();
        ramScoreText.text = "Ram Score: " + cs.ramPerformance.ToString();
        cpuScoreText.text = "Cpu Score: " + cs.cpuPerformance.ToString();
        totalScoreText.text = "Final Score: " + cs.totalPerformance.ToString();
    }
}
