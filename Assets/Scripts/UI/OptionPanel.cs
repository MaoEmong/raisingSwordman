using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : MonoBehaviour
{// 사운드 설정용 UI 
	public Slider BGMSoundSlider;
	public Slider EffectSoundSlider;

	ScrollRect scrollRect;

	private void Start()
	{
		BGMSoundSlider.value = Managers.Sound.GetBGMVol();
		EffectSoundSlider.value = Managers.Sound.GetEffectVol();

		BGMSoundSlider.onValueChanged.AddListener(Managers.Sound.SetBGMVol);
		EffectSoundSlider.onValueChanged.AddListener(Managers.Sound.SetEffectVol);

		scrollRect = transform.GetComponentInChildren<ScrollRect>();
		Managers.CallWaitForOneFrame(() => { scrollRect.normalizedPosition = new Vector2(0f, 1f); });

	}

	// 타이틀로 돌아가기
	public void GoToMenu()
	{
		Managers.TargetScene = Define.SceneType.MenuScene;

		Managers.CallWaitForSeconds(0.3f, () => {
			Managers.GData.player.EndGame();
			Managers.Scene.LeadScene(Define.SceneType.LoadScene);
		});

	}


	// 게임 종료 콜백함수
	public void GameExit()
	{
		Managers.CallWaitForSeconds(0.3f, () => {
			Managers.GData.player.EndGame();
			Application.Quit();
		});
		
	}

}
