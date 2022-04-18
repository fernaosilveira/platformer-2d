using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Core.Singleton;

public class CollectableManager : Singleton<CollectableManager>
{

    [Header("Coins")]
    public int coins;
    public TextMeshProUGUI uiCoins;


    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        coins = 0;
    }

    public void AddCoins(int amount = 1)
    {
        coins += amount;
        UpdateUi();
    }

    public void UpdateUi()
    {
        uiCoins.text = coins.ToString();
    }
}
