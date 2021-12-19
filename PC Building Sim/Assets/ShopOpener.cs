using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopOpener : MonoBehaviour
{
    [SerializeField]
    private GameObject shopCanvas;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject uiCanvas;
    [SerializeField]
    private GameObject shopBackground;
    [SerializeField]
    private GameObject shopComponents;
    private bool needsToCheck;
    private Vector2 initScaleCompValues;
    // Start is called before the first frame update


    private void Start()
    {
        initScaleCompValues = shopComponents.transform.localScale;
        shopBackground.transform.localScale = Vector2.zero;
        shopComponents.transform.localScale = Vector2.zero;
    }
    private void Update()
    {
        if(needsToCheck)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                shopCanvas.GetComponent<Canvas>().enabled = true;
                shopBackground.transform.LeanScale(Vector2.one, 0.8f).setEaseOutQuart();
                shopComponents.transform.LeanScale(initScaleCompValues, 0.8f).setEaseOutQuart();
                uiCanvas.GetComponentInChildren<Canvas>().enabled = false;
                //Time.timeScale = 0f;
                player.GetComponent<PlayerStatus>().isWatchingShop = true;
                Cursor.lockState = CursorLockMode.None;
            }          
        }
    }

    public void CloseButton()
    {               
        shopBackground.transform.LeanScale(Vector2.zero, 0.3f).setEaseInBack();
        shopComponents.transform.LeanScale(Vector2.zero, 0.3f).setEaseInBack();       
        Cursor.lockState = CursorLockMode.Locked;
        Invoke("HideCanvas", 0.3f);
    }
    public void HideCanvas()
    {
        shopCanvas.GetComponentInChildren<Canvas>().enabled = false;
        uiCanvas.GetComponentInChildren<Canvas>().enabled = true;
        player.GetComponent<PlayerStatus>().isWatchingShop = false;
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
