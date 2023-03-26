using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class supcal : EnemyController
{

    public GameObject slowBullet;

    public override IEnumerator Shoot()
    {

        StartCoroutine(BackgroundShots());
        while (true)
        {

            foreach (Quaternion quart in GenerateRotatedQuaternions(40))
            {
                SpawnLocalRotation(quart, default, slowBullet);
                yield return new WaitForSeconds(0.02f);
            }


            yield return new WaitForSeconds(attackCooldown);
        }
    }

    public IEnumerator BackgroundShots()
    {
        while (true)
        {
            Vector3 myPos = gameObject.transform.position;
            Vector3 playerPos = PlayerController.playerController.transform.position;

            Vector3 startPos = playerPos;
            startPos.x = -10;

            ShootAt(startPos, playerPos);

            startPos.x = 10;
            ShootAt(startPos, playerPos);
            yield return new WaitForSeconds(attackCooldown);
        }

    }
}
