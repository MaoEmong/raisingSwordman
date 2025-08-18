using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScene : SceneBase
{
	public Image BlackImage;

	public BackgroundScroll Scroll;
	public EnemySpawner Spawner;

	public GameObject enemy;

	public int UpgradeAtk;
	public int UpgradeAtkspeed;
	public int Upgradecritical;
	public int Upgradegetgold;

	public bool isGameOver = false;
	public Coroutine BgmCor;

	private void Start()
	{
		type = Define.SceneType.MainScene;
		Managers.Scene.CurScene = this;

		StartCoroutine(MyTools.ImageFadeOut(BlackImage, 0.3f));
		Managers.CallWaitForSeconds(0.3f, () => { BlackImage.gameObject.SetActive(false); });
		Managers.CallWaitForSeconds(0.3f, () => 
		{
			BgmCor = StartCoroutine(RandomBGMPlay()); 
		});

		enemy = Spawner.SpawnEnemy();

	}

	IEnumerator RandomBGMPlay()
	{
		Managers.Sound.Play("BGM/BackgroundBGM1", Define.Sound.Bgm);
		int killCount = Managers.GData.player.KillCount;
		int curCount = 0;
		while(true)
		{
			yield return null;

			curCount = Managers.GData.player.KillCount - killCount;

			if(curCount>= 10)
			{
				curCount = 0;
				killCount = Managers.GData.player.KillCount;

				int RandVal = Random.Range(1, 10);

				Managers.Sound.Play($"BGM/BackgroundBGM{RandVal}", Define.Sound.Bgm);

			}


		}


	}

	private void Update()
	{
		if(isGameOver)
		{

			return;
		}

		if(!enemy.activeSelf)
		{
			enemy = Spawner.SpawnEnemy();
		}

		CheckPlayerData();
	}

	void CheckPlayerData()
	{
		GameData.PlayerData player = Managers.GData.player;

		UpgradeAtk = player.UpgradeAttack;
		UpgradeAtkspeed = player.UpgradeAttackSpeed;
		Upgradecritical = player.UpgradeCritical;
		Upgradegetgold = player.UpgradeGetGold;
	}


	public void GameOver()
	{
		isGameOver = true;
		Managers.CallWaitForSeconds(0.5f, () => 
		{
			StopCoroutine(BgmCor); 
			Managers.Sound.BgmStop(); 
		});
		
		Scroll.isScroll = false;

	}

	public override void Clear()
	{
	}
}
