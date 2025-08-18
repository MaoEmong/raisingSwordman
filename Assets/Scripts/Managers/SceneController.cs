using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// �� ������ ��Ʈ�ѷ�(�� �Ŵ����� ������̳� SceneManager�� ��ġ�⿡ �̸� �ٲ�)
public class SceneController
{
	// ���� ������� ���� ����
	public SceneBase CurScene;
	// ������ ������ �̵�
	public void LeadScene(Define.SceneType type)
	{
		Managers.Clear();

		// �� ��ȯ �� �÷��̾� ������ ����
		Managers.GData.SaveData();

		// �񵿱� �� ��ȯ
		SceneManager.LoadSceneAsync(GetSceneName(type));
	}

	// enum���� string���� ��ȯ
	string GetSceneName(Define.SceneType type)
	{
		string name = Enum.GetName(typeof(Define.SceneType), type);
		return name;
	}
	// ���� �� �����
	public void RestartScene()
	{
		Managers.Clear();

		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
	}

	public void Clear()
	{
		CurScene.Clear();
		CurScene = null;
	}
}
