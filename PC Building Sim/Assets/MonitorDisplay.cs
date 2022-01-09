using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorDisplay : MonoBehaviour
{
    public Material missingComponentsMaterial;
    public Material startedComputerMaterial;

    public void SetStartComputerMaterial()
    {
        gameObject.GetComponent<MeshRenderer>().material = startedComputerMaterial;
    }

    public void SetMissingPartsMaterial()
    {
        gameObject.GetComponent<MeshRenderer>().material = missingComponentsMaterial;
    }

}
