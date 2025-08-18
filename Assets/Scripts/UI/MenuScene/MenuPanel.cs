using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
	public Text CristalText;
	public Image BlackImage;

	private void Update()
	{
		CristalText.text = $"보유 크리스탈 : {Managers.GData.player.Cristal}";
	}
	
	public void CallGameStart()
	{
		Managers.TargetScene = Define.SceneType.MainScene;

		BlackImage.gameObject.SetActive(true);
		Color EmptyColor = BlackImage.color;
		EmptyColor.a = 0;
		BlackImage.color = EmptyColor;
		StartCoroutine(MyTools.ImageFadeIn(BlackImage, 0.3f));

		Managers.CallWaitForSeconds(0.3f, () => 
		{
			Managers.GData.player.StartGame();

			Managers.Scene.LeadScene(Define.SceneType.LoadScene); 
		});
		
	}

	public void CallGameExit()
	{
		Managers.CallWaitForSeconds(0.3f, () => { Application.Quit(); });
		
	}
}
