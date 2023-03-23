using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAfterSeconds : BulletController
{

    private float timeToWait;
    private Quaternion rotation;

    public void startTimer(float timeToWait, Quaternion rotation)
    {
        this.timeToWait = timeToWait;
        this.rotation = rotation;
        StartCoroutine(RotateCoroutine());
    }

    public IEnumerator RotateCoroutine()
    {
        yield return new WaitForSeconds(timeToWait);
        gameObject.transform.rotation *= rotation;
        yield return null;
    }

}
