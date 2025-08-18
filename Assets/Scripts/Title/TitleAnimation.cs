using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAnimation : MonoBehaviour
{
	public Transform EnemyPos;

	public void TitleParticleAction()
	{
		GameObject particle = Managers.Pool.Pop(Managers.Resource.Load<GameObject>("Prefab/HitAnimation"));
		particle.transform.position = EnemyPos.position;
		Managers.Sound.Play("Effect/Object/Hit");
	}

}
