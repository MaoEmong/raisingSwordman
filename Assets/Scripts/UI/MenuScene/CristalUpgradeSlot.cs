using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CristalUpgradeSlot : MonoBehaviour
{
    public MenuUpgradePanel.CristalUpgradeType SlotType;

	public Text LevelText;
    public Text NameText;
    public int BuyCristal;
    public Text CristalText;


    public  void Init(MenuUpgradePanel.CristalUpgradeType type)
    {
        SlotType = type;

		LevelText.text = "Lv.XX";
        NameText.text = "테스트중 슬롯";
        BuyCristal = 9999999;
        CristalText.text = $"{BuyCristal}크리스탈";

        GameData.PlayerData player = Managers.GData.player;

		switch (SlotType)
        {
            case MenuUpgradePanel.CristalUpgradeType.Attack:
                LevelText.text = $"Lv.{Managers.GData.player.cristalUpgradeAttack}";
                NameText.text = "기본 공격력 증가";
                BuyCristal = 50 + (player.cristalUpgradeAttack * 100);
                CristalText.text = $"{BuyCristal}크리스탈";
                break;
			case MenuUpgradePanel.CristalUpgradeType.Timer:
				LevelText.text = $"Lv.{Managers.GData.player.cristalUpgradeTimer}";
				NameText.text = "게임 타이머 시간 증가";
				BuyCristal = 50 + (player.cristalUpgradeTimer * 100);
				CristalText.text = $"{BuyCristal}크리스탈";
				break;

        }

    }


    public void UpgradeSlot()
    {
		if (Managers.GData.player.Cristal < BuyCristal)
		{
			Debug.Log("Upgrade Failed");
			Managers.Sound.Play("Effect/UI/Failed");

			return;
		}
		else
		{
			Managers.GData.player.Cristal -= BuyCristal;
			Managers.Sound.Play("Effect/UI/BuyButton");

			GameData.PlayerData player = Managers.GData.player;

			switch (SlotType)
			{
				case MenuUpgradePanel.CristalUpgradeType.Attack:
					player.cristalUpgradeAttack++;
					LevelText.text = $"Lv.{Managers.GData.player.cristalUpgradeAttack}";
					NameText.text = "기본 공격력 증가";
					BuyCristal = 50 + (player.cristalUpgradeAttack * 100);
					CristalText.text = $"{BuyCristal}크리스탈";
					break;
				case MenuUpgradePanel.CristalUpgradeType.Timer:
					player.cristalUpgradeTimer++;
					LevelText.text = $"Lv.{Managers.GData.player.cristalUpgradeTimer}";
					NameText.text = "게임 타이머 시간 증가";
					BuyCristal = 50 + (player.cristalUpgradeTimer * 100);
					CristalText.text = $"{BuyCristal}크리스탈";
					break;

			}
		}

	}


}
