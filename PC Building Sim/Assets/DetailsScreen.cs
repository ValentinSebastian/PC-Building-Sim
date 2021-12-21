using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailsScreen : MonoBehaviour
{

    public TMPro.TextMeshProUGUI itemName;
    public TMPro.TextMeshProUGUI itemSpec1;
    public TMPro.TextMeshProUGUI itemSpec2;
    public TMPro.TextMeshProUGUI itemSpec3;
    public TMPro.TextMeshProUGUI itemSpec4;
    public TMPro.TextMeshProUGUI itemSpec5;
    public TMPro.TextMeshProUGUI itemSpec6;

    void Start()
    {
        transform.localScale = Vector2.zero;
    }

    public void MinimizeDetails()
    {
        transform.LeanScale(Vector2.zero, 0.5f).setEaseInBack();
    }
}
