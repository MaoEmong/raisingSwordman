using System.Collections.Generic;
using System.IO;
using UnityEngine;

// 게임데이터 관리 / 매니저 스크립트에서 관리
public class GameData
{
	// 플레이어 정보
	public class PlayerData
	{
		public double Gold = 0;
		public int Cristal = 0;
		public int KillCount = 0;

		public bool isAttackBuff = false;

		//==============골드 업그레이드 목록===========
		public int UpgradeAttack = 0;
		public int UpgradeAttackSpeed = 0;
		public int UpgradeCritical = 0;
		public int UpgradeGetGold = 0;

		//=============옵션 데이터==============
		public float BGMVol = 1.0f;
		public float EffectVol = 1.0f;

		//============크리스탈 업그레이드=================
		public int cristalUpgradeTimer = 0;
		public int cristalUpgradeAttack = 0;

		//============================ 아래 목록 읽기 전용 =====================

		//=============기본 값===============
		public int DefaultAttack = 2;
		readonly public float DefaultAttackSpeed = 1.0f;
		readonly public float Critical = 0.0f;
		readonly public int GetMinGold = 10;
		readonly public int GetMaxGold = 20;

		public float DefaultTimer = 60.0f;

		//=============골드 업그레이드 추가 값==========
		readonly public int UpgradeAttackVal = 2;
		readonly public float UpgradeAttackSpeedVal = 0.01f;
		readonly public float UpgradeCriticalVal = 0.3f;
		readonly public int UpgradeGetGoldVal = 5;

		//==============크리스탈 업그레이드 추가 값=========
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

	// 최초 초기화
	public void Init()
	{
		// 플레이어 데이터 받아오기
		string Path;
#if UNITY_EDITOR
		Path = Application.dataPath;
#else
		Path = Application.persistentDataPath;
#endif
		// 플레이어 데이터 가져오기
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
	// 게임 데이터 저장
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
	// 게임 데이터 삭제
	public void Clear()
	{
		player = new();
		SaveData();
	}
}
