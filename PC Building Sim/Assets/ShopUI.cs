using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopUI : MonoBehaviour
{
    public static PC_Component.ComponentType cType;
    public List<GameObject> allGpuComponents = new List<GameObject>();
    public List<GameObject> allCpuComponents = new List<GameObject>();
    public List<GameObject> allRamComponents = new List<GameObject>();
    public List<GameObject> allMotherboardComponents = new List<GameObject>();
    public static bool tabChanged;

    #region Fill functions
    public void fillGpuList()
    {
        allGpuComponents = new List<GameObject>();
        Object[] subListObjects = Resources.LoadAll("Prefabs/Components/GPU", typeof(GameObject));
        foreach (GameObject temp in subListObjects)
        {
            Debug.Log("filling with gpus");
            allGpuComponents.Add(temp);
        }           
    }
    public void fillCpuList()
    {
        allCpuComponents = new List<GameObject>();
        Object[] subListObjects2 = Resources.LoadAll("Prefabs/Components/CPU", typeof(GameObject));
        foreach (GameObject temp in subListObjects2)
        {
            allCpuComponents.Add(temp);
        }      
    }
    public void fillRamList()
    {
        allRamComponents = new List<GameObject>();
        Object[] subListObjects3 = Resources.LoadAll("Prefabs/Components/RAM", typeof(GameObject));
        foreach (GameObject temp in subListObjects3)
        {
            allRamComponents.Add(temp);
        }
    }
    public void fillMotherboardList()
    {
        allMotherboardComponents = new List<GameObject>();
        Object[] subListObjects4 = Resources.LoadAll("Prefabs/Components/Motherboard", typeof(GameObject));
        foreach (GameObject temp in subListObjects4)
        {
            allMotherboardComponents.Add(temp);
        }
    }
    #endregion 
    #region Button Functions
    public void CloseButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
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
