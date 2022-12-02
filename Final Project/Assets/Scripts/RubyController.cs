using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RubyController : MonoBehaviour
{
    public float speed = 3.0f;

    public int maxHealth = 5;
    public float timeInvincible = 2.0f;

    public int health { get { return currentHealth; } }
    int currentHealth;

    bool isInvincible;
    float invincibleTimer;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    public GameObject projectilePrefab;

    AudioSource audioSource;

    public AudioClip cogSound;
    public AudioClip damagedSound;

    public GameObject damagedEffect;

    public GameObject loseMessage;

    public AudioSource musicSource;
    public AudioClip loseMusic;

    public int ammo;
    public TextMeshProUGUI ammoText;

    public bool canChangeLevel = false;

    public GameObject bomb;

    private void Awake()
    {
        loseMessage.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        ammo = 6;

        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;
            }

        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (ammo > 0)
            {
                Launch();
            }

        }

        if(Input.GetKeyDown(KeyCode.B)){
            if(ammo > 2){
                Place();
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    if (canChangeLevel && UIFixedRobots.instance.wonLevel)
                    {
                        character.ChangeLevel();
                    }
                    else
                    {
                        character.DisplayDialog();
                    }

                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        ammoText.text = "Ammo: " + ammo.ToString();
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {

            animator.SetTrigger("Hit");
            if (isInvincible)
            {
                return;
            }


            isInvincible = true;
            invincibleTimer = timeInvincible;

            this.PlaySound(damagedSound);
            GameObject damagedEff = Instantiate(this.damagedEffect, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
            Destroy(damagedEff, 1.0f);
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);

        if (currentHealth <= 0)
        {
            loseMessage.SetActive(true);
            this.gameObject.SetActive(false);
            musicSource.clip = loseMusic;
            musicSource.Play();
            RestartManager.instance.canRestart = true;
        }
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");

        this.PlaySound(cogSound);

        ammo--;
    }

    void Place(){
        GameObject bombObject = Instantiate(bomb, rigidbody2d.position, Quaternion.identity);
        bombObject.GetComponent<BombController>().GetRubyController(this);

        ammo -= 3;
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
