using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopUI : MonoBehaviour
{
    public static PC_Component.ComponentType cType;
    public List<GpuSO> allGpuComponents = new List<GpuSO>();
    public List<CpuSO> allCpuComponents = new List<CpuSO>();
    public List<RamSO> allRamComponents = new List<RamSO>();
    public List<MotherboardSO> allMotherboardComponents = new List<MotherboardSO>();
    public static bool tabChanged;

    #region Fill functions
    public void fillGpuList()
    {
        Debug.Log("trying to fill with gpu's");
        allGpuComponents = new List<GpuSO>();
        Object[] subListObjects = Resources.LoadAll("ScriptableObjects/GPU", typeof(GpuSO));
        foreach (GpuSO temp in subListObjects)
        {
            Debug.Log("filling with gpus");
            allGpuComponents.Add(temp);
        }           
    }
    public void fillCpuList()
    {
        allCpuComponents = new List<CpuSO>();
        Object[] subListObjects2 = Resources.LoadAll("ScriptableObjects/CPU", typeof(CpuSO));
        foreach (CpuSO temp in subListObjects2)
        {
            allCpuComponents.Add(temp);
        }      
    }
    public void fillRamList()
    {
        allRamComponents = new List<RamSO>();
        Object[] subListObjects3 = Resources.LoadAll("ScriptableObjects/RAM", typeof(RamSO));
        foreach (RamSO temp in subListObjects3)
        {
            allRamComponents.Add(temp);
        }
    }
    public void fillMotherboardList()
    {
        allMotherboardComponents = new List<MotherboardSO>();
        Object[] subListObjects4 = Resources.LoadAll("ScriptableObjects/Motherboard", typeof(MotherboardSO));
        foreach (MotherboardSO temp in subListObjects4)
        {
            allMotherboardComponents.Add(temp);
        }
    }
    #endregion 
    #region Button Functions
    public void CloseButton()
    {
        GameObject.Find("Shop_Canvas").GetComponentInChildren<Canvas>().enabled = false;
        GameObject.Find("UI_Canvas").GetComponentInChildren<Canvas>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }


    public void ChangeTabToGpu()
    {
        cType = PC_Component.ComponentType.GPU;
        tabChanged = true;
    }
    public void ChangeTabToCpu()
    {
        cType = PC_Component.ComponentType.CPU;
        tabChanged = true;
    }
    public void ChangeTabToMotherboard()
    {
        cType = PC_Component.ComponentType.Motherboard;
        tabChanged = true;
    }
    public void ChangeTabToRam()
    {
        cType = PC_Component.ComponentType.RAM;
        tabChanged = true;
    }

    #endregion

}
