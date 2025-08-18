using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatePanel : MonoBehaviour
{
    public Text GoldText;
    public Text TimerText;
    public float MaxTimer;
    public float curTimer;
    GameData.PlayerData playerData;
    PlayerController player;

    public MainScene mainScene;

    bool isGameOver = false;

    public Image BlackImage;


	void Start()
    {
		player = GameObject.Find("Player").GetComponentInChildren<PlayerController>();
		playerData = Managers.GData.player;
        GoldText.text = $"°ñµå : {playerData.Gold.ToMoneyString()}G";
        MaxTimer = playerData.DefaultTimer + (playerData.cristalUpgradeTimer * playerData.CristalUpgradeTimerVal);
        curTimer = MaxTimer;
        TimerText.text = curTimer.ToString("F0");

	}

    void Update()
    {
        if (isGameOver)
        {
            return;
        }

		GoldText.text = $"°ñµå : {playerData.Gold.ToMoneyString()}G";

        if (player.isAttack)
        {
            curTimer -= Time.deltaTime;
        }
        else
            curTimer = MaxTimer;

		TimerText.text = curTimer.ToString("F0");

        if(curTimer<=0.0f)
        {
            if(!isGameOver)
            {
                GameOver();
            }
        }

	}

    public void GameOver()
    {
		isGameOver = true;
		mainScene.GameOver();
		Managers.CallWaitForSeconds(0.5f, () =>
		{
			BlackImage.gameObject.SetActive(true);
			player.GameOver();
			StartCoroutine(MyTools.ImageFadeIn(BlackImage, 0.3f));
			BlackImage.GetComponent<GameOver>().GameOverAction();
		});

	}
}
