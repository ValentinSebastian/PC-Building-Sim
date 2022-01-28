using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private RectTransform placeImageRoot;
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
    private static GameObject cpuLoc;
    private static GameObject coolerLoc;
    private static RAMLocation ramLoc1;
    private static RAMLocation ramLoc2;
    private static RAMLocation ramLoc3;
    private static RAMLocation ramLoc4;

    private void Awake()
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
                placeImageRoot.gameObject.SetActive(false);
                placeImageRoot.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                pickupImageRoot.gameObject.SetActive(true);
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
                        placeImageRoot.gameObject.SetActive(true);
                        if (placeImageRoot.gameObject.transform.localScale.x < 1f)
                            placeImageRoot.gameObject.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
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
                        placeImageRoot.gameObject.SetActive(false);
                        placeImageRoot.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
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
                ChangeHintNameTag();
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
                //Debug.Log("FoundLocation");
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

    private void ChangeHintNameTag()
    {
        switch(itemBeingPickedUp.tag)
        {
            case "GPU":
                itemNameText.text = "Pickup " + itemBeingPickedUp.GetComponentInChildren<GPU_Component>().gpuSpecs.cName;
                break;
            case "CPU":
                itemNameText.text = "Pickup " + itemBeingPickedUp.GetComponentInChildren<CPU_Component>().cpuSpecs.cName; ;
                break;
            case "Motherboard":
                itemNameText.text = "Pickup " + itemBeingPickedUp.GetComponentInChildren<Motherboard_Component>().mbSpecs.cName; ;
                break;
            case "RAM":
                itemNameText.text = "Pickup " + itemBeingPickedUp.GetComponentInChildren<RAM_Component>().ramSpecs.cName; ;
                break;
            case "Cooler":
                itemNameText.text = "Pickup " + itemBeingPickedUp.GetComponentInChildren<Cooler_Component>().name; ;
                break;
        }
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
        if (ModifyPCStatus(true))
            SetComponentLocation();
        else
            return;
        heldItem = null;
        isHoldingItem = false;
        ps.isHolding = false;
    }

    public bool ModifyPCStatus(bool status)
    {       
        if(gpuLoc == null || mbLoc == null || ramLoc1 == null)
        {
            fillLocations();
        }
        if (!CheckCompatibility())
            return false;
        switch (lastItemBeingPickedUp.tag)
        {
            case "GPU":
                if (status && computerStatus.HasGpu)
                    return false;
                if (status && !computerStatus.HasMotherboard)
                    return false;
                computerStatus.HasGpu = status;
                if (status)
                    computerStatus.mountedGpu = lastItemBeingPickedUp.GetComponent<GPU_Component>();
                else
                {
                    if(computerStatus.ComputerIsRunning())
                        computerStatus.StopComputer();
                    computerStatus.mountedGpu = null;
                }
                break;
            case "CPU":
                if (status && computerStatus.HasCpu)
                    return false;
                if (status && !computerStatus.HasMotherboard)
                    return false;
                computerStatus.HasCpu = status;
                if (status)
                    computerStatus.mountedCpu = lastItemBeingPickedUp.GetComponent<CPU_Component>();
                else
                {
                    if (computerStatus.ComputerIsRunning())
                        computerStatus.StopComputer();
                    cpuLoc.GetComponent<BoxCollider>().enabled = true;
                    computerStatus.mountedCpu = null;
                }

                break;
            case "Motherboard":
                computerStatus.HasMotherboard = status;
                mbLoc.gameObject.SetActive(!status);
                if (status)
                    computerStatus.mountedMotherboard = lastItemBeingPickedUp.GetComponent<Motherboard_Component>();
                else
                    computerStatus.mountedMotherboard = null;
                break;
            case "Cooler":
                if (status && !computerStatus.HasCpu)
                    return false;
                if (status && !computerStatus.HasMotherboard)
                    return false;
                computerStatus.HasCooler = status;
                if (status)
                    computerStatus.mountedCooler = lastItemBeingPickedUp.GetComponent<Cooler_Component>();
                else
                {
                    coolerLoc.GetComponent<BoxCollider>().enabled = true;
                    Debug.Log("Activated collider for cooler location" + coolerLoc.gameObject.name);
                    if (computerStatus.ComputerIsRunning())
                        computerStatus.StopComputer();
                    computerStatus.mountedCooler = null;
                }
                break;
            case "RAM":
                if (status && !computerStatus.HasMotherboard)
                    return false;
                if (!status)
                {
                    if (computerStatus.ComputerIsRunning())
                        computerStatus.StopComputer();
                    compLocation = lastItemBeingPickedUp.GetComponent<RAM_Component>().GetMountSlot();
                }
                computerStatus.mountedRam = lastItemBeingPickedUp.GetComponent<RAM_Component>();
                if(compLocation.name == "ramSlot1")
                {
                    if (status && computerStatus.HasRam1)
                        return false;
                    computerStatus.HasRam1 = status;
                    computerStatus.mountedRam1 = lastItemBeingPickedUp.GetComponent<RAM_Component>();
                    if (status)
                    {
                        lastItemBeingPickedUp.GetComponent<RAM_Component>().SetMountSlot(compLocation);
                        if (computerStatus.ComputerIsRunning())
                            computerStatus.StopComputer();
                    }
                    else
                        computerStatus.mountedRam1 = null;

                }
                else if (compLocation.name == "ramSlot2")
                {
                    if (status && computerStatus.HasRam2)
                        return false;
                    computerStatus.HasRam2 = status;
                    computerStatus.mountedRam2 = lastItemBeingPickedUp.GetComponent<RAM_Component>();
                    if (status)
                    {
                        lastItemBeingPickedUp.GetComponent<RAM_Component>().SetMountSlot(compLocation);
                        if (computerStatus.ComputerIsRunning())
                            computerStatus.StopComputer();
                    }
                    else
                        computerStatus.mountedRam2 = null;
                }
                else if (compLocation.name == "ramSlot3")
                {
                    if (status && computerStatus.HasRam3)
                        return false;
                    computerStatus.HasRam3 = status;
                    computerStatus.mountedRam3 = lastItemBeingPickedUp.GetComponent<RAM_Component>();
                    if (status)
                    {
                        lastItemBeingPickedUp.GetComponent<RAM_Component>().SetMountSlot(compLocation);
                        if (computerStatus.ComputerIsRunning())
                            computerStatus.StopComputer();
                    }
                    else
                        computerStatus.mountedRam3 = null;
                }
                else if (compLocation.name == "ramSlot4")
                {
                    if (status && computerStatus.HasRam4)
                        return false;
                    computerStatus.HasRam4 = status;
                    computerStatus.mountedRam4 = lastItemBeingPickedUp.GetComponent<RAM_Component>();
                    if (status)
                    {
                        lastItemBeingPickedUp.GetComponent<RAM_Component>().SetMountSlot(compLocation);
                        if (computerStatus.ComputerIsRunning())
                            computerStatus.StopComputer();
                    }
                    else
                        computerStatus.mountedRam4 = null;
                }
                break;
        }
        if (computerStatus.MotherboardHasComponents())
            computerStatus.mountedMotherboard.gameObject.GetComponent<BoxCollider>().enabled = false;
        else if (computerStatus.mountedMotherboard != null)
            computerStatus.mountedMotherboard.gameObject.GetComponent<BoxCollider>().enabled = true;
        if (lastItemBeingPickedUp.tag != "RAM")
            Debug.Log("Modified status of " + lastItemBeingPickedUp.tag + " to " + status);
        else
            Debug.Log("Modified status of " + compLocation.name + " to " + status);

        computerStatus.somethingChanged = true;
        return true;
    }

    public void SetComponentLocation()
    {
        GetComponent<Rigidbody>().useGravity = false;
        this.transform.position = compLocation.transform.position;
        this.transform.rotation = compLocation.transform.rotation;
        switch (lastItemBeingPickedUp.tag)
        {
            case "GPU":             
                if(this.gameObject.name.Contains("2060"))
                {
                    this.transform.Rotate(0f, 0f, 0f);
                    this.transform.localScale = originalTransform.localScale * 2;
                    this.transform.parent = compLocation.transform.parent;
                    this.transform.localPosition += new Vector3(0.2f, 0.1f, 0.7f);
                }
                else if(gameObject.name.Contains("radeon"))
                {
                    this.transform.Rotate(0f, 90f, 0f);
                    this.transform.localScale = originalTransform.localScale * 2;
                    this.transform.parent = compLocation.transform.parent;
                    this.transform.localPosition += new Vector3(0.2f, 0.1f, 0.7f);
                    Debug.Log("dis a radeon");
                }
                else
                {
                    this.transform.Rotate(90f, 0f, -90f);
                    this.transform.localScale = originalTransform.localScale * 2;
                    this.transform.parent = compLocation.transform.parent;
                    this.transform.localPosition += new Vector3(0f, -0.2f, 0.7f);
                }
                break;
            case "CPU":
                this.transform.Rotate(-90f, 0f, -90f);
                this.transform.localScale = originalTransform.localScale * 2;
                this.transform.parent = compLocation.transform.parent;
                this.transform.localPosition += new Vector3(-0.32f, -0.01f, -0.5f);
                cpuLoc = compLocation.gameObject;
                cpuLoc.GetComponent<BoxCollider>().enabled = false;
                break;
            case "Cooler":
                this.transform.Rotate(0f, 90f, 90f);
                this.transform.localScale = originalTransform.localScale * 2;
                this.transform.parent = compLocation.transform.parent;
                this.transform.localPosition += new Vector3(0.25f, 0f, 0f);
                coolerLoc = compLocation.gameObject.transform.parent.gameObject;
                coolerLoc.GetComponent<BoxCollider>().enabled = false;
                break;
            case "Motherboard":
                this.transform.Rotate(-90f, 0f, -90f);
                this.transform.localScale = originalTransform.localScale * 2;
                this.transform.parent = compLocation.transform.parent;
                break;
            case "RAM":
                this.transform.Rotate(0f, -90f, -90f);
                this.transform.localScale = originalTransform.localScale * 2;
                this.transform.parent = compLocation.transform.parent;
                break;

                
        }       
    }

    public bool CheckCompatibility()
    {
        switch(lastItemBeingPickedUp.tag)
        {
            case "GPU":
                return true;
            case "CPU":
                if (lastItemBeingPickedUp.GetComponentInChildren<CPU_Component>().cpuSpecs.socket == lastComponentLocation.gameObject.GetComponentInParent<Motherboard_Component>().mbSpecs.cpuSocket)
                    return true;
                    break;
            case "RAM":
                if (lastItemBeingPickedUp.GetComponentInChildren<RAM_Component>().ramSpecs.memoryType == lastComponentLocation.gameObject.GetComponentInParent<Motherboard_Component>().mbSpecs.memoryType)
                    return true;
                break;
            case "Motherboard":
                return true;
            case "Cooler":
                return true;
        }
        return false;
    }

    public void fillLocations()
    {
        gpuLoc = GameObject.Find("GPULocation").GetComponent<GPULocation>();
        mbLoc = computerStatus.gameObject.GetComponentInChildren<MotherboardLocation>();
        ramLoc1 = GameObject.Find("ramSlot1").GetComponent<RAMLocation>();
    }

    public void Init()
    {
        pickupProgressImage.fillAmount = 0;
        pickupImageRoot.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        placeImageRoot.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        pickupImageRoot.gameObject.SetActive(false);
        placeImageRoot.gameObject.SetActive(false);
        ps = thePlayer.GetComponent<PlayerStatus>();
        originalTransform = this.transform;
        //lastItemBeingPickedUp = new PC_Component();
        //lastComponentLocation = new ComponentLocation();
        startcolor = GetComponent<Outline>().OutlineColor;     
        computerStatus = GameObject.Find("MotherboardLocation").transform.parent.gameObject.GetComponent<ComputerStatus>();
    }

    public void ChangeResources(ItemHandler other)
    {
        this.camera = other.camera;
        this.pickupTime = other.pickupTime;
        this.pickupImageRoot = other.pickupImageRoot;
        this.placeImageRoot = other.placeImageRoot;
        this.pickupProgressImage = other.pickupProgressImage;
        this.layerMaskComponent = other.layerMaskComponent;
        this.layerMaskLocation = other.layerMaskLocation;
        this.theDestination = other.theDestination;
        this.pickupCooldown = other.pickupCooldown;
        this.thePlayer = other.thePlayer;
        this.itemNameText = other.itemNameText;
    }
}
