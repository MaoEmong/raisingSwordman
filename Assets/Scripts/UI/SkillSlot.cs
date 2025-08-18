using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ��ų ����
public class SkillSlot : MonoBehaviour
{
    // ȭ�鿡 ��µǴ�UI
    public Text SkillLevelText;         // ���� �ؽ�Ʈ
    public Text SkillCooltimeText;      // ��Ÿ�� �ؽ�Ʈ
    public Text SkillNameText;          // ��ų�̸� �ؽ�Ʈ
    public Text SkillExplaneText;       // ��ų ���� �ؽ�Ʈ
    public Image SkillImage;            // ��ų �̹��� 
    public Image SkillBlackImage;       // ��ų���� ��Ÿ�� �������̹���
    public Text TimerText;              // ���ð� ǥ�� �ؽ�Ʈ

    public float CoolTime;              // ��ų ��Ÿ��

    public int SkillCode;               // ��ų �ڵ�

    public float curTime = 0.0f;        // ��ų ��Ÿ�� ����ð�
    public float endTime = 10.0f;       // ��ų ��Ÿ�� 

    public void Init(int skillcode)
    {
        // �ش� ��ų�ڵ忡 �´� ������ �޾ƿ���
        GameData.SkillData skill = Managers.GData.Skill[skillcode];
        // �޾ƿ� ��ų �����͸� ������� �⺻�� ����
        SkillCode = skill.SkillCode;
        SkillLevelText.text = "Lv.1";
        SkillCooltimeText.text = $"��Ÿ�� : {skill.CoolTime.ToString("F1")}s";
        CoolTime = skill.CoolTime;
		SkillNameText.text = skill.Name;
        SkillExplaneText.text = skill.Explane;
        SkillImage.sprite = Managers.Resource.Load<Sprite>($"Sprite/SkillIcon/{SkillCode}");

        // ��Ÿ�� ����
		endTime = skill.CoolTime;
        curTime = endTime;

        TimerText.text = "";


		SkillBlackImage.enabled = false;
    }

    void Update()
    {
        // ��ų��Ÿ���� 0���� �����ؼ� ���Ѱ����� ���ԵǸ� ����Ҽ� �ְ� ����
        // ������Ʈ���� ���� ��Ÿ�� üũ 
        if (curTime >= endTime)
        {
            curTime = endTime;
            if(SkillBlackImage.enabled)
            {
                SkillBlackImage.enabled = false;
                TimerText.text = "";
			}
        }
        else
        {
            curTime += Time.deltaTime;
			if (!SkillBlackImage.enabled)
			{
				SkillBlackImage.enabled = true;

			}

            // ��ų�� ��Ÿ�����϶� ȭ���� ������ �̹���
            SkillBlackImage.fillAmount = 1.0f - (curTime/endTime);
            float time = CoolTime - curTime;
            // ���� ���ð� ǥ��
            TimerText.text = $"{time.ToString("F1")}";
		}


    }

    // ��ư �ݹ�
    public void SkillButton()
    {
        // ���� ��ų�� �غ���� �ʾҴٸ� return
        if (curTime < endTime)
        {
			Managers.Sound.Play("Effect/UI/Failed");
			return;
        }
        // �غ� �Ǿ��ٸ�
        else
        {
            // ��ų��Ÿ�� �ʱ�ȭ
            curTime = 0.0f;
            Managers.Sound.Play("Effect/UI/SkillButton");
            // �÷��̾ũ��Ʈ�� ��ų ���
            Managers.Player.GetComponent<PlayerController>().PlayerSkill(SkillCode);

        }       

    }

}
