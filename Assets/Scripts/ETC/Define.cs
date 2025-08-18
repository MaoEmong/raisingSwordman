using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

// ��Ÿ ��� enum��
public class Define
{
	// ���� ����
	public enum Sound
	{
		Bgm,
		Effect,
		MaxCount
	}
	// �� ����
	public enum SceneType
	{
		Unknown,
		MenuScene,
		LoadScene,
		MainScene,
	}


	

}

// Ȯ��޼���
public static class ExtensionMethod
{
	// Enum���� string���� ��ȯ
	public static T ToEnum<T>(this string value)
	{
		if(!System.Enum.IsDefined(typeof(T), value))
			return default(T);

		return (T)System.Enum.Parse(typeof(T), value, true);
	}

}

