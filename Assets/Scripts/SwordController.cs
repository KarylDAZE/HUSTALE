using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    Rigidbody2D Rigidbody2d;
    public AudioClip throwClip;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Awake()
    {
        Rigidbody2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude > 100) { Destroy(gameObject); }
    }
    public void Launch(Vector2 direction, float force)
    {
        audioSource.PlayOneShot(throwClip);
        Rigidbody2d.AddForce(direction *force);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PigController enemyController = collision.gameObject.GetComponent<PigController>();
        BossController bossController = collision.gameObject.GetComponent<BossController>();
        GhostController ghostController = collision.gameObject.GetComponent<GhostController>();
        if (enemyController != null)
        {
            enemyController.Hurt();
            
        }
        else if (bossController != null)
        {
            bossController.Hurt();
        }
        else if(ghostController != null)
        {
            ghostController.Hurt();
        }
        Destroy(gameObject);
    }
}
