using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCollectible : MonoBehaviour
{
    public GameObject swordCollectiblePrefab, healthCollectiblePrefab;
    public PolygonCollider2D[] collectibleConfiners;
    public float swordCollectibleCd=10, healthCollectibleCd=20;
    float swordCollectibleTimer, healthCollectibleTimer;
    int collectibleConfinersCount, swordCollectibleCount=0, healthCollectibleCount=0;
    PolygonCollider2D confiner;
    // Start is called before the first frame update
    void Start()
    {
        swordCollectibleTimer=swordCollectibleCd;
        healthCollectibleTimer=healthCollectibleCd;
        collectibleConfinersCount=collectibleConfiners.Length;
    }

    // Update is called once per frame
    void Update()
    {
        swordCollectibleTimer-=Time.deltaTime;
        healthCollectibleTimer-=Time.deltaTime;
        if (swordCollectibleTimer < 0&&swordCollectibleCount<=5)
        {
            swordCollectibleTimer=swordCollectibleCd;
            confiner=collectibleConfiners[Random.Range(0, collectibleConfinersCount)];
            Vector2 swordPos = new Vector2(Random.Range(confiner.bounds.min.x+1, confiner.bounds.max.x-1), Random.Range(confiner.bounds.min.y+1, confiner.bounds.max.y-1));
            GameObject newSwordCollectible =Instantiate(swordCollectiblePrefab,swordPos,Quaternion.identity);
            swordCollectibleCount++;
        }
        if (healthCollectibleTimer < 0&&healthCollectibleCount<=5)
        {
            healthCollectibleTimer = healthCollectibleCd;
            confiner=collectibleConfiners[Random.Range(0, collectibleConfinersCount)];
            Vector2 healthPos = new Vector2(Random.Range(confiner.bounds.min.x+1, confiner.bounds.max.x-1), Random.Range(confiner.bounds.min.y+1, confiner.bounds.max.y-1));
            GameObject newHealthCollectible = Instantiate(healthCollectiblePrefab, healthPos, Quaternion.identity);
            healthCollectibleCount++;
        }
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
