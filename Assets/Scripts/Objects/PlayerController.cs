using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 컨트롤러
public class PlayerController : MonoBehaviour
{
	// 애니메이션 속도 관리용
	Animator anim;
	// 부딪힌 적
	public EnemyScript Enemy = null;
	// 공격중인지 체크
	public bool isAttack = false;
	// 스킬 시작위치
	public Transform FireballStartPos;
	public Transform[] MeteorPos;
	// 게임오버 체크
	bool isGameOver = false;

	private void Start()
	{
		// 매니저의 플레이어 오브젝트에 자기자신 넣기
		Managers.Player = gameObject;

		anim = transform.GetComponent<Animator>();
		// 0.3초 후부터 플레이어 걷는 소리 재생
		// 메인 씬 실행 시 화면 전환에 0.3초 걸리기 때문
		Managers.CallWaitForSeconds(0.3f, () => { StartCoroutine(WalkPlayer()); });
		
	}
	// 발걸음 소리 출력 코루틴
	IEnumerator WalkPlayer()
	{
		float curTime = 0.0f;
		float endTime = 0.3f;

		// 설정한 시간마다 현재 걷는중인지 체크 후
		// 걷는중이라면 발걸음소리 출력
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
		// 게임오버 시 return
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
		// 현재 플레이어 데이터를 받아오기
		GameData.PlayerData player = Managers.GData.player;
		// 공격중이라면
		if (isAttack)
		{
			// 공격 애니메이션 재생
			anim.SetBool("Attack", true);			
			float speed = player.DefaultAttackSpeed +
				(player.UpgradeAttackSpeedVal * player.UpgradeAttackSpeed);
			// 공격 애니메이션의 속도는 기본 공격속도+ 업그레이드한 수치
			anim.speed = speed;
		}
		else
		{
			// 공격 애니메이션 취소 
			anim.SetBool("Attack", false);
			// 애니메이션 재생속도 초기화
			anim.speed = 1.0f;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// 부딪힌 오브젝트가 적이라면 데이터 받기
		if(collision.CompareTag("Enemy"))
		{
			Enemy = collision.GetComponent<EnemyScript>();
			isAttack = true;
		}
	}

	// 공격애니메이션에 이벤트로 추가
	// 애니메이션 실행포인트에서 콜백
	public void AttackSound()
	{
		Managers.Sound.Play("Effect/Object/Attack");
	}
	// 공격애니메이션에 이벤트로 추가
	// 공격이 적에게 닿을때 처리
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

	// 게임오버처리
	public void GameOver()
	{
		isGameOver = true;
		anim.speed = 0.0f;
	}

	// 플레이어 스킬 콜백
	public void PlayerSkill(int skillcode)
	{
		// 스킬코드에 따라 콜백함수를 다르게
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

	// 파이어볼
	void FireBall()
	{
		GameObject FireBall = Managers.Pool.Pop(Managers.Resource.Load<GameObject>("Prefab/FireBall"));
		FireBall.transform.position = FireballStartPos.position;

		FireBall.GetComponent<FireBall>().Init();

		Managers.Sound.Play("Effect/Object/Fireball");
	}

	// 맹독공격
	void AcidExplosion()
	{
		GameObject Acid = Managers.Pool.Pop(Managers.Resource.Load<GameObject>("Prefab/AcidExplosion"));
		Acid.transform.position = transform.position;
		Acid.GetComponent<AcidExplosion>().Init();
		Managers.Sound.Play("Effect/Object/Poison");
	}

	// 메테오
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
