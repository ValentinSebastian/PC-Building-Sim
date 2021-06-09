using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopComponentPage : ShopUI
{
    public int nrOfComponents;
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
        }
    }

    public void FillGpuShop()
    {
        fillGpuList();
        if (allGpuComponents.Count > 0)
        {
            foreach (var gpu in allGpuComponents)
            {
                var temp = Instantiate(itemTemplate);
                temp.transform.parent = transform;
                temp.GetComponentInChildren<ShopItem>().itemName.text = gpu.GetComponentInChildren<GPU_Component>().cName;
                temp.GetComponentInChildren<ShopItem>().itemPrice.text = gpu.GetComponentInChildren<GPU_Component>().cPrice.ToString();
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

    public void ClearCurrentTab()
    {
        var gameObjects = GameObject.FindGameObjectsWithTag("ShopItemTemplate");

        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
    }
}
