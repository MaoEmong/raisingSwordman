using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
	Animator anim;

	Coroutine moveCor;

	public void Init(float speed = 3.0f)
	{
		anim = GetComponent<Animator>();

		anim.Play("FireballStart");
		moveCor = StartCoroutine(FireballMove(speed));
		
	}
	IEnumerator FireballMove(float speed)
	{
		while(true)
		{
			yield return null;
			if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
			{
				anim.Play("FireballActive");
				break;
			}
		}

		float curTime = 0.0f;
		float endTime = 3.0f;

		while(curTime< endTime)
		{
			yield return null;
			curTime += Time.deltaTime;

			transform.position += Vector3.right * speed * Time.deltaTime;
		}

		StartCoroutine(FireballEndAction());

	}

	IEnumerator FireballEndAction()
	{
		anim.Play("FireballEnd");

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
			StopCoroutine(moveCor);

			StartCoroutine(FireballEndAction());
			GameObject hit = Managers.Pool.Pop(Managers.Resource.Load<GameObject>("Prefab/FireballHit"));
			hit.transform.position = collision.gameObject.transform.position;

			GameData.PlayerData Player = Managers.GData.player;
			int Damage = Player.DefaultAttack + (Player.UpgradeAttackVal * Player.UpgradeAttack);
			Damage = (int)(Damage * 2.5f);

			collision.GetComponent<EnemyScript>().GetDamage(Damage, false,"Red");

		}
	}


}
