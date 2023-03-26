using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safespot : EnemyController
{
    public GameObject rotateBullet;

    public int arenaCount = 5;
    public override IEnumerator Shoot()
    {
        StartCoroutine(spawnArena());
        float spread = 30f;
        while (true)
        {
            for (int i = 0; i < 5; i++)
            {
                ShootAtPlayer(Quaternion.Euler(0, 0, Random.Range(-spread, spread)));
            }
            yield return new WaitForSeconds(attackCooldown);
        }


    }

    public IEnumerator spawnArena()
    {
        while (arenaCount > 0)
        {

            for (int i = 0; i < arenaCount; i++)
            {
                Quaternion areaRotationOffset = Quaternion.Euler(0, 0, 360 * i / arenaCount);
                float rot = 30f;
                float rotateAfterSeconds = .8f;
                Quaternion offset = Quaternion.Euler(0, 0, rot);
                GameObject result = ShootAtPlayer(offset * areaRotationOffset, rotateBullet);
                result.GetComponent<RotateAfterSeconds>().startTimer(rotateAfterSeconds, Quaternion.Euler(0, 0, -rot * 1.5f));

                rot = -30f;
                offset = Quaternion.Euler(0, 0, rot);
                result = ShootAtPlayer(offset * areaRotationOffset, rotateBullet);
                result.GetComponent<RotateAfterSeconds>().startTimer(rotateAfterSeconds, Quaternion.Euler(0, 0, -rot * 1.5f));

            }
            yield return new WaitForSeconds(.05f);


        }
    }
}
