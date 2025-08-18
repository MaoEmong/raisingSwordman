using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class MenuScene : SceneBase
{
    public Image BlackImage;

    void Start()
    {
        Managers.Scene.CurScene = this;
        type = Define.SceneType.MenuScene;

        Managers.Sound.Play("BGM/TitleBGM", Define.Sound.Bgm);

        BlackImage.gameObject.SetActive(true);
        StartCoroutine(MyTools.ImageFadeOut(BlackImage, 0.4f));
        Managers.CallWaitForSeconds(0.4f, () => { BlackImage.gameObject.SetActive(false); });

    }
    
    public void CallMenuSound()
    {
		Managers.Sound.Play("Effect/UI/MenuButton");
	}

	public override void Clear()
    {
    }
}

