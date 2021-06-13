using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopComponentPage : ShopUI
{
    public int nrOfComponents = 0;
    public GameObject itemTemplate;

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
                var temp = Instantiate(itemTemplate);
                temp.transform.parent = transform;
                temp.GetComponentInChildren<ShopItem>().itemName.text = ram.GetComponentInChildren<RAM_Component>().cName;
                temp.GetComponentInChildren<ShopItem>().itemPrice.text = ram.GetComponentInChildren<RAM_Component>().cPrice.ToString();
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
                var temp = Instantiate(itemTemplate);
                temp.transform.parent = transform;
                temp.GetComponentInChildren<ShopItem>().itemName.text = cpu.GetComponentInChildren<CPU_Component>().cName;
                temp.GetComponentInChildren<ShopItem>().itemPrice.text = cpu.GetComponentInChildren<CPU_Component>().cPrice.ToString();
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
                var temp = Instantiate(itemTemplate);
                temp.transform.parent = transform;
                temp.GetComponentInChildren<ShopItem>().itemName.text = motherboard.GetComponentInChildren<Motherboard_Component>().cName;
                temp.GetComponentInChildren<ShopItem>().itemPrice.text = motherboard.GetComponentInChildren<Motherboard_Component>().cPrice.ToString();
                Debug.Log("instantiated object");
            }
        }
    }
    #endregion
    public void BuyButton(GameObject temp)
    {
        Debug.Log(temp.GetComponent<ShopItem>().itemId.text);
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
