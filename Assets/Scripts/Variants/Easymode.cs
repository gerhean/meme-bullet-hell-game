using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Easymode : EnemyController
{
    public override IEnumerator Shoot()
    {
        float spread = 30f;
        while (true)
        {
            ShootAtPlayer(Quaternion.Euler(0, 0, Random.Range(-spread, spread)));
            yield return new WaitForSeconds(Random.Range(0.01f, attackCooldown));
        }


    }
}
