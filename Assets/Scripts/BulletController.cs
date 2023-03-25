using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject yellowGlow;
    public float speed = 5f;
    public float bulletLifetime = 3f;

    // 0: error, 3: 3*, 5: 5*, 6: on banner 5*
    public int pullType = 3;
    private void Awake()
    {
        Destroy(gameObject, bulletLifetime);
    }


    public virtual void Update()
    {
        // Get the current rotation of the object
        Quaternion rotation = transform.rotation;

        // Get the direction of the rotation by multiplying the rotation with Vector2.right
        Vector2 direction = rotation * Vector2.right;

        // Move the object in the direction of the rotation by adding the direction multiplied by the speed to the object's position
        transform.position += new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;
    }


}
