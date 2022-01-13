using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartComputer : MonoBehaviour
{
    private bool needsToCheck;
    private ComputerStatus computerStatus;
    public Canvas scoreCanvas;
    public Canvas uiCanvas;
    public GameObject score;
    public GameObject scoreForSave;
    public GameObject scorePcComponents;
    public GameObject player;
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
                    scoreCanvas.GetComponent<Canvas>().enabled = true;
                    uiCanvas.GetComponent<Canvas>().enabled = false;
                    player.GetComponent<PlayerStatus>().isWatchingShop = true;
                    Cursor.lockState = CursorLockMode.None;
                    computerStatus.StartComputerAnimations(true);
                    computerStatus.StartComputerSounds(true);
                    score.GetComponent<ScoreController>().UpdateScores();
                    scoreForSave.GetComponent<ScoreController>().UpdateScoresEfficient();
                    scorePcComponents.GetComponent<ScoreUsedComponents>().UpdateScoreComponentsText();
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
        player.GetComponent<PlayerStatus>().isWatchingShop = false;
        Cursor.lockState = CursorLockMode.Locked;
        
    }
}
