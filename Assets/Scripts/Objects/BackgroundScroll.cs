using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��׶��� ��������Ʈ ��ũ��
public class BackgroundScroll : MonoBehaviour
{

	// �� 2���� ���̾�� �ӵ��� �ٸ��� ��ũ��
	public GameObject[] Layer1;
	public GameObject[] Layer2;

	public float Layer1Speed = 1.5f;
	float Layer2Speed = 5.0f;

	// ��������Ʈ ��ũ�� ������ġ�� ������ġ
	public float Layer1XEndPos = -5;
	public float Layer1XSpawnPos = 7;

	public float Layer2XEndPos = -5;
	public float Layer2XSpawnPos = 7;


	public bool isScroll = true;

	private void Start()
	{
		// 2��° ���̾��� �̵��ӵ��� ��������Ʈ�� �̵��ӵ��� �����ϰ� ����
		Layer2Speed = Managers.MoveSpeed;
	}

	private void Update()
	{
		if(isScroll)
		{
			ScrollLayer();
		}
	}

	// ��׶��� ��ũ��
	void ScrollLayer()
	{
		// ���������� ������ ������Ʈ�� ����������� ��ġ�� ����
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
