using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimizeAtStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector2.zero;
    }

    public void MinimizeObject()
    {
        transform.LeanScale(Vector2.zero, 0.5f).setEaseInBack();
    }

}
