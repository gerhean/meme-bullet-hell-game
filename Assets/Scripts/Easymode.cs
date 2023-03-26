using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Easymode : EnemyController
{
    public float shootSpread = 30f;
    public int tackCount = 6;
    public float tackAttackCooldown = 5f;

    public IEnumerator TackShoot()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
            float angle = 200f / tackCount;
            for (int i = 0; i < tackCount; i++)
            {
                ShootAtPlayer(Quaternion.Euler(0, 0, -100 + angle * i));
            }

            yield return new WaitForSeconds(Random.Range(0.5f, tackAttackCooldown));
        }
    }

    public override IEnumerator Shoot()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(TackShoot());
        while (true)
        {
            ShootAtPlayer(Quaternion.Euler(0, 0, Random.Range(-shootSpread, shootSpread)));
            yield return new WaitForSeconds(Random.Range(0.2f, attackCooldown));
        }


    }
}
