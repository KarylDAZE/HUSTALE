using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    public PlayerController player;

    void Start()
    {
        if (!Main.hasChangedScene) return;
        Scene scene = SceneManager.GetActiveScene();
        int health = PlayerPrefs.GetInt("health"), sword = PlayerPrefs.GetInt("sword");
        player.currentHealth = health;
        player.currentSword = sword;
        UiManager.instance.UpdateHealthBar(health, player.maxHealth);
        UiManager.instance.UpdateKnifeCountText(sword, player.maxSword);
        if (scene.name.Equals("Main"))
        {
            player.transform.position = new Vector2(PlayerPrefs.GetFloat("Position.x"), PlayerPrefs.GetFloat("Position.y"));
        }
    }
}
