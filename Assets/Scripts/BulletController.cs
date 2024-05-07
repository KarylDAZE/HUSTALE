using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody2D Rigidbody2d;
    public int attackHurt = 1;
    // Start is called before the first frame update
    void Awake()
    {
        Rigidbody2d = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude > 100) { Destroy(gameObject); }
    }
    public void Launch(Vector2 direction, float force)
    {
        Rigidbody2d.AddForce(direction *force);
        
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
