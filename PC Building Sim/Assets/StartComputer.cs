using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartComputer : MonoBehaviour
{
    private bool needsToCheck;
    public ComputerStatus computerStatus;
    public Canvas scoreCanvas;
    public Canvas uiCanvas;
    public GameObject score;
    public GameObject scoreForSave;
    private void Start()
    {
        computerStatus = GameObject.Find("MotherboardLocation").transform.parent.gameObject.GetComponent<ComputerStatus>();
    }
    private void Update()
    {
        if (needsToCheck)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (computerStatus.TryStart())
                {
                    score.GetComponent<ScoreController>().UpdateScores();
                    scoreForSave.GetComponent<ScoreController>().UpdateScores();
                    scoreCanvas.GetComponent<Canvas>().enabled = true;
                    uiCanvas.GetComponent<Canvas>().enabled = false;
                    Time.timeScale = 0f;
                    Cursor.lockState = CursorLockMode.None;
                    
                }
                else
                {
                    Debug.Log("missing components");
                }
            }
        }
    }
    private void OnMouseEnter()
    {
        needsToCheck = true;
    }
    private void OnMouseExit()
    {
        needsToCheck = false;
    }

    public void OkButton()
    {
        scoreCanvas.GetComponent<Canvas>().enabled = false;
        uiCanvas.GetComponent<Canvas>().enabled = true;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        
    }
}
