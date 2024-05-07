using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCollectible : MonoBehaviour
{
    public AudioClip winClip,collectClip,winMusic;
    private void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController controller = collision.GetComponent<PlayerController>();
        if (controller != null)
        {
            Main.collectedStarCount++;
        PlayerController.PlaySound(collectClip);
            Destroy(this.gameObject);
            if (Main.collectedStarCount >= 4)
                Win();
        }
    }
    void Win()
    {
        PlayerController.PlaySound(winClip);
        GameObject main = GameObject.Find("--Main Control--");
        main.GetComponent<AudioSource>().Stop();
        main.GetComponent<AudioSource>().PlayOneShot(winMusic);
        Hint.instance.Win();
    }
}
