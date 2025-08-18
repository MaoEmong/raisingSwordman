using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    ScrollRect scrollRect;

    void Start()
    {
        scrollRect = transform.GetComponentInChildren<ScrollRect>();

        Managers.CallWaitForOneFrame(() => {
            scrollRect.normalizedPosition = new Vector2(0f, 1f);
        });
    }
}
