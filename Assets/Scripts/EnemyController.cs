using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject bullet;
    public float attackCooldown = 1f;

    private int pityCount = 0;
    public int maxPity = 80;
    private bool isFiftyFifty = true;

    void Start()
    {
        StartCoroutine(Shoot());
    }

    public virtual IEnumerator Shoot()
    {
        while (true)
        {
            ShootAtPlayer();
            yield return new WaitForSeconds(attackCooldown);
        }
    }

    //Returns a list of n quarts each rotated 360/n eulerangles of each other
    public List<Quaternion> GenerateRotatedQuaternions(int numQuarternions)
    {
        List<Quaternion> result = new();
        float angle = 360.0f / numQuarternions;
        for (int i = 0; i < numQuarternions; i++)
        {
            Quaternion rot = Quaternion.identity;
            rot.eulerAngles = new Vector3(0, 0, angle * i);
            result.Add(rot);

        }
        return result;
    }

    protected GameObject ShootAtPlayer(Quaternion offset = default, GameObject toSpawn = default)
    {
        return ShootAt(transform.position, PlayerController.playerController.transform.position, offset, toSpawn);
    }

    protected void setPull(BulletController bullet)
    {
        if (pityCount < maxPity) {
            pityCount++;
            bullet.pullType = 3;
        }
        else {
            pityCount = 0;
            bullet.yellowGlow.SetActive(true);
            if (isFiftyFifty) {
                isFiftyFifty = false;
                if (Random.Range(0, 3) == 0) {
                    bullet.pullType = 6;
                }
                else {
                    bullet.pullType = 5;
                }
            }
            else {
                isFiftyFifty = true;
                bullet.pullType = 6;
            }
        }
    }


    protected GameObject ShootAt(Vector3 startLoc, Vector3 endLoc, Quaternion offset = default, GameObject toSpawn = default)
    {

        if (toSpawn == default)
        {
            toSpawn = bullet;
        }

        Vector3 myLocation = startLoc;
        Vector3 targetLocation = endLoc;
        targetLocation.z = myLocation.z;
        Vector3 vectorToTarget = targetLocation - myLocation;

        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;
        Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);
        Quaternion rotation = Quaternion.LookRotation(endLoc - startLoc, Vector3.forward);

        GameObject toAdd = Instantiate(toSpawn, startLoc, rotation);
        setPull(toAdd.GetComponent<BulletController>());

        toAdd.transform.rotation = Quaternion.RotateTowards(Quaternion.Euler(0, 0, 0), targetRotation, 360f);

        if (offset.z == 0 || offset.eulerAngles == Vector3.zero)
        {
            offset = Quaternion.Euler(0, 0, 0);
        }
        toAdd.transform.rotation = offset * toAdd.transform.rotation;
        return toAdd;
    }

    protected GameObject SpawnLocalRotation(Quaternion offset = default, Vector3 loc = default, GameObject toSpawn = default)
    {
        if (toSpawn == default)
        {
            toSpawn = bullet;
        }
        if (offset.z == 0 || offset.eulerAngles == Vector3.zero)
        {
            offset = Quaternion.Euler(0, 0, 0);
        }

        if (loc == default)
        {
            loc = transform.position;
        }


        offset *= Quaternion.Euler(0, 0, 90);

        GameObject toAdd = Instantiate(toSpawn, loc, gameObject.transform.rotation);
        toAdd.transform.rotation = offset * transform.rotation;
        return toAdd;

    }
}
