using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHandler : MonoBehaviour
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
    private ComputerStatus computerStatus;
    private PC_Component heldItem;
    private ComponentLocation compLocation;
    private float currentPickupTimerElapsed;
    private float currentPickupCooldown;
    private bool isHoldingItem = false;
    private Transform originalTransform;
    private Color startcolor;
    private PlayerStatus ps;

    private static GPULocation gpuLoc;
    private static MotherboardLocation mbLoc;
 
    private void Start()
    {
        Init();
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
                //Debug.Log("Item targeted : " + itemBeingPickedUp.gameObject.name);
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
                SelectLocationFromRay();
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
            if(hititem != null && hititem != itemBeingPickedUp && hitinfo.collider.gameObject == this.gameObject)
            {
                GetComponent<Outline>().OutlineColor = Color.red;
                itemBeingPickedUp = hititem;
                lastItemBeingPickedUp = hititem;
                itemNameText.text = "Pickup " + itemBeingPickedUp.tag;
            }
        }
        else
        {
            itemBeingPickedUp = null;
            GetComponent<Outline>().OutlineColor = startcolor;
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
        if (GetComponent<Collider>().isTrigger == true)
            ModifyPCStatus(false);
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
        transform.localScale = originalTransform.localScale*2;
        heldItem = null;
        isHoldingItem = false;
        ps.isHolding = false;
    }
    private void PlaceComponent()
    {        
        GetComponent<Rigidbody>().useGravity = false;
        this.transform.position = compLocation.transform.position;
        this.transform.rotation = compLocation.transform.rotation;
        this.transform.Rotate(-90f, 0f,-90f);
        this.transform.localScale = originalTransform.localScale * 2;
        this.transform.parent = compLocation.transform.parent;
        ModifyPCStatus(true);
        heldItem = null;
        isHoldingItem = false;
        ps.isHolding = false;
    }

    public void ModifyPCStatus(bool status)
    {       
        switch (lastItemBeingPickedUp.tag)
        {
            case "GPU":
                computerStatus.HasGpu = status;
                gpuLoc.gameObject.SetActive(!status);
                if (status)
                    computerStatus.mountedGpu = lastItemBeingPickedUp.GetComponent<GPU_Component>();
                else
                    computerStatus.mountedGpu = null;
                break;
            case "CPU":
                computerStatus.HasCpu = status;
                break;
            case "Motherboard":
                computerStatus.HasMotherboard = status;
                mbLoc.gameObject.SetActive(!status);
                if (status)
                    computerStatus.mountedMotherboard = lastItemBeingPickedUp.GetComponent<Motherboard_Component>();
                else
                    computerStatus.mountedMotherboard = null;
                break;
            case "RAM":
                computerStatus.HasRam = status;
                break;
        }
        if (computerStatus.MotherboardHasComponents())
            computerStatus.mountedMotherboard.gameObject.GetComponent<BoxCollider>().enabled = false;
        else if (computerStatus.mountedMotherboard != null)
            computerStatus.mountedMotherboard.gameObject.GetComponent<BoxCollider>().enabled = true;
        Debug.Log("Modified status of " + lastItemBeingPickedUp.tag + " to " + status);      
    }

    public void fillLocations()
    {
        gpuLoc = GameObject.Find("GPULocation").GetComponent<GPULocation>();
        mbLoc = computerStatus.gameObject.GetComponentInChildren<MotherboardLocation>();
    }

    public void Init()
    {
        pickupProgressImage.fillAmount = 0;
        pickupImageRoot.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        pickupImageRoot.gameObject.SetActive(false);
        ps = thePlayer.GetComponent<PlayerStatus>();
        originalTransform = this.transform;
        lastItemBeingPickedUp = new PC_Component();
        lastComponentLocation = new ComponentLocation();
        startcolor = GetComponent<Outline>().OutlineColor;     
        computerStatus = GameObject.Find("MotherboardLocation").transform.parent.gameObject.GetComponent<ComputerStatus>();
        fillLocations();
    }
}
