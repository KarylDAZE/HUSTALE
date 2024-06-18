using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//UI管理相关脚本
public class UiManager : MonoBehaviour
{
    public static UiManager instance { get; private set; }
    public Image healthbar;
    public TextMeshProUGUI knifeCountText;

    private UiManager()
    {
    }

    private void Awake()
    {
        instance = this;
    }

    public void UpdateHealthBar(int currentamount, int amount)
    {
        healthbar.fillAmount = (float)currentamount / amount;
    }

    public void UpdateKnifeCountText(int current, int maximum)
    {
        knifeCountText.text = current.ToString() + " / " + maximum.ToString();
    }
}
