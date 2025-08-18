using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 백그라운드 스프라이트 스크롤
public class BackgroundScroll : MonoBehaviour
{

	// 총 2개의 레이어로 속도를 다르게 스크롤
	public GameObject[] Layer1;
	public GameObject[] Layer2;

	public float Layer1Speed = 1.5f;
	float Layer2Speed = 5.0f;

	// 스프라이트 스크롤 시작위치와 종료위치
	public float Layer1XEndPos = -5;
	public float Layer1XSpawnPos = 7;

	public float Layer2XEndPos = -5;
	public float Layer2XSpawnPos = 7;


	public bool isScroll = true;

	private void Start()
	{
		// 2번째 레이어의 이동속도는 적오브젝트의 이동속도와 동일하게 설정
		Layer2Speed = Managers.MoveSpeed;
	}

	private void Update()
	{
		if(isScroll)
		{
			ScrollLayer();
		}
	}

	// 백그라운드 스크롤
	void ScrollLayer()
	{
		// 종료지점에 도달한 오브젝트는 출발지점으로 위치값 변경
		for(int i = 0; i < Layer1.Length; i++)
		{
			Layer1[i].transform.position += Vector3.left * Layer1Speed * Time.deltaTime;
			if (Layer1[i].transform.position.x < Layer1XEndPos)
			{
				Layer1[i].transform.position = new Vector3(Layer1XSpawnPos, Layer1[i].transform.position.y, 0);
			}
		}
		for(int i = 0; i < Layer2.Length; i++)
		{
			Layer2[i].transform.position += Vector3.left * Layer2Speed * Time.deltaTime;
			if (Layer2[i].transform.position.x < Layer2XEndPos)
			{
				Layer2[i].transform.position = new Vector3(Layer2XSpawnPos, Layer2[i].transform.position.y, 0);
			}

		}
	}

}
