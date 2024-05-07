using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D collision)
   {
      PlayerController controller = collision.GetComponent<PlayerController>();
      if (controller != null)
            if (controller.currentSword<controller.maxSword)
            {
                controller.ChangeHealth(2);
            GameObject.Find("--Main Control--").GetComponent<RandomCollectible>().ChangeSwordCollectibleCount(-1);

            Destroy(gameObject); 
      }
      
   }

}
