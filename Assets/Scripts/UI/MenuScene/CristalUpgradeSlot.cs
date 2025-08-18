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
        NameText.text = "�׽�Ʈ�� ����";
        BuyCristal = 9999999;
        CristalText.text = $"{BuyCristal}ũ����Ż";

        GameData.PlayerData player = Managers.GData.player;

		switch (SlotType)
        {
            case MenuUpgradePanel.CristalUpgradeType.Attack:
                LevelText.text = $"Lv.{Managers.GData.player.cristalUpgradeAttack}";
                NameText.text = "�⺻ ���ݷ� ����";
                BuyCristal = 50 + (player.cristalUpgradeAttack * 100);
                CristalText.text = $"{BuyCristal}ũ����Ż";
                break;
			case MenuUpgradePanel.CristalUpgradeType.Timer:
				LevelText.text = $"Lv.{Managers.GData.player.cristalUpgradeTimer}";
				NameText.text = "���� Ÿ�̸� �ð� ����";
				BuyCristal = 50 + (player.cristalUpgradeTimer * 100);
				CristalText.text = $"{BuyCristal}ũ����Ż";
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
					NameText.text = "�⺻ ���ݷ� ����";
					BuyCristal = 50 + (player.cristalUpgradeAttack * 100);
					CristalText.text = $"{BuyCristal}ũ����Ż";
					break;
				case MenuUpgradePanel.CristalUpgradeType.Timer:
					player.cristalUpgradeTimer++;
					LevelText.text = $"Lv.{Managers.GData.player.cristalUpgradeTimer}";
					NameText.text = "���� Ÿ�̸� �ð� ����";
					BuyCristal = 50 + (player.cristalUpgradeTimer * 100);
					CristalText.text = $"{BuyCristal}ũ����Ż";
					break;

			}
		}

	}


}
