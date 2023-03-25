using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D body;
    private float horizontal;
    private float vertical;

    public static PlayerController playerController;
    public TMPro.TextMeshProUGUI primoCountText;
    public GameObject splashBox;

    public float runSpeed = 15.0f;

    public int maxPrimos = 8000;
    private int currentPrimos = 1;

    public float damageCooldownTime = 1.0f;
    private bool isDamageCooldown = false;

    public GameObject[] threeStars;
    public GameObject[] fiveStars;
    public GameObject bannerFiveStar;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        playerController = this;
        currentPrimos = maxPrimos;
        primoCountText.text = currentPrimos.ToString();
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
            takeDamage(bullet.pullType);
        }
    }

    IEnumerator DamageCooldown()
    {
        yield return new WaitForSeconds(damageCooldownTime);
        isDamageCooldown = false;
        
    }

    void takeDamage(int pullType)
    {
        if (isDamageCooldown) {
            return;
        }
        isDamageCooldown = true;
        StartCoroutine(DamageCooldown());

        if (pullType == 3) {
            Debug.Log(pullType);
            GameObject prefab = threeStars[Random.Range(0, threeStars.Length)];
            GameObject splash = Instantiate(prefab, splashBox.transform);
            splash.transform.SetParent(splashBox.transform);
            Destroy(splash, 0.5f);
        }
        else if (pullType == 5) {
            GameObject prefab = fiveStars[Random.Range(0, threeStars.Length)];
            GameObject splash = Instantiate(prefab, splashBox.transform);
            splash.transform.SetParent(splashBox.transform);
            Destroy(splash, 0.5f);
        }
        else if (pullType == 6) {
            GameObject prefab = bannerFiveStar;
            GameObject splash = Instantiate(prefab, splashBox.transform);
            splash.transform.SetParent(splashBox.transform);
        }

        currentPrimos -= 160;
        primoCountText.text = currentPrimos.ToString();
        print(currentPrimos);
        //Add something when player is dead
        // if (currentPrimos <= 0)
        //     textbox.gameObject.GetComponent<Timer>().continueTimer = false;
    }


}