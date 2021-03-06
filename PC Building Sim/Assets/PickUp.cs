using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform theDestination;
    private Color startcolor;
    void OnMouseDown()
    {
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        this.transform.position = theDestination.position;
        this.transform.parent = GameObject.Find("PickUpLocation").transform;
    }

    private void OnMouseUp()
    {
        this.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<BoxCollider>().enabled = true;
    }

    void OnMouseEnter()
    {
        startcolor = GetComponent<Outline>().OutlineColor;
        GetComponent<Outline>().OutlineColor = Color.red;
    }
    void OnMouseExit()
    {
        GetComponent<Outline>().OutlineColor = startcolor;
    }
}
