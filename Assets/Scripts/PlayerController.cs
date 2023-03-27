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
    public FloatingJoystick joystick;
    public Timer timer;
    public GameOver gameOverScreen;
    public TMPro.TextMeshProUGUI primoCountText;
    public AudioSource pullSound;
    public GameObject splashBox;

    public float runSpeed = 15.0f;

    public int maxPrimos = 8000;
    private int currentPrimos = 1;
    private bool isFiftyFifty = true;
    private int threeStarObtained = 0;
    private int fiveStarObtained = 0;
    private int bannerObtained = 0;

    public float commonDamageCooldownTime = 0.4f;
    public float rareDamageCooldownTime = 1.0f;
    private bool isDamageCooldown = false;
    private bool isGameOver = false;
    private Vector3 minScreenBounds;
    private Vector3 maxScreenBounds;
    private BulletController bulletInCollider;

    public GameObject[] ticks;
    public GameObject[] threeStars;
    public GameObject[] fiveStars;
    public GameObject bannerFiveStar;

    void Awake()
    {
        minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
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
        if (horizontal == 0 && vertical == 0) {
            horizontal = joystick.Horizontal;
            vertical = joystick.Vertical;
        }
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal, vertical) * runSpeed * Time.fixedDeltaTime;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minScreenBounds.x + 1, maxScreenBounds.x - 1),Mathf.Clamp(transform.position.y, minScreenBounds.y + 1, maxScreenBounds.y - 1), transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        BulletController bullet = col.gameObject.GetComponentInParent<BulletController>();
        if (bullet != null)
        {
            if (isGameOver) {
                return;
            }
            if (isDamageCooldown) {
                if (bullet.pullType != 3) {
                    bulletInCollider = bullet;
                }
                return;
            }
            takeDamage(bullet.pullType);
            Destroy(bullet.gameObject);
        }
    }

    private void OnTriggerExit2D (Collider2D col) {
        BulletController bullet = col.gameObject.GetComponentInParent<BulletController>();
        if (bullet != null && bullet == bulletInCollider) {
            bulletInCollider = null;
        }        
      }

    public void setIsGameOver(bool isGameOver)
    {
        this.isGameOver = isGameOver;
        if (isGameOver) {
            timer.continueTimer = false;
            gameOverScreen.OpenScreen(threeStarObtained, fiveStarObtained, bannerObtained, currentPrimos);
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
        if (bulletInCollider != null) {
            takeDamage(bulletInCollider.pullType);
            Destroy(bulletInCollider.gameObject);
            bulletInCollider = null;
        }
    }

    void takeDamage(int pullType)
    {
        isDamageCooldown = true;
        StartCoroutine(BlinkWhileDamaged());
        GameObject prefab = null;
        float damageCooldownTime = 0.0f;
        pullSound.Play();
        if (pullType == 3) {
            threeStarObtained++;
            damageCooldownTime = commonDamageCooldownTime;
            prefab = threeStars[Random.Range(0, threeStars.Length)];
        }
        else if (pullType == 5) {
            damageCooldownTime = rareDamageCooldownTime;
            if (isFiftyFifty) {
                if (Random.Range(0, 2) == 0) {
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
            Debug.Log("Invalid pull type");
            return;
        }

        if (pullType == 5)
        {
            fiveStarObtained++;
            prefab = fiveStars[Random.Range(0, fiveStars.Length)];
        }
        else if (pullType == 6) {
            ticks[bannerObtained].SetActive(true);
            damageCooldownTime += 1.0f;
            bannerObtained++;
            prefab = bannerFiveStar;
        }

        GameObject splash = Instantiate(prefab, splashBox.transform);
        splash.transform.SetParent(splashBox.transform);
        StartCoroutine(DamageCooldown(damageCooldownTime));
        Destroy(splash, damageCooldownTime);

        currentPrimos -= 160;
        primoCountText.text = currentPrimos.ToString();
        //Add something when player is dead
        if (currentPrimos <= 0) {
            currentPrimos = 0;
            setIsGameOver(true);
        }
        if (bannerObtained >= 7) {
            setIsGameOver(true);
        }
    }


}