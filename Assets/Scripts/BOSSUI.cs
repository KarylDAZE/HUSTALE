using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BOSSUI : MonoBehaviour
{
    public static BOSSUI Instance { get; private set; }
    public Image healthbar;
    private void Awake()
    {
        Instance = this;
        if (PlayerPrefs.GetInt("OfficeCompleted") == 1)
            healthbar.gameObject.SetActive(false);

    }

    public void UpdateHealthBar(int currentamount, int amount)
    {
        healthbar.fillAmount = (float)currentamount / amount;
    }

}
