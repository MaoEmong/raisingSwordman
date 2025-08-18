using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 매니저 총 관리
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

	// 필드 스크롤 속도
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

			// 각 매니저 초기화 (순서 주의!!!!)
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

	// 간단한 코루틴함수 사용을 위한 람다식
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
