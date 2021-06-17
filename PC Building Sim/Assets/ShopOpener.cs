using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopOpener : MonoBehaviour
{
    private bool needsToCheck;
    // Start is called before the first frame update

    private void Update()
    {
        if(needsToCheck)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameObject.Find("Shop_Canvas").GetComponentInChildren<Canvas>().enabled = true;
                GameObject.Find("UI_Canvas").GetComponentInChildren<Canvas>().enabled = false;
                Time.timeScale = 0f;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().isWatchingShop = true;
                Cursor.lockState = CursorLockMode.None;
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

}
