using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollectible : MonoBehaviour
{
    public AudioClip collectedClip;

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController controller = collision.GetComponent<PlayerController>();
        if (controller != null)
            if (controller.currentSword < controller.maxSword)
            {
                controller.ChangeSword(10);
                GameObject.Find("--Main Control--").GetComponent<RandomCollectible>().pushSwordCollectible(gameObject);
                //PlayerController.PlaySound(collectedClip);
            }
    }
}
