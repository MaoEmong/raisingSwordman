using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 스킬창UI
public class SkillPanel : MonoBehaviour
{
    // 스크롤뷰의 COntent오브젝트
    public GameObject Content;

    ScrollRect scrollRect;

    void Start()
    {
        // 데이터로 저장된 스킬의 수만큼 스킬슬롯 생성
        for(int i = 0; i < Managers.GData.Skill.Count; i++)
        {
            if (Managers.GData.Skill[i].CoolTime <= 0)
                continue;

            GameObject skillSlot = Managers.Pool.Pop(Managers.Resource.Load<GameObject>("Prefab/SkillSlot"));
            skillSlot.transform.parent = Content.transform;
            skillSlot.transform.localScale = Vector3.one;
            skillSlot.GetComponent<SkillSlot>().Init(i);

        }

		scrollRect = transform.GetComponentInChildren<ScrollRect>();

        // 스크롤뷰의 맨 위로 이동처리
		Managers.CallWaitForOneFrame(() => {
            scrollRect.normalizedPosition = new Vector2(0f, 1f);
        });

    }

    void Update()
    {
        
    }
}
