using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quest : MonoBehaviour
{
    public EnterDoor[] doors;
    public GameObject star;
    GameObject enemy, collectibles;
    List<GameObject> enemys;
    int enemyCount;
    bool questCompleted;
    Scene scene;
    GameObject mainControl;
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        mainControl = GameObject.Find("--Main Control--");

        questCompleted=PlayerPrefs.GetInt(scene.name+"Completed")==1;
        enemys = new List<GameObject>();
        enemy=GameObject.Find("Enemy");
        foreach (Transform t in enemy.transform)
        {
            if (t.gameObject.activeSelf)
                enemys.Add(t.gameObject);
        }
        enemyCount = enemys.Count;
        if (questCompleted)
        {
            doors[0].isOpen=true;
            if (PlayerPrefs.GetInt("LibraryCompleted")==1&&doors.Length>1)
                doors[1].isOpen=true;
            enemy.SetActive(false);
            if (mainControl != null)
                mainControl.GetComponent<RandomCollectible>().enabled=false;
        }
        else
            foreach (EnterDoor door in doors)
                door.isOpen=false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeEnemyCount(int count)
    {
        enemyCount+=count;
        if (enemyCount <= 0)
        {
            questCompleted = true;
            doors[0].isOpen=true;
            if (mainControl != null)
                mainControl.GetComponent<RandomCollectible>().enabled=false;
            if (PlayerPrefs.GetInt("LibraryCompleted")==1&&doors.Length>1)
                doors[1].isOpen=true;
            star.SetActive(true);
            //UI
        }
    }
}
