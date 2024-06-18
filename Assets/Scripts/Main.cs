using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Main : MonoBehaviour
{
    public static Main Instance;
    public static bool hasChangedScene = false;
    public static int collectedStarCount = 0;
    // Start is called before the first frame update
    void Awake()
    {
        if (!hasChangedScene)
        {
            PlayerPrefs.SetInt("MainCompleted", 0);
            PlayerPrefs.SetInt("LibraryCompleted", 0);
            PlayerPrefs.SetInt("OfficeCompleted", 0);
            PlayerPrefs.SetFloat("MainLookDirection.x", 0);
            PlayerPrefs.SetFloat("MainLookDirection.y", 1);
            PlayerPrefs.SetFloat("LibraryLookDirection.x", 0);
            PlayerPrefs.SetFloat("LibraryLookDirection.y", -1);
            PlayerPrefs.SetFloat("OfficeLookDirection.x", 0);
            PlayerPrefs.SetFloat("OfficeLookDirection.y", -1);
        }
    }

}
