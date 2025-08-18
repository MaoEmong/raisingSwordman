using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharactorStatePanel : MonoBehaviour
{
    public Text AttackText;
    public Text SpeedText;
    public Text CriticalText;
    public Text KillCountText;
    public Text GetGoldText;
    void Start()
    {
        GameData.PlayerData Player = Managers.GData.player;

        AttackText.text = 
            $"공격력 : {Player.DefaultAttack + (Player.UpgradeAttackVal * Player.UpgradeAttack)}";
		SpeedText.text =
			$"공격속도 : {Player.DefaultAttackSpeed + (Player.UpgradeAttackSpeedVal * Player.UpgradeAttackSpeed)}";
		CriticalText.text =
			$"치명타 : {Player.Critical + (Player.UpgradeCriticalVal * Player.UpgradeCritical)}";
        KillCountText.text = $"처치 수 : {Player.KillCount}";
        GetGoldText.text = 
            $"획득 골드 : {Player.GetMinGold} ~ {MoneyUnitString.ToString( Player.GetMaxGold + (Player.UpgradeGetGoldVal * Player.UpgradeGetGold))}";
	}

    void Update()
    {
        RefreshText();
    }

    void RefreshText()
    {
		GameData.PlayerData Player = Managers.GData.player;

		AttackText.text =
			$"공격력 : {Player.DefaultAttack + (Player.UpgradeAttackVal * Player.UpgradeAttack)}";
		SpeedText.text =
			$"공격속도 : {Player.DefaultAttackSpeed + (Player.UpgradeAttackSpeedVal * Player.UpgradeAttackSpeed)}";
		CriticalText.text =
			$"치명타 : {Player.Critical + (Player.UpgradeCriticalVal * Player.UpgradeCritical)}";
		KillCountText.text = $"처치 수 : {Player.KillCount}";
		GetGoldText.text =
			$"획득 골드 : {Player.GetMinGold} ~ {MoneyUnitString.ToString(Player.GetMaxGold + (Player.UpgradeGetGoldVal * Player.UpgradeGetGold))}";
	}
}
