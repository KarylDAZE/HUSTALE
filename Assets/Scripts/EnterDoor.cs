using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterDoor : MonoBehaviour
{
    public string enterSceneName;
    public bool isOpen = false;
    public AudioClip doorClip;
    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        Scene scene = SceneManager.GetActiveScene();

        if (player != null && isOpen)
        {
            PlayerPrefs.SetInt("health", player.currentHealth);
            PlayerPrefs.SetInt("sword", player.currentSword);
            PlayerPrefs.SetInt(scene.name + "Completed", 1);
            if (scene.name.Equals("Main"))
            {
                if (enterSceneName.Equals("Library"))
                {
                    PlayerPrefs.SetFloat("Position.x", player.transform.position.x);
                    PlayerPrefs.SetFloat("Position.y", player.transform.position.y - 1);
                }
                else if (enterSceneName.Equals("Office"))
                {
                    PlayerPrefs.SetFloat("Position.x", player.transform.position.x + 1);
                    PlayerPrefs.SetFloat("Position.y", player.transform.position.y);
                }
            }
            PlayerPrefs.SetFloat(scene.name + "LookDirection.x", player.lookDirection.x);
            PlayerPrefs.SetFloat(scene.name + "LookDirection.y", player.lookDirection.y);

            Main.hasChangedScene = true;
            audioSource.PlayOneShot(doorClip);
            DontDestroyOnLoad(audioSource);
            SceneManager.LoadScene(enterSceneName);
        }
    }
}
