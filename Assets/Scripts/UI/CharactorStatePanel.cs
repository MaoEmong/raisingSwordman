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
            $"���ݷ� : {Player.DefaultAttack + (Player.UpgradeAttackVal * Player.UpgradeAttack)}";
		SpeedText.text =
			$"���ݼӵ� : {Player.DefaultAttackSpeed + (Player.UpgradeAttackSpeedVal * Player.UpgradeAttackSpeed)}";
		CriticalText.text =
			$"ġ��Ÿ : {Player.Critical + (Player.UpgradeCriticalVal * Player.UpgradeCritical)}";
        KillCountText.text = $"óġ �� : {Player.KillCount}";
        GetGoldText.text = 
            $"ȹ�� ��� : {Player.GetMinGold} ~ {MoneyUnitString.ToString( Player.GetMaxGold + (Player.UpgradeGetGoldVal * Player.UpgradeGetGold))}";
	}

    void Update()
    {
        RefreshText();
    }

    void RefreshText()
    {
		GameData.PlayerData Player = Managers.GData.player;

		AttackText.text =
			$"���ݷ� : {Player.DefaultAttack + (Player.UpgradeAttackVal * Player.UpgradeAttack)}";
		SpeedText.text =
			$"���ݼӵ� : {Player.DefaultAttackSpeed + (Player.UpgradeAttackSpeedVal * Player.UpgradeAttackSpeed)}";
		CriticalText.text =
			$"ġ��Ÿ : {Player.Critical + (Player.UpgradeCriticalVal * Player.UpgradeCritical)}";
		KillCountText.text = $"óġ �� : {Player.KillCount}";
		GetGoldText.text =
			$"ȹ�� ��� : {Player.GetMinGold} ~ {MoneyUnitString.ToString(Player.GetMaxGold + (Player.UpgradeGetGoldVal * Player.UpgradeGetGold))}";
	}
}
