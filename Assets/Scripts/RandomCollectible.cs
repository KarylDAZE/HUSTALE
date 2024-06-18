using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCollectible : MonoBehaviour
{
    public GameObject swordCollectiblePrefab, healthCollectiblePrefab;
    public PolygonCollider2D[] collectibleConfiners;
    public float swordCollectibleCd = 10, healthCollectibleCd = 20;
    float swordCollectibleTimer, healthCollectibleTimer;
    [SerializeField] int collectibleConfinersCount, swordCollectibleCount = 0, healthCollectibleCount = 0;
    PolygonCollider2D confiner;
    private GameObject poolRoot, swordPool, healthPool;
    private Queue<GameObject> swordQueue, healthQueue;
    public enum CollectibleType { SWORD, HEALTH };

    void Start()
    {
        swordCollectibleTimer = swordCollectibleCd;
        healthCollectibleTimer = healthCollectibleCd;
        collectibleConfinersCount = collectibleConfiners.Length;
        //create object pool
        poolRoot = new GameObject("Object Pools");
        swordPool = new GameObject("Sword Pool");
        healthPool = new GameObject("Health Pool");
        swordPool.transform.SetParent(poolRoot.transform);
        healthPool.transform.SetParent(poolRoot.transform);
        swordQueue = new Queue<GameObject>();
        healthQueue = new Queue<GameObject>();
    }

    void Update()
    {
        swordCollectibleTimer -= Time.deltaTime;
        healthCollectibleTimer -= Time.deltaTime;
        if (swordCollectibleTimer < 0)
        {
            swordCollectibleTimer = swordCollectibleCd;
            if (swordCollectibleCount < 5)
            {
                confiner = collectibleConfiners[Random.Range(0, collectibleConfinersCount)];
                Vector2 swordPos = new Vector2(Random.Range(confiner.bounds.min.x + 1, confiner.bounds.max.x - 1), Random.Range(confiner.bounds.min.y + 1, confiner.bounds.max.y - 1));
                GameObject newSwordCollectible = PopSwordCollectible();
                newSwordCollectible.transform.position = swordPos;
            }
        }
        if (healthCollectibleTimer < 0)
        {
            healthCollectibleTimer = healthCollectibleCd;
            if (healthCollectibleCount < 5)
            {
                confiner = collectibleConfiners[Random.Range(0, collectibleConfinersCount)];
                Vector2 healthPos = new Vector2(Random.Range(confiner.bounds.min.x + 1, confiner.bounds.max.x - 1), Random.Range(confiner.bounds.min.y + 1, confiner.bounds.max.y - 1));
                GameObject newHealthCollectible = PopHealthCollectible();
                newHealthCollectible.transform.position = healthPos;
            }
        }
    }

    public GameObject PopSwordCollectible()
    {
        GameObject swordPrefab;
        if (0 == swordQueue.Count)
        {
            swordPrefab = Instantiate(swordCollectiblePrefab, Vector3.zero, Quaternion.identity);
            swordPrefab.transform.SetParent(swordPool.transform);
        }
        else
            swordPrefab = swordQueue.Dequeue();
        swordPrefab.SetActive(true);
        ChangeSwordCollectibleCount(1);
        return swordPrefab;
    }

    public void PushSwordCollectible(GameObject swordPrefab)
    {
        if (swordPrefab == null) return;
        swordPrefab.SetActive(false);
        swordQueue.Enqueue(swordPrefab);
        ChangeSwordCollectibleCount(-1);
    }

    public GameObject PopHealthCollectible()
    {
        GameObject healthPrefab;
        if (0 == healthQueue.Count)
        {
            healthPrefab = Instantiate(healthCollectiblePrefab, Vector3.zero, Quaternion.identity);
            healthPrefab.transform.SetParent(healthPool.transform);
        }
        else
            healthPrefab = healthQueue.Dequeue();
        healthPrefab.SetActive(true);
        ChangeHealthCollectibleCount(1);
        return healthPrefab;
    }

    public void PushHealthCollectible(GameObject healthPrefab)
    {
        if (healthPrefab == null) return;
        healthPrefab.SetActive(false);
        healthQueue.Enqueue(healthPrefab);
        ChangeHealthCollectibleCount(-1);
    }

    public void ChangeHealthCollectibleCount(int count)
    {
        healthCollectibleCount += count;
    }
    public void ChangeSwordCollectibleCount(int count)
    {
        swordCollectibleCount += count;
    }
}
