using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldToPickUp : MonoBehaviour
{
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private float pickupTime;
    [SerializeField]
    private RectTransform pickupImageRoot;
    [SerializeField]
    private Image pickupProgressImage;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    public Transform theDestination;
    [SerializeField]
    private float pickupCooldown;
    [SerializeField]
    private TMPro.TextMeshProUGUI itemNameText;

    private PC_Component itemBeingPickedUp;
    private float currentPickupTimerElapsed;
    private float currentPickupCooldown;
    private bool isHoldingItem = false;

    // Update is called once per frame
    private void Start()
    {
        pickupProgressImage.fillAmount = 0;
    }
    void Update()
    {
        SelectComponentFromRay();
        if (HasItemTargeted() && !isHoldingItem)
        {
            pickupImageRoot.gameObject.SetActive(true);
            if (Input.GetButton("Fire1"))
            {
                IncrementPickupAndTryComplete();
            }
            else
            {
                currentPickupTimerElapsed = 0f;
            }
            UpdatePickupProgressImage();
        }
        else
        {
            pickupImageRoot.gameObject.SetActive(false);
            pickupProgressImage.fillAmount = 0;
            currentPickupTimerElapsed = 0f;
            if (isHoldingItem)
            {
                if (Input.GetButton("Fire2"))
                {
                    DropComponent();
                }
            }
        }
    }

    private void SelectComponentFromRay()
    {
        Ray ray = camera.ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hitinfo;
        if(Physics.Raycast(ray , out hitinfo , 2f , layerMask))
        {
            var hititem = hitinfo.collider.GetComponent<PC_Component>();
            if(hititem == null)
            {
                itemBeingPickedUp = null;
            }
            else if(hititem != null && hititem != itemBeingPickedUp)
            {
                itemBeingPickedUp = hititem;
                itemNameText.text = "Pickup " + itemBeingPickedUp.gameObject.name;
            }
        }
        else
        {
            itemBeingPickedUp = null;
        }
    }

    private void StartPickupCooldown()
    {
        currentPickupTimerElapsed += Time.deltaTime;
    }

    private void UpdatePickupProgressImage()
    {
        float prog = currentPickupTimerElapsed / pickupTime;
        pickupProgressImage.fillAmount = prog;
    }

    private bool HasItemTargeted()
    {
        return itemBeingPickedUp != null;
    }

    private void IncrementPickupAndTryComplete()
    {
        currentPickupTimerElapsed += Time.deltaTime;
        if(currentPickupTimerElapsed >= pickupTime)
        {
            PickupComponent();
        }
    }

    private void PickupComponent()
    {
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        this.transform.position = theDestination.position;
        this.transform.parent = GameObject.Find("PickUpLocation").transform;
        isHoldingItem = true;
    }

    private void DropComponent()
    {
        this.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<BoxCollider>().enabled = true;
        isHoldingItem = false;
    }
}
