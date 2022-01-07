using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuObject;
    public GameObject player;

    void Update()
    {
        if(!GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().isWatchingShop)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!player.GetComponent<PlayerStatus>().isPaused)
                {
                    ActivateMenu();
                }
                else
                {
                    DeactivateMenu();
                }
            }       
        }
    }

    public void ActivateMenu()
    {
        pauseMenuObject.GetComponentInChildren<Canvas>().enabled = true;
        Cursor.lockState = CursorLockMode.None;
        player.GetComponent<PlayerStatus>().isPaused = true;
    }

    public void DeactivateMenu()
    {
        pauseMenuObject.GetComponentInChildren<Canvas>().enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        player.GetComponent<PlayerStatus>().isPaused = false;
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
