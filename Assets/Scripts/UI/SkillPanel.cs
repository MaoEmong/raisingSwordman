using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ��ųâUI
public class SkillPanel : MonoBehaviour
{
    // ��ũ�Ѻ��� COntent������Ʈ
    public GameObject Content;

    ScrollRect scrollRect;

    void Start()
    {
        // �����ͷ� ����� ��ų�� ����ŭ ��ų���� ����
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

        // ��ũ�Ѻ��� �� ���� �̵�ó��
		Managers.CallWaitForOneFrame(() => {
            scrollRect.normalizedPosition = new Vector2(0f, 1f);
        });

    }

    void Update()
    {
        
    }
}
