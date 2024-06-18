using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public float absSpeed = 1, directionChangeCd = 10, attackCd = 1, weakCd = 5;
    public int maxHealth = 30, health, attackHurt = 3;
    public int bulletSpeed = 3;
    public ParticleSystem dieParticle;
    public GameObject[] bulletPrefab;
    public AudioClip hurtClip;
    float speed;
    float directionChangeTimer, attackTimer, weakTimer;
    int hurtCount = 0;
    bool isWeak, diedOnce;
    Animator animator;
    Rigidbody2D rigidbody2d;
    AudioSource audioSource;

    //Start is called before the first frame update
    void Start()
    {
        speed = absSpeed;
        health = maxHealth;
        BOSSUI.Instance.UpdateHealthBar(health, maxHealth);
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        isWeak = false;
        directionChangeTimer = directionChangeCd;
        attackTimer = attackCd;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWeak)
        {
            directionChangeTimer -= Time.deltaTime;
            attackTimer -= Time.deltaTime;
        }
        else
            weakTimer -= Time.deltaTime;

        if (directionChangeTimer < 0)
        {
            directionChangeTimer += directionChangeCd;
            transform.eulerAngles = new Vector3(0, 180 * (speed < 0 ? 0 : 1), 0);
            speed = -speed;
        }
        if (attackTimer < 0 && !isWeak)
        {
            attackTimer += attackCd;
            Attack();
        }
        isWeak = weakTimer > 0;

        animator.SetBool("isWeak", isWeak);
    }

    private void FixedUpdate()
    {
        if (isWeak) return;
        rigidbody2d.position += new Vector2(speed * Time.deltaTime, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.ChangeHealth(-3);
        }
    }

    void Attack()
    {
        GameObject bulletObj = Instantiate(bulletPrefab[Random.Range(0, 4)], rigidbody2d.position + Vector2.right * Random.Range(-7, 7), Quaternion.identity);
        BulletController bulletController = bulletObj.GetComponent<BulletController>();
        if (bulletController != null)
        {
            bulletController.attackHurt = attackHurt;
            bulletController.Launch(Vector2.down, 4);
        }
    }

    public void Hurt()
    {
        health--;
        animator.SetTrigger("Hit");
        audioSource.PlayOneShot(hurtClip);
        if (weakTimer <= 0)
            hurtCount++;
        if (hurtCount >= 10)
        {
            isWeak = true;
            weakTimer = weakCd;
            hurtCount = 0;
        }
        if (health <= 0)
        {
            if (!diedOnce)
            {
                health = maxHealth;
                isWeak = false;
                diedOnce = true;
            }
            else
                Die();
        }
        BOSSUI.Instance.UpdateHealthBar(health, maxHealth);

    }
    void Die()
    {
        GameObject main = GameObject.Find("--Main Control--");
        Quest quest = main.GetComponent<Quest>();
        if (quest != null)
            quest.ChangeEnemyCount(-10);
        GameObject dieBomb = Instantiate(dieParticle.gameObject, rigidbody2d.position, Quaternion.identity);
        rigidbody2d.simulated = false;
        Destroy(gameObject, 1);
    }
}
