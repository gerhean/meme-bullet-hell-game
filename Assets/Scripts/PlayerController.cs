using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D body;
    private SpriteRenderer playerSprite;
    private float horizontal;
    private float vertical;

    public static PlayerController playerController;
    public Timer timer;
    public TMPro.TextMeshProUGUI primoCountText;
    public AudioSource pullSound;
    public GameObject splashBox;

    public float runSpeed = 15.0f;

    public int maxPrimos = 8000;
    private int currentPrimos = 1;
    private bool isFiftyFifty = true;
    private int bannerObtained = 0;

    public float commonDamageCooldownTime = 0.4f;
    public float rareDamageCooldownTime = 1.0f;
    private bool isDamageCooldown = false;

    public GameObject[] ticks;
    public GameObject[] threeStars;
    public GameObject[] fiveStars;
    public GameObject bannerFiveStar;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        playerController = this;
        currentPrimos = maxPrimos;
        primoCountText.text = currentPrimos.ToString();
        for (int i = 0; i < ticks.Length; i++) {
            ticks[i].SetActive(false);
        }
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
            if (isDamageCooldown || currentPrimos <= 0) {
                return;
            }
            isDamageCooldown = true;
            takeDamage(bullet.pullType);
            Destroy(bullet.gameObject);
        }
    }

    private IEnumerator BlinkWhileDamaged() {
        Color defaultColor = playerSprite.color;
        yield return new WaitForSeconds(0.01f);
        while (isDamageCooldown) {
            playerSprite.color = new Color(0f, 0f, 0f, 0f);
            yield return new WaitForSeconds(0.05f);
            playerSprite.color = defaultColor;
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator DamageCooldown(float damageCooldownTime)
    {
        yield return new WaitForSeconds(damageCooldownTime);
        isDamageCooldown = false;
    }

    void takeDamage(int pullType)
    {
        StartCoroutine(BlinkWhileDamaged());
        GameObject prefab = null;
        float damageCooldownTime = 0.0f;
        if (pullType == 3) {
            pullSound.Play();
            prefab = threeStars[Random.Range(0, threeStars.Length)];
            damageCooldownTime = commonDamageCooldownTime;
        }
        else if (pullType == 5) {
            damageCooldownTime = rareDamageCooldownTime;
            if (isFiftyFifty) {
                if (Random.Range(0, 3) == 0) {
                    pullType = 6;
                }
                else {
                    isFiftyFifty = false;
                }
            }
            else {
                pullType = 6;
                isFiftyFifty = true;
            }
        }
        else {
            return;
        }

        if (pullType == 5)
        {
            prefab = fiveStars[Random.Range(0, fiveStars.Length)];
        }
        else if (pullType == 6) {
            prefab = bannerFiveStar;
            ticks[bannerObtained].SetActive(true);
            bannerObtained++;
        }

        GameObject splash = Instantiate(prefab, splashBox.transform);
        splash.transform.SetParent(splashBox.transform);
        StartCoroutine(DamageCooldown(commonDamageCooldownTime));
        Destroy(splash, commonDamageCooldownTime);

        currentPrimos -= 160;
        primoCountText.text = currentPrimos.ToString();
        //Add something when player is dead
        if (currentPrimos <= 0) {
            currentPrimos = 0;
            timer.continueTimer = false;
        }
    }


}