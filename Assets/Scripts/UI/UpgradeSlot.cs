using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSlot : MonoBehaviour
{
    public enum UpgradeType
    {
        Attack,
        AttackSpeed,
        Critical,
        GetGold,

    }

    public UpgradeType type = UpgradeType.Attack;
    public Text GoldText;
    public double UpgradeGold;
    public Text LevelText;

    public void Start()
    {
        int i = 0;
		switch (type)
		{
			case UpgradeType.Attack:
				i = Managers.GData.player.UpgradeAttack;
				break;
			case UpgradeType.AttackSpeed:
				i = Managers.GData.player.UpgradeAttackSpeed;
				break;
			case UpgradeType.Critical:
				i = Managers.GData.player.UpgradeCritical;
				break;
            case UpgradeType.GetGold:
				i = Managers.GData.player.UpgradeGetGold;
				break;
		}
		LevelText.text =$"Lv.{i}";  
		UpgradeGold = 50 + (50 * i);
        GoldText.text = $"°ñµå : {UpgradeGold.ToMoneyString()}G";
	}

    public void SlotUpgrade()
    {
        if (Managers.GData.player.Gold < UpgradeGold)
        {
			Managers.Sound.Play("Effect/UI/Failed");
            return;
        }
        else
        {
            Managers.GData.player.Gold -= UpgradeGold;
			Managers.Sound.Play("Effect/UI/BuyButton");

			Debug.Log("Upgrade!");

			switch (type)
            {
                case UpgradeType.Attack:
                    Managers.GData.player.UpgradeAttack++;
                    break;
                case UpgradeType.AttackSpeed:
                    Managers.GData.player.UpgradeAttackSpeed++;
                    break;
                case UpgradeType.Critical:
                    Managers.GData.player.UpgradeCritical++;
                    break;
				case UpgradeType.GetGold:
					Managers.GData.player.UpgradeGetGold++;
					break;
			}

			int i = 0;
			switch (type)
			{
				case UpgradeType.Attack:
					i = Managers.GData.player.UpgradeAttack;
					break;
				case UpgradeType.AttackSpeed:
					i = Managers.GData.player.UpgradeAttackSpeed;
					break;
				case UpgradeType.Critical:
					i = Managers.GData.player.UpgradeCritical;
					break;
				case UpgradeType.GetGold:
					i = Managers.GData.player.UpgradeGetGold;
					break;
			}

			LevelText.text = $"Lv.{i}";
			UpgradeGold = 50 + (50 * i);
			GoldText.text = $"°ñµå : {UpgradeGold.ToMoneyString()}G";

			Managers.GData.SaveData();

		}
	}

}
