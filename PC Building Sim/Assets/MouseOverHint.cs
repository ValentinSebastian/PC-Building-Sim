using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverHint : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI itemNameText;

    [SerializeField]
    private Color textColor;
    private void Start()
    {
        itemNameText.enabled = false;
    }
    void OnMouseEnter()
    {
        itemNameText.enabled = true;
    }
    void OnMouseExit()
    {
        itemNameText.enabled = false;
    }
}
