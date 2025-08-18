using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameOver : MonoBehaviour, IPointerUpHandler,IPointerDownHandler
{
	public GameObject GameOverPanel;
	public GameObject GameOverText;
	public GameObject ScrollView;
	public GameObject[] ScrollTexts;
	public GameObject TouchText;

	public Image BlackImage;

	bool isAction = false;

	void Start()
	{
		GameOverPanel.SetActive(false);
		GameOverText.SetActive(false);
		ScrollView.SetActive(false);
		for(int i = 0; i < ScrollTexts.Length; i++)
		{
			ScrollTexts[i].SetActive(false);
			
		}
		TouchText.SetActive(false);

	}


	public void GameOverAction()
	{
		if (isAction)
			return;
		else
		{
			StartCoroutine(GameOverCor());
			isAction = true;
		}

	}

	IEnumerator GameOverCor()
	{
		yield return new WaitForSeconds(0.5f);
		GameOverPanel.SetActive(true);
		GameOverText?.SetActive(true);
		ScrollView?.SetActive(true);
		yield return new WaitForSeconds(0.8f);
		GameData.PlayerData player = Managers.GData.player;

		int total = 0;
		total += player.UpgradeAttack;
		total += player.UpgradeAttackSpeed;
		total += player.UpgradeCritical;
		total += player.UpgradeGetGold;

		int getCristal = total + player.KillCount;

		for (int i = 0; i < ScrollTexts.Length; i++)
		{
			yield return new WaitForSeconds(0.3f);
			ScrollTexts[i].SetActive(true);
			Text scrollT = ScrollTexts[i].GetComponent<Text>();

			switch(i)
			{
				case 0:
					scrollT.text = $"레벨 총합 : {total}";
					break;
				case 1:
					scrollT.text = $"몬스터 처치 : {player.KillCount}";
					break;
				case 2:
					scrollT.text = $"남은 골드 : {player.Gold}";
					break;
				case 3:
					scrollT.text = "";
					break;
				case 4:
					scrollT.text = $"크리스탈 변환 : {getCristal}";
					Managers.GData.player.Cristal += getCristal;
					break;
			}

		}
		yield return new WaitForSeconds(0.5f);
		TouchText.SetActive(true);

	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if(TouchText.activeSelf)
		{
			// 메뉴 화면으로 전환
			Managers.TargetScene = Define.SceneType.MenuScene;
			BlackImage.gameObject.SetActive(true);

			Color EmptyColor = new Color(0,0,0,0);
			BlackImage.color = EmptyColor;

			StartCoroutine(MyTools.ImageFadeIn(BlackImage, 0.3f));

			Managers.CallWaitForSeconds(0.3f, () => {
				Managers.Scene.LeadScene(Define.SceneType.LoadScene);
			});
		}
	}





	public void OnPointerDown(PointerEventData eventData)
	{
	}
}

