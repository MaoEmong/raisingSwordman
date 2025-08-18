using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾� ��Ʈ�ѷ�
public class PlayerController : MonoBehaviour
{
	// �ִϸ��̼� �ӵ� ������
	Animator anim;
	// �ε��� ��
	public EnemyScript Enemy = null;
	// ���������� üũ
	public bool isAttack = false;
	// ��ų ������ġ
	public Transform FireballStartPos;
	public Transform[] MeteorPos;
	// ���ӿ��� üũ
	bool isGameOver = false;

	private void Start()
	{
		// �Ŵ����� �÷��̾� ������Ʈ�� �ڱ��ڽ� �ֱ�
		Managers.Player = gameObject;

		anim = transform.GetComponent<Animator>();
		// 0.3�� �ĺ��� �÷��̾� �ȴ� �Ҹ� ���
		// ���� �� ���� �� ȭ�� ��ȯ�� 0.3�� �ɸ��� ����
		Managers.CallWaitForSeconds(0.3f, () => { StartCoroutine(WalkPlayer()); });
		
	}
	// �߰��� �Ҹ� ��� �ڷ�ƾ
	IEnumerator WalkPlayer()
	{
		float curTime = 0.0f;
		float endTime = 0.3f;

		// ������ �ð����� ���� �ȴ������� üũ ��
		// �ȴ����̶�� �߰����Ҹ� ���
		while(true)
		{
			yield return null;
			curTime += Time.deltaTime;

			if(curTime > endTime) 
			{
				curTime = 0.0f;

				if(!isAttack ) 
				{
					Managers.Sound.Play("Effect/Object/Step");
				}
			}

		}
	}


	private void Update()
	{
		// ���ӿ��� �� return
		if (isGameOver)
		{
			return;
		}
		else
		{
			PlayerAnimation();
		}
	}

	void PlayerAnimation()
	{
		// ���� �÷��̾� �����͸� �޾ƿ���
		GameData.PlayerData player = Managers.GData.player;
		// �������̶��
		if (isAttack)
		{
			// ���� �ִϸ��̼� ���
			anim.SetBool("Attack", true);			
			float speed = player.DefaultAttackSpeed +
				(player.UpgradeAttackSpeedVal * player.UpgradeAttackSpeed);
			// ���� �ִϸ��̼��� �ӵ��� �⺻ ���ݼӵ�+ ���׷��̵��� ��ġ
			anim.speed = speed;
		}
		else
		{
			// ���� �ִϸ��̼� ��� 
			anim.SetBool("Attack", false);
			// �ִϸ��̼� ����ӵ� �ʱ�ȭ
			anim.speed = 1.0f;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// �ε��� ������Ʈ�� ���̶�� ������ �ޱ�
		if(collision.CompareTag("Enemy"))
		{
			Enemy = collision.GetComponent<EnemyScript>();
			isAttack = true;
		}
	}

	// ���ݾִϸ��̼ǿ� �̺�Ʈ�� �߰�
	// �ִϸ��̼� ��������Ʈ���� �ݹ�
	public void AttackSound()
	{
		Managers.Sound.Play("Effect/Object/Attack");
	}
	// ���ݾִϸ��̼ǿ� �̺�Ʈ�� �߰�
	// ������ ������ ������ ó��
	public void Attack()
	{
		GameData.PlayerData Player = Managers.GData.player;
		int Damage = Player.DefaultAttack + (Player.UpgradeAttackVal * Player.UpgradeAttack);
		bool iscri = false;

		if(Random.Range(0.0f,100.0f) < Player.UpgradeCriticalVal * Player.UpgradeCritical)
		{
			Damage = (int)(Damage * 1.5f);
			iscri = true;
		}
		Enemy.GetDamage(Damage,iscri);
		GameObject HitAnim = Managers.Pool.Pop(Managers.Resource.Load<GameObject>("Prefab/HitAnimation"));
		Managers.Sound.Play("Effect/Object/Hit");
		HitAnim.transform.position = Enemy.transform.position;
		if (Enemy.Hp <= 0)
		{
			Enemy.EnemyDead();
			isAttack = false;
		}
	}

	// ���ӿ���ó��
	public void GameOver()
	{
		isGameOver = true;
		anim.speed = 0.0f;
	}

	// �÷��̾� ��ų �ݹ�
	public void PlayerSkill(int skillcode)
	{
		// ��ų�ڵ忡 ���� �ݹ��Լ��� �ٸ���
		switch(skillcode)
		{
			case 0:
				FireBall();
				break;
			case 1:
				AcidExplosion();
				break;
			case 2:
				Meteor();
				break;
			default:

				break;
		}
	}

	// ���̾
	void FireBall()
	{
		GameObject FireBall = Managers.Pool.Pop(Managers.Resource.Load<GameObject>("Prefab/FireBall"));
		FireBall.transform.position = FireballStartPos.position;

		FireBall.GetComponent<FireBall>().Init();

		Managers.Sound.Play("Effect/Object/Fireball");
	}

	// �͵�����
	void AcidExplosion()
	{
		GameObject Acid = Managers.Pool.Pop(Managers.Resource.Load<GameObject>("Prefab/AcidExplosion"));
		Acid.transform.position = transform.position;
		Acid.GetComponent<AcidExplosion>().Init();
		Managers.Sound.Play("Effect/Object/Poison");
	}

	// ���׿�
	void Meteor()
	{
		for(int i = 0; i < 6; i++)
		{
			float val = Random.Range(0.1f, 0.3f);
			Managers.CallWaitForSeconds(val, () => {
				GameObject MeteorObj = Managers.Pool.Pop(Managers.Resource.Load<GameObject>("Prefab/Meteor"));
				int iVal = Random.Range(0, MeteorPos.Length);
				MeteorObj.transform.position = MeteorPos[iVal].position;
				MeteorObj.GetComponent<Meteor>().Init();
			Managers.Sound.Play("Effect/Object/Fireball");
			});

		}
	}
}
