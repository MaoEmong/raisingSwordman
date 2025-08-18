using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    ScrollRect scrollRect;

    public BuffList buffList;

    void Start()
    {
        scrollRect = transform.GetComponentInChildren<ScrollRect>();
        scrollRect.normalizedPosition = new Vector2(0f, 1f);
    }

}
