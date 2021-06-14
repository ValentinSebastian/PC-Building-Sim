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

    private void Start()
    {
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
                var shopItem = obj.GetComponentInChildren<ShopItem>();
                var gpuSpecs = gpu.GetComponentInChildren<GPU_Component>();
                shopItem.itemName.text = gpuSpecs.cName;
                shopItem.itemPrice.text = gpuSpecs.cPrice.ToString();
                shopItem.itemSpec1.text = gpuSpecs.memory.size.ToString() + " GB";
                shopItem.itemSpec2.text = gpuSpecs.memory.type.ToString();
                shopItem.itemSpec3.text = gpuSpecs.memory.bandwidth.ToString() + " bit";
                shopItem.itemId.text = nrOfComponents.ToString();
                obj.GetComponentInChildren<Button>().onClick.AddListener(delegate { BuyButton(obj);}) ;
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
                var shopItem = obj.GetComponentInChildren<ShopItem>();
                var ramSpecs = ram.GetComponentInChildren<RAM_Component>();
                shopItem.itemName.text = ramSpecs.cName;
                shopItem.itemPrice.text = ramSpecs.cPrice.ToString();
                shopItem.itemSpec1.text = "temp";
                shopItem.itemSpec2.text = "temp";
                shopItem.itemSpec3.text = "temp";
                shopItem.itemId.text = nrOfComponents.ToString();
                obj.GetComponentInChildren<Button>().onClick.AddListener(delegate { BuyButton(obj); });
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
                var shopItem = obj.GetComponentInChildren<ShopItem>();
                var cpuSpecs = cpu.GetComponentInChildren<CPU_Component>();
                shopItem.itemName.text = cpuSpecs.cName;
                shopItem.itemPrice.text = cpuSpecs.cPrice.ToString();
                shopItem.itemSpec1.text = "temp";
                shopItem.itemSpec2.text = "temp";
                shopItem.itemSpec3.text = "temp";
                shopItem.itemId.text = nrOfComponents.ToString();
                obj.GetComponentInChildren<Button>().onClick.AddListener(delegate { BuyButton(obj); });
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
                var shopItem = obj.GetComponentInChildren<ShopItem>();
                var mbSpecs = motherboard.GetComponentInChildren<Motherboard_Component>();
                shopItem.itemName.text = mbSpecs.cName;
                shopItem.itemPrice.text = mbSpecs.cPrice.ToString();
                shopItem.itemSpec1.text = "temp";
                shopItem.itemSpec2.text = "temp";
                shopItem.itemSpec3.text = "temp";
                shopItem.itemId.text = nrOfComponents.ToString();
                obj.GetComponentInChildren<Button>().onClick.AddListener(delegate { BuyButton(obj); });
                nrOfComponents++;
                Debug.Log("instantiated object");
            }
        }
    }
    #endregion
    public void BuyButton(GameObject temp)
    {
        Debug.Log(temp.GetComponent<ShopItem>().itemId.text);
        GameObject objToSpawn = temp, tempObj = temp;
        int index = int.Parse(temp.GetComponent<ShopItem>().itemId.text);
        Vector3 position;
        Quaternion rotation;                  
        switch(cType)
        {
            case PC_Component.ComponentType.GPU:
                tempObj = gpuSpawn;
                objToSpawn = allGpuComponents[index];
                break;
            case PC_Component.ComponentType.CPU:
                tempObj = cpuSpawn;
                objToSpawn = allCpuComponents[index];
                break;
            case PC_Component.ComponentType.Motherboard:
                tempObj = mbSpawn;
                objToSpawn = allMotherboardComponents[index];
                break;
            case PC_Component.ComponentType.RAM:
                tempObj = ramSpawn1;
                objToSpawn = allRamComponents[index];
                break;
            default:
                Debug.Log("tag error");
                    break;
        }
        objToSpawn.GetComponentInChildren<ItemHandler>().ChangeResources(referenceObject.GetComponent<ItemHandler>());
        Debug.Log(tempObj.name);
        position = tempObj.transform.position;
        rotation = tempObj.transform.rotation;
        var spawnedObj = Instantiate(objToSpawn , position , rotation);
        spawnedObj.transform.parent = tempObj.transform.parent;
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
