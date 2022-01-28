using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
    [SerializeField]
    private GameObject shopTabs;
    [SerializeField]
    private GameObject shopComponentsList;
    private bool needsToCheck;
    private Vector2 initScaleCompValues;
    // Start is called before the first frame update
    Vector2 tempTransform;

    private void Start()
    {
        tempTransform = shopComponentsList.transform.localScale;
        HideCanvas();
        initScaleCompValues = shopComponents.transform.localScale;
        shopBackground.transform.localScale = Vector2.zero;
        //shopTabs.transform.localScale = Vector2.zero;
        shopComponents.transform.localScale = Vector2.zero;
    }
    private void Update()
    {
        if(needsToCheck)
        {
            if (Input.GetKeyDown(KeyCode.E) && !player.GetComponent<PlayerStatus>().isPaused)
            {
                shopCanvas.GetComponent<Canvas>().enabled = true;
                shopBackground.transform.LeanScale(Vector2.one, 0.8f).setEaseOutQuart();
                //shopTabs.transform.LeanScale(Vector2.one, 0.8f).setEaseOutQuart();              
                shopComponents.transform.LeanScale(initScaleCompValues, 0.8f).setEaseOutQuart();
                uiCanvas.GetComponentInChildren<Canvas>().enabled = false;
                player.GetComponent<PlayerStatus>().isWatchingShop = true;
                Cursor.lockState = CursorLockMode.None;
                shopComponentsList.SetActive(true);
            }          
        }
    }

    public void CloseButton()
    {               
        shopBackground.transform.LeanScale(Vector2.zero, 0.3f).setEaseInBack();
        //shopTabs.transform.LeanScale(Vector2.zero, 0.3f).setEaseInBack();
        shopComponents.transform.LeanScale(Vector2.zero, 0.3f).setEaseInBack();
        //shopComponents.GetComponentInChildren<ScrollRect>().verticalNormalizedPosition = 0.5f;      
        Cursor.lockState = CursorLockMode.Locked;
        Invoke("HideCanvas", 0.3f);
    }
    public void HideCanvas()
    {
        shopComponentsList.SetActive(false);
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
