using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 스킬 슬롯
public class SkillSlot : MonoBehaviour
{
    // 화면에 출력되는UI
    public Text SkillLevelText;         // 레벨 텍스트
    public Text SkillCooltimeText;      // 쿨타임 텍스트
    public Text SkillNameText;          // 스킬이름 텍스트
    public Text SkillExplaneText;       // 스킬 설명 텍스트
    public Image SkillImage;            // 스킬 이미지 
    public Image SkillBlackImage;       // 스킬슬롯 쿨타임 진행중이미지
    public Text TimerText;              // 대기시간 표시 텍스트

    public float CoolTime;              // 스킬 쿨타임

    public int SkillCode;               // 스킬 코드

    public float curTime = 0.0f;        // 스킬 쿨타임 현재시간
    public float endTime = 10.0f;       // 스킬 쿨타임 

    public void Init(int skillcode)
    {
        // 해당 스킬코드에 맞는 데이터 받아오기
        GameData.SkillData skill = Managers.GData.Skill[skillcode];
        // 받아온 스킬 데이터를 기반으로 기본값 설정
        SkillCode = skill.SkillCode;
        SkillLevelText.text = "Lv.1";
        SkillCooltimeText.text = $"쿨타임 : {skill.CoolTime.ToString("F1")}s";
        CoolTime = skill.CoolTime;
		SkillNameText.text = skill.Name;
        SkillExplaneText.text = skill.Explane;
        SkillImage.sprite = Managers.Resource.Load<Sprite>($"Sprite/SkillIcon/{SkillCode}");

        // 쿨타임 설정
		endTime = skill.CoolTime;
        curTime = endTime;

        TimerText.text = "";


		SkillBlackImage.enabled = false;
    }

    void Update()
    {
        // 스킬쿨타임은 0부터 시작해서 제한값까지 차게되면 사용할수 있게 제작
        // 업데이트마다 현재 쿨타임 체크 
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

            // 스킬이 쿨타임중일때 화면을 가리는 이미지
            SkillBlackImage.fillAmount = 1.0f - (curTime/endTime);
            float time = CoolTime - curTime;
            // 남은 대기시간 표시
            TimerText.text = $"{time.ToString("F1")}";
		}


    }

    // 버튼 콜백
    public void SkillButton()
    {
        // 아직 스킬이 준비되지 않았다면 return
        if (curTime < endTime)
        {
			Managers.Sound.Play("Effect/UI/Failed");
			return;
        }
        // 준비가 되었다면
        else
        {
            // 스킬쿨타임 초기화
            curTime = 0.0f;
            Managers.Sound.Play("Effect/UI/SkillButton");
            // 플레이어스크립트의 스킬 사용
            Managers.Player.GetComponent<PlayerController>().PlayerSkill(SkillCode);

        }       

    }

}
