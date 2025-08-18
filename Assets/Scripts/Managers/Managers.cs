using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Ŵ��� �� ����
public class Managers : MonoBehaviour
{
	static Managers _instance;
	public static Managers Instance { get { return _instance; } }

	SoundManager _sound = new();
	ResourceManager _resource = new();
	PoolManager _pool = new();
	SceneController _scene = new();
	JsonManager _json = new();
	GameData _gdata = new();


	Define.SceneType _targetscene = Define.SceneType.MenuScene;

	public static SoundManager Sound { get { return Instance._sound; } }
	public static ResourceManager Resource { get { return Instance._resource; } }
	public static PoolManager Pool { get { return Instance._pool; } }
	public static SceneController Scene { get { return Instance._scene; } }
	public static JsonManager Json { get { return Instance._json; } }
	public static GameData GData { get { return Instance._gdata; } }

	public static Define.SceneType TargetScene { get { return Instance._targetscene; } set { Instance._targetscene = value; } }

	GameObject player = null;
	public static GameObject Player { get { return Instance.player; } set { Instance.player = value; } }

	// �ʵ� ��ũ�� �ӵ�
	public static float MoveSpeed = 2.2f;

	private void Awake()
	{
		Init();
	}

	static void Init()
	{
		if(_instance == null)
		{
			GameObject go = GameObject.Find("Managers");
			if( go == null)
			{
				go = new GameObject { name = "Managers" };
				go.AddComponent<Managers>();
			}

			DontDestroyOnLoad(go);
			_instance = go.GetComponent<Managers>();

			Application.targetFrameRate = 30;

			// �� �Ŵ��� �ʱ�ȭ (���� ����!!!!)
			_instance._json.Init();

			_instance._gdata.Init();
			_instance._pool.Init();


			_instance._sound.Init();
		}
	}

	public static void Clear()
	{
		Sound.Clear();
		Pool.Clear();
		Scene.Clear();
		Player = null;
	}

	public static void TimePause()
	{
		Time.timeScale = 0;
	}

	public static void TimePlay()
	{
		Time.timeScale = 1;
	}

	// ������ �ڷ�ƾ�Լ� ����� ���� ���ٽ�
	public static void CallWaitForOneFrame(Action action)
	{
		Instance.StartCoroutine(DoCallWaitForOneFrame(action));
	}
	public static void CallWaitForSeconds(float time, Action action)
	{
		Instance.StartCoroutine(DoCallWaitForSeconds(time, action));
	}
	private static IEnumerator DoCallWaitForOneFrame(Action action)
	{
		yield return null;
		action();
	}
	private static IEnumerator DoCallWaitForSeconds(float time, Action action)
	{
		yield return new WaitForSeconds(time);
		action();
	}
}
