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
    private LayerMask layerMaskComponent;
    [SerializeField]
    private LayerMask layerMaskLocation;
    [SerializeField]
    public Transform theDestination;
    [SerializeField]
    private float pickupCooldown;
    [SerializeField]
    private GameObject thePlayer;
    [SerializeField]
    private TMPro.TextMeshProUGUI itemNameText;

    private PC_Component itemBeingPickedUp;
    private static PC_Component lastItemBeingPickedUp;
    private static ComponentLocation lastComponentLocation;
    private PC_Component heldItem;
    private ComponentLocation compLocation;
    private float currentPickupTimerElapsed;
    private float currentPickupCooldown;
    private bool isHoldingItem = false;
    private Transform originalTransform;
    PlayerStatus ps;
    // Update is called once per frame
    private void Start()
    {
        pickupProgressImage.fillAmount = 0;
        pickupImageRoot.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        pickupImageRoot.gameObject.SetActive(false);
        ps = thePlayer.GetComponent<PlayerStatus>();
        originalTransform = this.transform;
        lastItemBeingPickedUp = new PC_Component();
        lastComponentLocation = new ComponentLocation();
    }
    void Update()
    {   
        if(!ps.isHolding)
            SelectComponentFromRay();
        if (lastItemBeingPickedUp == this.GetComponent<PC_Component>())
        {
            if (HasItemTargeted() && !isHoldingItem)
            {
                pickupImageRoot.gameObject.SetActive(true);
                Debug.Log("Item targeted : " + itemBeingPickedUp.gameObject.name);
                if (pickupImageRoot.gameObject.transform.localScale.x < 1f)
                    pickupImageRoot.gameObject.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
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
                pickupImageRoot.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                pickupProgressImage.fillAmount = 0;
                currentPickupTimerElapsed = 0f;
                if (isHoldingItem)
                {
                    SelectLocationFromRay();
                    if (HasCompLocationTargeted())
                    {
                        if (Input.GetButton("Fire2"))
                        {
                            if (lastComponentLocation.tag == lastItemBeingPickedUp.tag + "Location")
                                PlaceComponent();
                            else
                                Debug.Log(lastComponentLocation.tag + " / " + lastItemBeingPickedUp.tag + "Location");
                        }
                    }
                    else
                    {
                        if (Input.GetButton("Fire2"))
                        {
                            DropComponent();
                        }
                    }
                }
            }

        }
    }
    private void SelectComponentFromRay()
    {
        Ray ray = camera.ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hitinfo;
        if(Physics.Raycast(ray , out hitinfo , 2f , layerMaskComponent))
        {
            var hititem = hitinfo.collider.GetComponent<PC_Component>();
            if(hititem == null)
            {
                itemBeingPickedUp = null;
            }
            else if(hititem != null && hititem != itemBeingPickedUp && hitinfo.collider.gameObject == this.gameObject)
            {
                itemBeingPickedUp = hititem;
                lastItemBeingPickedUp = hititem;
                itemNameText.text = "Pickup " + itemBeingPickedUp.gameObject.name;
            }
        }
        else
        {
            itemBeingPickedUp = null;
        }
    }
    private void SelectLocationFromRay()
    {
        Ray ray = camera.ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hitinfo;
        if (Physics.Raycast(ray, out hitinfo, 2f, layerMaskLocation))
        {
            var hititem = hitinfo.collider.GetComponent<ComponentLocation>();
            if (hititem == null)
            {
                compLocation = null;
            }
            else if (hititem != null && hititem != compLocation)
            {
                Debug.Log("FoundLocation");
                lastComponentLocation = hititem;
                compLocation = hititem;
            }
        }
        else
        {
            compLocation = null;
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
    private bool HasCompLocationTargeted()
    {
        return compLocation != null;
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
        heldItem = itemBeingPickedUp;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Collider>().isTrigger = true;
        this.transform.position = theDestination.position;
        this.transform.localScale = originalTransform.localScale / 2;
        this.transform.parent = theDestination.transform;
        isHoldingItem = true;
        ps.isHolding = true;
    }

    private void DropComponent()
    {    
        heldItem.transform.parent = null;        
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Collider>().isTrigger = false;
        this.transform.localScale = originalTransform.localScale*2;
        heldItem = null;
        isHoldingItem = false;
        ps.isHolding = false;
    }
    private void PlaceComponent()
    {
        Debug.Log("Placed object");
        GetComponent<Rigidbody>().useGravity = false;
        this.transform.position = compLocation.transform.position;
        this.transform.rotation = compLocation.transform.rotation;
        this.transform.Rotate(-90f, 0f,-90f);
        this.transform.localScale = originalTransform.localScale * 2;
        this.transform.parent = compLocation.transform;
        heldItem = null;
        isHoldingItem = false;
        ps.isHolding = false;
    }
}
