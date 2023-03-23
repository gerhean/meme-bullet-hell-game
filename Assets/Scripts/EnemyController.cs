using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject bullet;
    public float firerate = 1f;

    void Start()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            foreach (Quaternion a in GenerateRotatedQuaternions(40))
            {
                SpawnLocalRotation(a);
            }
            yield return new WaitForSeconds(firerate);
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

    void ShootAtPlayer()
    {
        ShootAt(transform.position, PlayerController.playerController.transform.position);
    }


    protected void ShootAt(Vector3 startLoc, Vector3 endLoc, Quaternion offset = default)
    {

        Vector3 myLocation = startLoc;
        Vector3 targetLocation = endLoc;
        targetLocation.z = myLocation.z;
        Vector3 vectorToTarget = targetLocation - myLocation;

        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;
        Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);
        Quaternion rotation = Quaternion.LookRotation(endLoc - startLoc, Vector3.forward);

        GameObject toAdd = Instantiate(bullet, startLoc, rotation);


        toAdd.transform.rotation = Quaternion.RotateTowards(Quaternion.Euler(0, 0, 0), targetRotation, 360f);

        if (offset.z == 0 || offset.eulerAngles == Vector3.zero)
        {
            offset = Quaternion.Euler(0, 0, 0);
        }
        toAdd.transform.rotation = offset * toAdd.transform.rotation;
    }

    protected void SpawnLocalRotation(Quaternion offset = default, Vector3 loc = default)
    {

        if (offset.z == 0 || offset.eulerAngles == Vector3.zero)
        {
            offset = Quaternion.Euler(0, 0, 0);
        }

        if (loc == default)
        {
            loc = transform.position;
        }


        offset *= Quaternion.Euler(0, 0, 90);

        GameObject toAdd = Instantiate(bullet, loc, gameObject.transform.rotation);
        toAdd.transform.rotation = offset * transform.rotation;


    }
}
