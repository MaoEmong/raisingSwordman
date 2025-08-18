using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �߰� ������ �Լ���
public class MyTools
{
	// �ð� ����
	public static void TimePause()
	{
		Time.timeScale = 0;
	}
	// �ð����� ����
	public static void TimePlay()
	{
		Time.timeScale = 1;
	}
	// C++�� Pair ����
	public class Pair<T, U>
	{
		public Pair() { }
		public Pair(T first, U second)
		{
			this.First = first;
			this.Second = second;
		}

		public T First { get; set; }
		public U Second { get; set; }
	}

	// �̹��� ������
	public static IEnumerator ImageMove(Image img, Vector3 targetpos, float time)
	{
		Vector3 moveVec = targetpos - img.transform.position;
		moveVec = moveVec.normalized;
		float distance = Vector3.Distance(img.transform.position, targetpos);
		float speed = distance / time;
		float curtime = 0;


		while(curtime < time)
		{
			yield return null;
			img.transform.position += moveVec * speed * Time.deltaTime;
			curtime += Time.deltaTime;
		}

	}
	/// <summary>
	/// �̹��� ������ ��Ÿ��
	/// </summary>
	/// <param name="img">������ �̹���</param>
	/// <param name="time">����Ǵ� �ð�</param>
	/// <returns></returns>
	public static IEnumerator ImageFadeIn(Image img, float time)
	{
		Color curColor = new Color(img.color.r, img.color.g, img.color.b, 0);
		img.color = new Color(img.color.r, img.color.g, img.color.b, 0);
		float speed = 1/time;
		float curtime = 0;

		while(curtime < time)
		{
			yield return null;
			curColor += new Color(0, 0, 0, speed * Time.deltaTime) ;
			img.color = curColor;
			curtime += Time.deltaTime;

		}
		img.color = new Color(img.color.r, img.color.g, img.color.b, 255);
	}
	/// <summary>
	/// ������ �����
	/// </summary>
	/// <param name="img">������ �̹���</param>
	/// <param name="time">����Ǵ� �ð�</param>
	/// <returns></returns>
	public static IEnumerator ImageFadeOut(Image img, float time)
	{
		Color curColor = new Color(img.color.r, img.color.g, img.color.b, 1);
		img.color = new Color(img.color.r, img.color.g, img.color.b, 1);
		float speed = 1 / time;
		float curtime = 0;

		while (curtime < time)
		{
			yield return null;
			curColor -= new Color(0, 0, 0, speed * Time.deltaTime);
			img.color = curColor;

			curtime += Time.deltaTime;
		}
		img.color = new Color(img.color.r, img.color.g, img.color.b, 0);
	}
	// ��Ÿ��
	public static IEnumerator TextFadeIn(Text _text, float time)
	{
		Color curColor = new Color(_text.color.r, _text.color.g, _text.color.b, 0);
		_text.color = new Color(_text.color.r, _text.color.g, _text.color.b, 0);
		float speed = 1 / time;
		float curtime = 0;

		while (curtime < time)
		{
			yield return null;
			curColor += new Color(0, 0, 0, speed * Time.deltaTime);
			_text.color = curColor;
			curtime += Time.deltaTime;

		}
		_text.color = new Color(_text.color.r, _text.color.g, _text.color.b, 255);

	}
	// �����
	public static IEnumerator TextFadeOut(Text _text, float time) 
	{
		Color curColor = new Color(_text.color.r, _text.color.g, _text.color.b, 1);
		_text.color = new Color(_text.color.r, _text.color.g, _text.color.b, 1);
		float speed = 1 / time;
		float curtime = 0;

		while (curtime < time)
		{
			yield return null;
			curColor -= new Color(0, 0, 0, speed * Time.deltaTime);
			_text.color = curColor;
			curtime += Time.deltaTime;

		}
		_text.color = new Color(_text.color.r, _text.color.g, _text.color.b, 0);

	}


}
