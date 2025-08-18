using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUpgradePanel : MonoBehaviour
{
    public enum CristalUpgradeType
    {
        Attack,
        Timer,


        MaxCount
    }


    public Transform Content;

    ScrollRect scrollRect;


    void Start()
    {
		scrollRect = transform.GetComponentInChildren<ScrollRect>();
        
        for(int i = 0; i < (int)CristalUpgradeType.MaxCount; i++)
        {
            GameObject menuUpgradeSlot = Managers.Pool.Pop(Managers.Resource.Load<GameObject>("Prefab/CristalUpgradeSlot"));
            menuUpgradeSlot.transform.parent = Content;
            menuUpgradeSlot.transform.localPosition = Vector3.zero;
            menuUpgradeSlot.transform.localScale = Vector3.one;
            menuUpgradeSlot.GetComponent<CristalUpgradeSlot>().Init((CristalUpgradeType)i);
        }

		Managers.CallWaitForOneFrame(() => {
			scrollRect.normalizedPosition = new Vector2(0f, 1f);
		});

	}

	void Update()
    {
        
    }
}
