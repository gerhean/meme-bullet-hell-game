using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tackshooter : EnemyController
{

    public override IEnumerator Shoot()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
            foreach (Quaternion quart in GenerateRotatedQuaternions(40))
                SpawnLocalRotation(quart);

            yield return new WaitForSeconds(attackCooldown);
        }
    }
}
