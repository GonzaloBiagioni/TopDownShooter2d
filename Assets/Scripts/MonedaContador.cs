using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MonedaContador : MonoBehaviour
{
    public static MonedaContador Instance;
    public TMP_Text coinText;
    public int currentCoin = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        coinText.text = "Monedas: " + currentCoin.ToString();
    }

    public void IncreaseCoin(int v)
    {
        AudioManager.Instance.PlaySFX(0);
        currentCoin += v;
        coinText.text = "Monedas: " + currentCoin.ToString();
    }

}
