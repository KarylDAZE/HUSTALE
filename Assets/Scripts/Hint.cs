using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hint : MonoBehaviour
{
    bool isHintAppearing = true, isLose = false, isWin = false;
    public GameObject hint, helpHint, loseHint, winHint;
    public static Hint instance { get; private set; }

    void Start()
    {
        instance = this;
        if (Main.hasChangedScene) return;
        Time.timeScale = 0;
        hint.SetActive(true);
        helpHint.SetActive(true);
        loseHint.SetActive(false);
        winHint.SetActive(false);
    }

    void Update()
    {
        if (isLose)
        {
            if (Input.anyKeyDown)
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (isWin)
        {
            if (Input.anyKeyDown)
            {
                hint.SetActive(false);
                isWin = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape) && isHintAppearing)
            {
                isHintAppearing = false;
                Time.timeScale = 1;
                hint.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Q) && isHintAppearing)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && !isHintAppearing)
            {
                isHintAppearing = true;
                Time.timeScale = 0;
                hint.SetActive(true);
                helpHint.SetActive(true);
                loseHint.SetActive(false);
                winHint.SetActive(false);
            }
        }
    }
    public void Lose()
    {
        hint.SetActive(true);
        loseHint.SetActive(true);
        helpHint.SetActive(false);
        winHint.SetActive(false);
        isLose = true;
    }
    public void Win()
    {
        isWin = true;
        hint.SetActive(true);
        winHint.SetActive(true);
        loseHint.SetActive(false);
        helpHint.SetActive(false);
    }
}
