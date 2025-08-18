using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    public enum ShopType
    {
        AttackPotion,

    }

	public ShopType type;

    public Text GoldText;
    public double Gold;
    public Image TimerImage;
    public float curTime = 0.0f;
    public float endTime;

	public BuffList buff;

    void Start()
    {
        GoldText.text = $"{Gold.ToMoneyString()}G";
        TimerImage.enabled = false;
		curTime = endTime;
		buff = transform.GetComponentInParent<ShopPanel>().buffList;
    }

    void Update()
    {
		if (curTime >= endTime)
		{
			curTime = endTime;
			if (TimerImage.enabled)
			{
				TimerImage.enabled = false;
				//TimerText.text = "";
			}
		}
		else
		{
			curTime += Time.deltaTime;
			if (!TimerImage.enabled)
			{
				TimerImage.enabled = true;

			}

			TimerImage.fillAmount = 1.0f - (curTime / endTime);
			//float time = endTime - curTime;
			//TimerText.text = $"{time.ToString("F1")}";
		}


	}

	public void ShopSlotButton()
    {
		if(Managers.GData.player.Gold < Gold)
		{
			Managers.Sound.Play("Effect/UI/Failed");
			return;
		}
		else
		{
			if(curTime >= endTime)
			{
				curTime = 0.0f;
				TimerImage.enabled = true;
				Managers.Sound.Play("Effect/UI/BuyButton");
				Managers.GData.player.Gold -= Gold;
				switch (type)
				{
					case ShopType.AttackPotion:
						buff.AddBuff("AttackPotion", endTime);
						break;
					default:
						Debug.Log("Error Type is NUll");

						break;




				}

			}


		}

    }

}
