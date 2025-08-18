using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageCanvas : MonoBehaviour
{
	public Text DamageText;

	public void Init(int damage, bool isCritical, string TextColor = "Empty")
	{
		DamageText.text = damage.ToString();
		DamageText.fontSize = 30;

		if (TextColor == "Empty")
		{
			if (isCritical)
				DamageText.color = Color.magenta;
			else
				DamageText.color = Color.black;
		}
		else if(TextColor == "Green")
		{
			DamageText.color = Color.green;
		}
		else if(TextColor == "Yellow")
		{
			DamageText.color = Color.yellow;
		}
		else if(TextColor == "Red")
		{
			DamageText.color = Color.red;
		}
		StartCoroutine(DamageTextAction());

	}

	IEnumerator DamageTextAction()
	{
		float curTime = 0;
		float endTime = 0.5f;
		float moveSpeed = 2.0f;

		while(curTime < endTime)
		{
			yield return null;
			curTime += Time.deltaTime;
			transform.position += Vector3.up * moveSpeed * Time.deltaTime;
			DamageText.fontSize += 1;
		}

		Managers.Pool.Push(gameObject);

	}

}
