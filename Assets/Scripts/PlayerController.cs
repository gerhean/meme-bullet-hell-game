using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D body;
    public TMPro.TextMeshProUGUI textbox;
    private float horizontal;
    private float vertical;

    public static PlayerController playerController;
    public Hpbar hpbar;

    public float runSpeed = 15.0f;

    public float maxHP = 10f;
    public float currentHP = 10f;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        playerController = this;
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
            takeDamage(1f);
        }
    }

    void takeDamage(float hp)
    {
        currentHP -= hp;
        hpbar.Resize(currentHP / maxHP);
        print(currentHP);
        //Add something when player is dead
        if (currentHP <= 0)
            textbox.gameObject.SetActive(false);
    }


}