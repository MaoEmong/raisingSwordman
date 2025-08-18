using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Enemy스크립트
public class EnemyScript : MonoBehaviour
{
	// 백그라운드 스프라이트 스크롤용
	public BackgroundScroll scrolls;

	// 움직이는지 체크
	public bool isMove = false;

	// 적의 체력
	public int MaxpHp = 100;
	public int Hp = 100;

	// 애니메이션 처리용
	Animator anim;

	// 체력바
	public Image HpBarBackGround;
	public Image HpBar;

	// 초기화
	public void Init(int spriteNum,BackgroundScroll scroll)
	{
		// 적오브젝트의 스프라이트 설정
		GetComponentInChildren<SpriteRenderer>().sprite = 
			Managers.Resource.Load<Sprite>($"Sprite/Enemy/{spriteNum}");

		scrolls = scroll;
		scrolls.isScroll = true;
		isMove = true;
		anim = GetComponentInChildren<Animator>();
		anim.Play("EnemyIdle");

		GetComponent<BoxCollider2D>().enabled = true;

		// 현재 킬 카운트를 기반으로 체력 설정
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
		// 움직임 활성화 시 
		if(isMove) 
		{
			// 매니저에 추가해둔 이동속도값을 기반으로 왼쪽(플레이어방향)으로 이동
			transform.position += Vector3.left * Managers.MoveSpeed * Time.deltaTime;
		}

		// 체력바 설정
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
		// 플레이어와 충돌 시
		if(collision.CompareTag("Player"))
		{
			// 이동정지, 백그라운드 스크롤 정지
			scrolls.isScroll = false;
			isMove = false;

		}
	}

	// 적 사망
	public void EnemyDead()
	{
		// 이동 활성화, 백그라운드 스크롤 활성화
		scrolls.isScroll = true;
		isMove = true;

		// 사망애니메이션 재생
		anim.Play("EnemyDie");
		// 추가적인 상호작용을 막기위해 콜라이더 비활성화
		GetComponent<BoxCollider2D>().enabled = false;

		// 플레이어 현재 데이터 받아와서 킬수치 추가, 골드 추가
		GameData.PlayerData player = Managers.GData.player;

		player.KillCount++;

		double RandomVal = Random.Range(player.GetMinGold, 
			player.GetMaxGold+(player.UpgradeGetGoldVal * player.UpgradeGetGold));

		player.Gold += RandomVal;

		// 이후 데이터 세이브
		Managers.GData.SaveData();

		// 사망 애니메이션이 끝나면 오브젝트 풀로 되롤리기
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

	// 데미지 처리
	public void GetDamage(int damage, bool iscri, string TextColor = "Empty")
	{
		Hp -= damage;

		// 데미지 폰트 출력
		GameObject DamageFont = Managers.Pool.Pop(Managers.Resource.Load<GameObject>("Prefab/DamageCanvas"));
		DamageFont.transform.position = transform.position;
		DamageFont.GetComponent<DamageCanvas>().Init(damage, iscri, TextColor);
			

		// 데미지가 들어올때 이미 Hit애니메이션 재생중이라면 다시 재생
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
