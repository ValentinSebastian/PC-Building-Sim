using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightOnHover : MonoBehaviour
{ 
    private Color startcolor;

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
