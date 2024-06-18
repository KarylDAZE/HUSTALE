using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigController : MonoBehaviour
{
    public float absSpeed = 5;
    public int health = 5;
    public bool isHorizontal = false;
    public bool directionToLeftOrDown = false;
    public AudioClip hurtClip;
    float speed;
    float directionChangeTimer = 5;
    Animator animator;
    Rigidbody2D rigidbody2d;
    AudioSource audioSource;

    void Start()
    {
        speed = absSpeed;
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        animator.SetFloat("LookDirection", directionToLeftOrDown ? -1 : 1);
        speed = directionToLeftOrDown ? -speed : speed;
    }

    void Update()
    {
        directionChangeTimer -= Time.deltaTime;
        if (directionChangeTimer < 0)
        {
            directionChangeTimer = 5;
            speed = -speed;
            directionToLeftOrDown = !directionToLeftOrDown;
            animator.SetFloat("LookDirection", speed / absSpeed);
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        if (isHorizontal)
        {
            position.x = position.x + speed * Time.deltaTime;
        }
        else
        {
            position.y = position.y + speed * Time.deltaTime;
        }
        rigidbody2d.position = position;
    }

    void OnCollisionEnter2D(Collision2D collision)
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
    }

    void Die()
    {
        rigidbody2d.simulated = false;
        GameObject main = GameObject.Find("--Main Control--");
        Quest quest = main.GetComponent<Quest>();
        quest?.ChangeEnemyCount(-1);
        Destroy(gameObject, 1);
    }
}
