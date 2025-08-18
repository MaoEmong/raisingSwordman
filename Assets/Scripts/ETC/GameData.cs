using System.Collections.Generic;
using System.IO;
using UnityEngine;

// ���ӵ����� ���� / �Ŵ��� ��ũ��Ʈ���� ����
public class GameData
{
	// �÷��̾� ����
	public class PlayerData
	{
		public double Gold = 0;
		public int Cristal = 0;
		public int KillCount = 0;

		public bool isAttackBuff = false;

		//==============��� ���׷��̵� ���===========
		public int UpgradeAttack = 0;
		public int UpgradeAttackSpeed = 0;
		public int UpgradeCritical = 0;
		public int UpgradeGetGold = 0;

		//=============�ɼ� ������==============
		public float BGMVol = 1.0f;
		public float EffectVol = 1.0f;

		//============ũ����Ż ���׷��̵�=================
		public int cristalUpgradeTimer = 0;
		public int cristalUpgradeAttack = 0;

		//============================ �Ʒ� ��� �б� ���� =====================

		//=============�⺻ ��===============
		public int DefaultAttack = 2;
		readonly public float DefaultAttackSpeed = 1.0f;
		readonly public float Critical = 0.0f;
		readonly public int GetMinGold = 10;
		readonly public int GetMaxGold = 20;

		public float DefaultTimer = 60.0f;

		//=============��� ���׷��̵� �߰� ��==========
		readonly public int UpgradeAttackVal = 2;
		readonly public float UpgradeAttackSpeedVal = 0.01f;
		readonly public float UpgradeCriticalVal = 0.3f;
		readonly public int UpgradeGetGoldVal = 5;

		//==============ũ����Ż ���׷��̵� �߰� ��=========
		readonly public int CristalUpgradeTimerVal = 1;
		readonly public int CristalUpgradeAttackVal = 3;

		public PlayerData() 
		{
			Gold = 0;
			KillCount = 0;
			UpgradeAttack = 0;
			UpgradeAttackSpeed = 0;
			UpgradeCritical = 0;
			UpgradeGetGold = 0;
		}
		public PlayerData(int level, double gold, int kills, int UAttack, int UASpeed, int UCri, int UGetGold) 
		{
			Gold = gold;
			KillCount = kills;
			UpgradeAttack = UAttack;
			UpgradeAttackSpeed = UASpeed;
			UpgradeCritical = UCri;
			UpgradeGetGold = UGetGold;
		}

		public void StartGame()
		{
			Gold = 0;
			KillCount = 0;
			UpgradeAttack = 0;
			UpgradeAttackSpeed = 0;
			UpgradeCritical = 0;
			UpgradeGetGold = 0;

			DefaultAttack = 2 + (CristalUpgradeAttackVal * cristalUpgradeAttack);
			DefaultTimer = 60 + (CristalUpgradeTimerVal * cristalUpgradeTimer);
		}

		public void EndGame()
		{
			Gold = 0;
			KillCount = 0;
			UpgradeAttack = 0;
			UpgradeAttackSpeed = 0;
			UpgradeCritical = 0;
			UpgradeGetGold = 0;

			DefaultAttack = 2;
			DefaultTimer = 60;

		}

	}

	public class SkillData
	{
		public int SkillCode;
		public string Name;
		public float CoolTime;
		public string Explane;

		public SkillData() { }
		public SkillData(int code, string name, float coolTime, string explane)
		{
			SkillCode = code;
			Name = name;
			CoolTime = coolTime;
			Explane = explane;
		}	
	}


	public PlayerData player = new();
	public List<SkillData> Skill = new();

	// ���� �ʱ�ȭ
	public void Init()
	{
		// �÷��̾� ������ �޾ƿ���
		string Path;
#if UNITY_EDITOR
		Path = Application.dataPath;
#else
		Path = Application.persistentDataPath;
#endif
		// �÷��̾� ������ ��������
		if(File.Exists($"{Path}/JsonData/PlayerData.json"))
		{
			Debug.Log("File is Find");
			player = Managers.Json.ImportJsonData<PlayerData>($"JsonData", "PlayerData");
			Debug.Log("Load File");
			if(player.isAttackBuff)
			{
				player.DefaultAttack -= 20;
				player.isAttackBuff = false;
			}
		}
		else
		{
			Debug.Log("File is NotFound");
			string playerdata = Managers.Json.ObjectToJson(player);
			Managers.Json.ExportJsonData("JsonData", "PlayerData", playerdata);
		}

		Skill = Managers.Json.ImportReadOnlyJsonData<List<SkillData>>("SkillData");

	}
	// ���� ������ ����
	public void SaveData()
	{
		if(player.isAttackBuff)
		{
			player.DefaultAttack -= 20;
			player.isAttackBuff = false;
		}
		string playerdata = Managers.Json.ObjectToJson(player);
		Managers.Json.ExportJsonData("JsonData","PlayerData",playerdata);
	}
	// ���� ������ ����
	public void Clear()
	{
		player = new();
		SaveData();
	}
}
