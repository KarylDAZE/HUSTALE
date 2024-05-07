using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigController : MonoBehaviour
{
    public float absSpeed = 5;
    public int health = 5;
    public bool isHorizontal = false;
    public bool directionToLeftOrDown = false;
    //public ParticleSystem smokeEffect, fixBomb;
    public AudioClip hurtClip;
    float speed;
    float directionChangeTimer = 5;
    Animator animator;
    Rigidbody2D rigidbody2d;
    AudioSource audioSource;
    //public Quest quest;
    // Start is called before the first frame update
    void Start()
    {
        speed=absSpeed;
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        animator.SetFloat("LookDirection", directionToLeftOrDown ? -1 : 1);
        speed=directionToLeftOrDown ? -speed : speed;
    }

    // Update is called once per frame
    void Update()
    {
        directionChangeTimer-= Time.deltaTime;
        if (directionChangeTimer < 0)
        {
            directionChangeTimer = 5;
            speed=-speed;
            directionToLeftOrDown=!directionToLeftOrDown;
            animator.SetFloat("LookDirection", speed/absSpeed);
        }
    }
    private void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        if (isHorizontal)
        {
            position.x = position.x + speed*Time.deltaTime;
        }
        else
        {
            position.y = position.y + speed*Time.deltaTime;
        }
        rigidbody2d.position = position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.ChangeHealth(-2);
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
        GameObject main=GameObject.Find("--Main Control--");
        Quest quest = main.GetComponent<Quest>();
        if (quest != null)
            quest.ChangeEnemyCount(-1);
        Destroy(gameObject, 1);
    }
}
