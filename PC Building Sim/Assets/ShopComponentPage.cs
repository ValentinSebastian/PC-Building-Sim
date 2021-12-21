using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopComponentPage : ShopUI
{
    public int nrOfComponents = 0;
    public GameObject itemTemplate;
    public GameObject referenceObject;
    public GameObject gpuSpawn;
    public GameObject cpuSpawn;
    public GameObject ramSpawn1;
    public GameObject mbSpawn;
    public GameObject detailsScreen;
    private float cpuPrice;
    private float gpuPrice;
    private float ramPrice;
    private float mbPrice;
    private float totalPrice;
    public TMPro.TextMeshProUGUI totalPriceText;



    private void Start()
    {
        totalPrice = 0;
        totalPriceText.text = totalPrice.ToString() + " $";
        FillGpuShop();
    }

    private void Update()
    {
        if(tabChanged)
        {
            ClearCurrentTab();
            FillShop();
            tabChanged = false;
        }       
    }

    #region Fill functions
    public void FillShop()
    {
        switch(cType)
        {
            case PC_Component.ComponentType.GPU:
                FillGpuShop();
                break;
            case PC_Component.ComponentType.CPU:
                FillCpuShop();
                break;
            case PC_Component.ComponentType.RAM:
                FillRamShop();
                break;
            case PC_Component.ComponentType.Motherboard:
                FillMotherboardShop();
                break;
            default:
                fillGpuList();
                break;
        }
    }

    public void FillGpuShop()
    {
        fillGpuList();
        if (allGpuComponents.Count > 0)
        {
            foreach (var gpu in allGpuComponents)
            {
                var obj = Instantiate(itemTemplate);
                obj.transform.parent = transform;
                Texture2D thumbnailItemImage = UnityEditor.AssetPreview.GetAssetPreview(gpu.gpuModel);
                obj.GetComponentInChildren<Image>().sprite = Sprite.Create(thumbnailItemImage, new Rect(0, 0, thumbnailItemImage.width, thumbnailItemImage.height), new Vector2(0.5f, 0.5f)); 
                var shopItem = obj.GetComponentInChildren<ShopItem>();
                shopItem.itemName.text = gpu.cName;
                shopItem.itemPrice.text = gpu.cPrice.ToString() + " $";
                shopItem.itemSpec1.text = gpu.memory.size.ToString() + " GB";
                shopItem.itemSpec2.text = gpu.memory.type.ToString();
                shopItem.itemSpec3.text = gpu.memory.bandwidth.ToString() + " bit";
                shopItem.itemId.text = nrOfComponents.ToString();
                shopItem.BuyButton.onClick.AddListener(delegate { BuyButton(obj);}) ;
                shopItem.MoreInfoButton.onClick.AddListener(delegate { MoreInfoButton(shopItem); });
                nrOfComponents++;
                Debug.Log("instantiated object");
            }
        }
    }


    public void FillRamShop()
    {
        fillRamList();
        if(allRamComponents.Count > 0)
        {
            foreach (var ram in allRamComponents)
            {
                var obj = Instantiate(itemTemplate);
                obj.transform.parent = transform;
                Texture2D thumbnailItemImage = UnityEditor.AssetPreview.GetAssetPreview(ram.ramModel);
                obj.GetComponentInChildren<Image>().sprite = Sprite.Create(thumbnailItemImage, new Rect(0, 0, thumbnailItemImage.width, thumbnailItemImage.height), new Vector2(0.5f, 0.5f));
                var shopItem = obj.GetComponentInChildren<ShopItem>();               
                shopItem.itemName.text = ram.cName;
                shopItem.itemPrice.text = ram.cPrice.ToString() + " $";
                shopItem.itemSpec1.text = ram.memorySize.ToString() + " GB";
                shopItem.itemSpec2.text = ram.memoryType;
                shopItem.itemSpec3.text = "CL " + ram.latency.ToString();
                shopItem.itemId.text = nrOfComponents.ToString();
                shopItem.BuyButton.onClick.AddListener(delegate { BuyButton(obj); });
                shopItem.MoreInfoButton.onClick.AddListener(delegate { MoreInfoButton(shopItem); });
                nrOfComponents++;
                Debug.Log("instantiated object");
            }
        }
        
    }
    public void FillCpuShop()
    {
        fillCpuList();
        if (allCpuComponents.Count > 0)
        {
            foreach (var cpu in allCpuComponents)
            {
                var obj = Instantiate(itemTemplate);
                obj.transform.parent = transform;
                Texture2D thumbnailItemImage = UnityEditor.AssetPreview.GetAssetPreview(cpu.cpuModel);
                obj.GetComponentInChildren<Image>().sprite = Sprite.Create(thumbnailItemImage, new Rect(0, 0, thumbnailItemImage.width, thumbnailItemImage.height), new Vector2(0.5f, 0.5f));
                var shopItem = obj.GetComponentInChildren<ShopItem>();
                shopItem.itemName.text = cpu.cName;
                shopItem.itemPrice.text = cpu.cPrice.ToString() + " $";
                shopItem.itemSpec1.text = cpu.cores.ToString() + " cores";
                shopItem.itemSpec2.text = cpu.threads.ToString() + " threads";
                shopItem.itemSpec3.text = cpu.botClock.ToString() + " - " + cpu.topClock.ToString() + " GHz";
                shopItem.itemSpec4.text = cpu.tdp.ToString() + " W";
                shopItem.itemSpec5.text = cpu.l3Cache.ToString() + " MB L3";
                shopItem.itemSpec6.text = cpu.socket;
                shopItem.itemId.text = nrOfComponents.ToString();
                shopItem.BuyButton.onClick.AddListener(delegate { BuyButton(obj); });
                shopItem.MoreInfoButton.onClick.AddListener(delegate { MoreInfoButton(shopItem); });
                nrOfComponents++;
                Debug.Log("instantiated object");
            }
        }
    }
    public void FillMotherboardShop()
    {
        fillMotherboardList();
        if (allMotherboardComponents.Count > 0)
        {
            foreach (var motherboard in allMotherboardComponents)
            {
                var obj = Instantiate(itemTemplate);
                obj.transform.parent = transform;
                Texture2D thumbnailItemImage = UnityEditor.AssetPreview.GetAssetPreview(motherboard.motherboardModel);
                obj.GetComponentInChildren<Image>().sprite = Sprite.Create(thumbnailItemImage, new Rect(0, 0, thumbnailItemImage.width, thumbnailItemImage.height), new Vector2(0.5f, 0.5f));
                var shopItem = obj.GetComponentInChildren<ShopItem>();
                shopItem.itemName.text = motherboard.cName;
                shopItem.itemPrice.text = motherboard.cPrice.ToString() + " $";
                shopItem.itemSpec1.text = motherboard.formFactor;
                shopItem.itemSpec2.text = motherboard.memorySlots.ToString() + " X " + motherboard.memoryType;
                shopItem.itemSpec3.text = motherboard.pci_eSlots;
                shopItem.itemSpec4.text = motherboard.audioChip;
                shopItem.itemSpec5.text = motherboard.memoryMaxFrequency + " Mhz";
                shopItem.itemSpec6.text = "Socket: " + motherboard.cpuSocket;
                shopItem.itemId.text = nrOfComponents.ToString();
                shopItem.BuyButton.onClick.AddListener(delegate { BuyButton(obj); });
                shopItem.MoreInfoButton.onClick.AddListener(delegate { MoreInfoButton(shopItem); });
                nrOfComponents++;
                Debug.Log("instantiated object");
            }
        }
    }
    #endregion
    public void BuyButton(GameObject temp)
    {
        Debug.Log(temp.GetComponent<ShopItem>().itemId.text);
        GameObject tempObj = temp;
        GameObject objToSpawn = new GameObject();
        int index = int.Parse(temp.GetComponent<ShopItem>().itemId.text);
        Vector3 position;
        Quaternion rotation;                  
        switch(cType)
        {
            case PC_Component.ComponentType.GPU:
                tempObj = gpuSpawn;
                objToSpawn = allGpuComponents[index].gpuModel;
                objToSpawn.GetComponentInChildren<GPU_Component>().gpuSpecs = allGpuComponents[index];
                gpuPrice = allGpuComponents[index].cPrice;
                break;
            case PC_Component.ComponentType.CPU:
                tempObj = cpuSpawn;
                objToSpawn = allCpuComponents[index].cpuModel;
                objToSpawn.GetComponentInChildren<CPU_Component>().cpuSpecs = allCpuComponents[index];
                cpuPrice = allCpuComponents[index].cPrice;
                break;
            case PC_Component.ComponentType.Motherboard:
                tempObj = mbSpawn;
                objToSpawn = allMotherboardComponents[index].motherboardModel;
                objToSpawn.GetComponentInChildren<Motherboard_Component>().mbSpecs = allMotherboardComponents[index];
                mbPrice = allMotherboardComponents[index].cPrice;
                break;
            case PC_Component.ComponentType.RAM:
                tempObj = ramSpawn1;
                objToSpawn = allRamComponents[index].ramModel;
                objToSpawn.GetComponentInChildren<RAM_Component>().ramSpecs = allRamComponents[index];
                ramPrice = allRamComponents[index].cPrice;
                break;
            default:
                Debug.Log("tag error");
                    break;
        }
        totalPrice = cpuPrice + gpuPrice + ramPrice + mbPrice;
        totalPriceText.text = totalPrice.ToString() + " $";
        objToSpawn.GetComponentInChildren<ItemHandler>().ChangeResources(referenceObject.GetComponent<ItemHandler>());
        Debug.Log(tempObj.name);
        position = tempObj.transform.position;
        rotation = tempObj.transform.rotation;
        var spawnedObj = Instantiate(objToSpawn , position , rotation);
        spawnedObj.transform.parent = tempObj.transform.parent;
    }
    public void MoreInfoButton(ShopItem shopItem)
    {
        DetailsScreen screenData = detailsScreen.GetComponent<DetailsScreen>();
        screenData.itemName.text = shopItem.itemName.text;
        screenData.itemSpec1.text = shopItem.itemSpec1.text;
        screenData.itemSpec2.text = shopItem.itemSpec2.text;
        screenData.itemSpec3.text = shopItem.itemSpec3.text;
        screenData.itemSpec4.text = shopItem.itemSpec4.text;
        screenData.itemSpec5.text = shopItem.itemSpec5.text;
        screenData.itemSpec6.text = shopItem.itemSpec6.text;

        detailsScreen.transform.LeanScale(Vector2.one, 0.5f).setEaseInQuart();
    }
    public void ClearCurrentTab()
    {
        var gameObjects = GameObject.FindGameObjectsWithTag("ShopItemTemplate");

        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
        nrOfComponents = 0;
    }
}
