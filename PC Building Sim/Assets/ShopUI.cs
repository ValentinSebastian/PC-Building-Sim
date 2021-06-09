using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopUI : MonoBehaviour
{
    public PC_Component.ComponentType cType;
    public List<GameObject> allGpuComponents = new List<GameObject>();

    public void closeButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void fillGpuList()
    {
        allGpuComponents = new List<GameObject>();
        //allGpuComponents = ((GameObject[])Resources.LoadAll("Prefabs/GPU", typeof(GameObject))).ToList();
        Object[] subListObjects = Resources.LoadAll("Prefabs/Components/GPU", typeof(GameObject));
        foreach (GameObject temp in subListObjects)
        {
            allGpuComponents.Add(temp);
        }
        foreach (GameObject temp in allGpuComponents)
        {
            var temp2 = temp.gameObject.GetComponentInChildren<GPU_Component>();
            Debug.Log("Filling list with gpu : " + temp2.cName);
        }
        Debug.Log("done");
    }

}
