using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rigidbody2d;
    public bool vertical;
    private int direction = 1;
    public float changeTime = 10.0f;
    private float timer;
    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        PlayMoveAnimation();

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction = -direction;
            PlayMoveAnimation();
            timer = changeTime;
        }
        Vector2 position = rigidbody2d.position;
        if (vertical==false)
        {
            position.x = position.x + speed * Time.deltaTime * direction;
        }
        else
        {
            position.y = position.y + speed * Time.deltaTime * direction;
        }
        
        rigidbody2d.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.ChangeHealth(-1);
        }

    }

    private void PlayMoveAnimation()
    {
        if (vertical)
        {
            animator.SetFloat("MoveX",0);
            animator.SetFloat("MoveY",direction);
        }
        else
        {
            animator.SetFloat("MoveX",direction);
            animator.SetFloat("MoveY",0);
        }
    }
}
