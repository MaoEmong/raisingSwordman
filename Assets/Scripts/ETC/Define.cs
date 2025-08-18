using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

// 기타 등등 enum값
public class Define
{
	// 사운드 종류
	public enum Sound
	{
		Bgm,
		Effect,
		MaxCount
	}
	// 씬 종류
	public enum SceneType
	{
		Unknown,
		MenuScene,
		LoadScene,
		MainScene,
	}


	

}

// 확장메서드
public static class ExtensionMethod
{
	// Enum값을 string으로 변환
	public static T ToEnum<T>(this string value)
	{
		if(!System.Enum.IsDefined(typeof(T), value))
			return default(T);

		return (T)System.Enum.Parse(typeof(T), value, true);
	}

}

