using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public TMPro.TextMeshProUGUI itemName;
    public TMPro.TextMeshProUGUI itemPrice;
    public TMPro.TextMeshProUGUI itemId;

    public TMPro.TextMeshProUGUI itemSpec1;
    public TMPro.TextMeshProUGUI itemSpec2;
    public TMPro.TextMeshProUGUI itemSpec3;
    public TMPro.TextMeshProUGUI itemSpec4;
    public TMPro.TextMeshProUGUI itemSpec5;
    public TMPro.TextMeshProUGUI itemSpec6;
    public Button BuyButton;
    public Button MoreInfoButton;


    public ShopItem(string _iName , string _iPrice , string _itemId)
    {
        itemName.text = _iName;
        itemPrice.text = _iPrice;
        itemId.text = _itemId;
    }
}
