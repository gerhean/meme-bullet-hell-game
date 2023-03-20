using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;

    public float runSpeed = 15.0f;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal, vertical) * runSpeed * Time.fixedDeltaTime;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        BulletController bullet = col.gameObject.GetComponentInParent<BulletController>();
        if (bullet != null)
        {

        }
    }


}