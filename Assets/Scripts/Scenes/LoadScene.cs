using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : SceneBase
{
	public Text loadText;
	string loading = "�ε���";
	string loadend = "�ε� �Ϸ�!";
	bool isEnd = false;

	void Start()
    {
		type = Define.SceneType.LoadScene;
		Managers.Scene.CurScene = this;

		Managers.Sound.Clear();

		StartCoroutine(LoadTextAction());
		StartCoroutine(LoadSceneAsync());
	}

	IEnumerator LoadTextAction()
	{
		int count = 0;
		loadText.text = loading;

		while (!isEnd)
		{
			yield return new WaitForSeconds(0.3f);
			loadText.text = $"{loadText.text}.";  
			
			if(count > 3)
			{
				count = 0;
				loadText.text = loading;
			}
			
			count++;
		}
		loadText.text = loadend;
	}

	IEnumerator LoadSceneAsync()
	{
		AsyncOperation op = SceneManager.LoadSceneAsync(Managers.TargetScene.ToString());
		op.allowSceneActivation = false;
		yield return new WaitForSeconds(1.0f);
		while (!op.isDone)
		{
			yield return new WaitForSeconds(0.1f);
			// ����ȯ�� ���� �������� ��� ���
			if (op.progress >= 0.9f)
			{
				isEnd = true;
				// ��� ���
				yield return new WaitForSeconds(0.8f);
				op.allowSceneActivation = true;
				yield break;
			}
		}
	}

    public override void Clear()
    {
    }

}
