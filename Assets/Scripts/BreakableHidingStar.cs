using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BreakableHidingStar : MonoBehaviour
{
    public GameObject hidingStar;

    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (PlayerPrefs.GetInt(scene.name + "Completed") == 1)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SwordController controller = collision.gameObject.GetComponent<SwordController>();
        if (controller != null)
        {
            hidingStar.SetActive(true);
            Destroy(gameObject);
        }
    }
}
