using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class supcal : EnemyController
{
    public override IEnumerator Shoot()
    {

        while (true)
        {

            foreach (Quaternion quart in GenerateRotatedQuaternions(40))
                SpawnLocalRotation(quart);

            yield return new WaitForSeconds(attackCooldown);
        }
    }
}
