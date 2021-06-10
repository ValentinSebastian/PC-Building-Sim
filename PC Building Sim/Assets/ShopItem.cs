using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public TMPro.TextMeshProUGUI itemName;
    public TMPro.TextMeshProUGUI itemPrice;
    public TMPro.TextMeshProUGUI itemId;

    public ShopItem(string _iName , string _iPrice , string _itemId)
    {
        itemName.text = _iName;
        itemPrice.text = _iPrice;
        itemId.text = _itemId;
    }
}
