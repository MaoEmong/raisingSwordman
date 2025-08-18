using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidExplosion : MonoBehaviour
{

    EnemyScript Enemy;

    public void Init()
    {
        Enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyScript>();
        
        if(Enemy == null)
        {
            Debug.Log("Enemy is NULL!");
        }

        transform.parent = Enemy.transform;
        transform.localPosition = Vector3.zero;

        StartCoroutine(ContinuousDamage());
    }

	IEnumerator ContinuousDamage()
    {
        float curTime = 0.0f;
        float endTime = 0.5f;
        int curCount = 0;
        int endCount = 10;

        while(curCount<endCount)
        {
            yield return null;

            if(Enemy.Hp <= 0 )
            {
                break;
            }

            curTime += Time.deltaTime;

            if(curTime>endTime)
            {
                curTime = 0.0f;
                curCount++;

				GameData.PlayerData Player = Managers.GData.player;
				int Damage = Player.DefaultAttack + (Player.UpgradeAttackVal * Player.UpgradeAttack);
				Damage = (int)(Damage * 0.5f);

				Enemy.GetDamage(Damage, false,"Green");

			}

        }

		Managers.Pool.Push(gameObject);

	}

}
