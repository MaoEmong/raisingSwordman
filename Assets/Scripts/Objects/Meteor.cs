using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
	Animator anim;

	Coroutine moveCor;

	public void Init(float speed = 3.0f)
    {
		anim = GetComponent<Animator>();

		anim.Play("MeteorStart");
		moveCor = StartCoroutine(MeteorAcrtion(speed));

	}

	IEnumerator MeteorAcrtion(float speed)
	{
		while(true)
		{
			yield return null;	

			if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime>= 0.95f)
			{
				anim.Play("MeteorIdle");
				break;
			}

		}
		Vector3 moveVec = new Vector3(1, -1);
		float curTime = 0.0f;
		float endTime = 3.0f;
		while (curTime < endTime)
		{
			yield return null;
			curTime += Time.deltaTime;

			transform.position += moveVec * speed * Time.deltaTime;


		}

		StartCoroutine(MeteorActionEnd());
	}

	IEnumerator MeteorActionEnd()
	{
		anim.Play("MeteorEnd");
		while (true)
		{
			yield return null;
			if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
			{
				break;
			}
		}

		Managers.Pool.Push(gameObject);


	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Enemy"))
		{
			//StopCoroutine(moveCor);

			StartCoroutine(MeteorActionEnd());

			GameObject hit = Managers.Pool.Pop(Managers.Resource.Load<GameObject>("Prefab/MeteorHit"));
			hit.transform.position = collision.gameObject.transform.position;

			GameData.PlayerData Player = Managers.GData.player;
			int Damage = Player.DefaultAttack + (Player.UpgradeAttackVal * Player.UpgradeAttack);
			Damage = (int)(Damage * 3.0f);

			collision.GetComponent<EnemyScript>().GetDamage(Damage, false,"Red");


		}
	}

}
