using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tackandtarget : EnemyController
{
    public override IEnumerator Shoot()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
            Quaternion rot = Quaternion.identity;
            Vector3 startLoc = gameObject.transform.position;
            Vector3 endLoc = PlayerController.playerController.transform.position;

            for (int i = 1; i < 5; i++)
            {
                rot.eulerAngles = new Vector3(0, 0, i * 10);
                ShootAt(startLoc, endLoc, rot);
                rot.eulerAngles = new Vector3(0, 0, 0);
                ShootAt(startLoc, endLoc, rot);
                rot.eulerAngles = new Vector3(0, 0, -i * 10);
                ShootAt(startLoc, endLoc, rot);

                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(0.3f);

            foreach (Quaternion quart in GenerateRotatedQuaternions(40))
                SpawnLocalRotation(quart);


            yield return new WaitForSeconds(attackCooldown);
        }
    }
}
