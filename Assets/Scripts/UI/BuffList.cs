using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffList : MonoBehaviour
{


	public void AddBuff(string buff, float timer = 0.0f)
	{
		GameObject Buff = new GameObject();
		Buff.AddComponent<Image>().type = Image.Type.Filled;
		Buff.transform.parent = transform;
		Buff.transform.localScale = Vector3.one;
		Buff.GetComponent<RectTransform>().sizeDelta = new Vector2(60, 60);
		Buff.name = "BUFF";

		float endTime = 0.0f;

		switch(buff)
		{
			case "AttackPotion":
				endTime = timer;
				// 이미지 변경
				Buff.GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>($"Sprite/BuffIcon/0");
				// 플레이어 공격력 증가
				Managers.GData.player.isAttackBuff = true;
				Managers.GData.player.DefaultAttack += 20;
				break;

			default:
				Debug.Log("Error Buff isFailed");
				break;

		}

		StartCoroutine(BuffImageAction(Buff.GetComponent<Image>(), endTime,buff));
	}

	IEnumerator BuffImageAction(Image image,float endTime, string buffName)
	{
		float curTime = 0.0f;

		while(curTime < endTime)
		{
			yield return null;

			curTime += Time.deltaTime;

			image.fillAmount = 1 - curTime / endTime;
			switch (buffName)
			{
				case "AttackPotion":
					if (!Managers.GData.player.isAttackBuff)
					{
						Managers.GData.player.DefaultAttack += 20;
						Managers.GData.player.isAttackBuff = true;
					}

					break;

				default:
					Debug.Log("Error BuffName is Not!");
					break;
			}
		}

		switch(buffName)
		{
			case "AttackPotion":
				if (Managers.GData.player.isAttackBuff)
				{
					Managers.GData.player.DefaultAttack -= 20;
					Managers.GData.player.isAttackBuff = false;
				}

				break;

			default:
				Debug.Log("Error BuffName is Not!");
				break;
		}

		Destroy(image.gameObject);

	}

}
