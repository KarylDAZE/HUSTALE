using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GhostController : MonoBehaviour
{
    public float moveDistance = 5;
    public int health = 5;
    public bool isHorizontal = false, directionToLeftOrDown = false;
    public float attackCd = 1;
    public GameObject bulletPrefab;
    bool isDisappearing;
    //public ParticleSystem smokeEffect, fixBomb;
    public AudioClip hurtClip;
    float directionChangeTimer = 5, disappearTimer = 0.5f, attackTimer;
    Animator animator;
    Rigidbody2D rigidbody2d;
    AudioSource audioSource;
    //public Quest quest;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        isDisappearing = false;
        attackTimer = attackCd;
        transform.eulerAngles=new Vector3(0, 180*(directionToLeftOrDown ? 0 : 1), 0);

    }

    // Update is called once per frame
    void Update()
    {
        directionChangeTimer-= Time.deltaTime;
        attackTimer -= Time.deltaTime;
        if (directionChangeTimer < 0)
        {
            directionChangeTimer = 5;
            isDisappearing=true;
            animator.SetTrigger("Disappear");
            Invoke("Move", 0.4f);
        }
        if (isDisappearing)
        {
            disappearTimer -= Time.deltaTime;
            if (disappearTimer < 0)
            {
                disappearTimer = 0.5f;
                isDisappearing=false;
                animator.SetTrigger("Appear");
            }
        }
        if (attackTimer < 0&&!isDisappearing)
        {
            attackTimer = attackCd;
            Attack();
        }
    }
    private void FixedUpdate()
    {

    }
    void Move()
    {
        Vector2 position = rigidbody2d.position;
        if (isHorizontal)
        {
            position.x = position.x + moveDistance*(directionToLeftOrDown ? -1 : 1);
        }
        else
        {
            position.y = position.y + moveDistance*(directionToLeftOrDown ? -1 : 1);
        }
        directionToLeftOrDown=!directionToLeftOrDown;
        transform.eulerAngles=new Vector3(0, 180*(directionToLeftOrDown ? 0 : 1), 0);
        rigidbody2d.position = position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }
    void Attack()
    {
        Vector2 lookDirection = new Vector2(isHorizontal ? (directionToLeftOrDown ? -1 : 1) : 0, isHorizontal ? 0 : (directionToLeftOrDown ? -1 : 1));
        GameObject bulletObj = Instantiate(bulletPrefab, rigidbody2d.position, Quaternion.identity);
        BulletController bulletController = bulletObj.GetComponent<BulletController>();
        if (bulletController != null)
        {
            bulletController.transform.eulerAngles = lookDirection;
            bulletController.attackHurt=2;
            bulletController.Launch(lookDirection, 4);
        }
    }
    public void Hurt()
    {
        health--;
        animator.SetTrigger("Hit");
        audioSource.PlayOneShot(hurtClip);
        if (health <= 0)
        {
            Die();
        }
        //GameObject fixParticle = Instantiate(fixBomb.gameObject, rigidbody2d.position, Quaternion.identity);
        //audioSource.Stop();
        //audioSource.PlayOneShot(fixedClip);
    }
    void Die()
    {
        rigidbody2d.simulated=false;
        GameObject main = GameObject.Find("--Main Control--");
        Quest quest = main.GetComponent<Quest>();
        if (quest != null)
            quest.ChangeEnemyCount(-1);
        Destroy(gameObject, 1);
    }
}
