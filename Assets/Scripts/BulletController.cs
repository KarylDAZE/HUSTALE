using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody2D Rigidbody2d;
    public int attackHurt = 1;

    void Awake()
    {
        Rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (transform.position.magnitude > 100) { Destroy(gameObject); }
    }

    public void Launch(Vector2 direction, float force)
    {
        Rigidbody2d.AddForce(direction * force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.ChangeHealth(-attackHurt);
        }
        Destroy(gameObject);
    }
}
