using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopComponentPage : ShopUI
{
    public int nrOfComponents;
    public GameObject itemTemplate;

    private void Start()
    {
        fillGpuList();
        foreach(var gpu in allGpuComponents)
        {
            var temp = Instantiate(itemTemplate);
            temp.transform.parent = transform;
            temp.GetComponentInChildren<ShopItem>().itemName.text = gpu.GetComponentInChildren<GPU_Component>().cName;
            temp.GetComponentInChildren<ShopItem>().itemPrice.text = gpu.GetComponentInChildren<GPU_Component>().cPrice.ToString();
            Debug.Log("instantiated object");
        }
    }

}
