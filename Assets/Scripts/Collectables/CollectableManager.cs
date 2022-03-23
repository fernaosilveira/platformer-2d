using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectableManager : MonoBehaviour
{

    public static CollectableManager Instance;

    [Header("Coins")]
    public int coins;
    public TextMeshProUGUI uiCoins;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
