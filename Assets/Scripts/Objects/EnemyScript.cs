using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Enemy��ũ��Ʈ
public class EnemyScript : MonoBehaviour
{
	// ��׶��� ��������Ʈ ��ũ�ѿ�
	public BackgroundScroll scrolls;

	// �����̴��� üũ
	public bool isMove = false;

	// ���� ü��
	public int MaxpHp = 100;
	public int Hp = 100;

	// �ִϸ��̼� ó����
	Animator anim;

	// ü�¹�
	public Image HpBarBackGround;
	public Image HpBar;

	// �ʱ�ȭ
	public void Init(int spriteNum,BackgroundScroll scroll)
	{
		// ��������Ʈ�� ��������Ʈ ����
		GetComponentInChildren<SpriteRenderer>().sprite = 
			Managers.Resource.Load<Sprite>($"Sprite/Enemy/{spriteNum}");

		scrolls = scroll;
		scrolls.isScroll = true;
		isMove = true;
		anim = GetComponentInChildren<Animator>();
		anim.Play("EnemyIdle");

		GetComponent<BoxCollider2D>().enabled = true;

		// ���� ų ī��Ʈ�� ������� ü�� ����
		int killcount = (int)(Managers.GData.player.KillCount / 3);
		if (killcount > 0)
			killcount *= 10;
		MaxpHp = 100 + killcount;
		Hp = MaxpHp;
		HpBarBackGround.enabled = true;
		HpBar.enabled = true;

	}

	private void Update()
	{
		// ������ Ȱ��ȭ �� 
		if(isMove) 
		{
			// �Ŵ����� �߰��ص� �̵��ӵ����� ������� ����(�÷��̾����)���� �̵�
			transform.position += Vector3.left * Managers.MoveSpeed * Time.deltaTime;
		}

		// ü�¹� ����
		if(HpBar.enabled)
		{
			if(Hp <= 0)
			{
				HpBar.enabled = false;
				HpBarBackGround.enabled = false;
			}
			else
			{
				HpBar.fillAmount = (float)Hp / MaxpHp;
			}
		}

	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		// �÷��̾�� �浹 ��
		if(collision.CompareTag("Player"))
		{
			// �̵�����, ��׶��� ��ũ�� ����
			scrolls.isScroll = false;
			isMove = false;

		}
	}

	// �� ���
	public void EnemyDead()
	{
		// �̵� Ȱ��ȭ, ��׶��� ��ũ�� Ȱ��ȭ
		scrolls.isScroll = true;
		isMove = true;

		// ����ִϸ��̼� ���
		anim.Play("EnemyDie");
		// �߰����� ��ȣ�ۿ��� �������� �ݶ��̴� ��Ȱ��ȭ
		GetComponent<BoxCollider2D>().enabled = false;

		// �÷��̾� ���� ������ �޾ƿͼ� ų��ġ �߰�, ��� �߰�
		GameData.PlayerData player = Managers.GData.player;

		player.KillCount++;

		double RandomVal = Random.Range(player.GetMinGold, 
			player.GetMaxGold+(player.UpgradeGetGoldVal * player.UpgradeGetGold));

		player.Gold += RandomVal;

		// ���� ������ ���̺�
		Managers.GData.SaveData();

		// ��� �ִϸ��̼��� ������ ������Ʈ Ǯ�� �ǷѸ���
		StartCoroutine(EnemyDieAnimation());
	}
	IEnumerator EnemyDieAnimation()
	{
		yield return null;
		while(true)
		{
			yield return null;
			if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
			{
				break;
			}
		}
		Managers.Pool.Push(this.gameObject);

	}

	// ������ ó��
	public void GetDamage(int damage, bool iscri, string TextColor = "Empty")
	{
		Hp -= damage;

		// ������ ��Ʈ ���
		GameObject DamageFont = Managers.Pool.Pop(Managers.Resource.Load<GameObject>("Prefab/DamageCanvas"));
		DamageFont.transform.position = transform.position;
		DamageFont.GetComponent<DamageCanvas>().Init(damage, iscri, TextColor);
			

		// �������� ���ö� �̹� Hit�ִϸ��̼� ������̶�� �ٽ� ���
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyHit"))
		{
			anim.Play("EnemyHit",-1,0f);

		}
		else
			anim.Play("EnemyHit");

		if (Hp <= 0)
		{
			Hp = 0;
		}
	}
}
