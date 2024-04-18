using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    [SerializeField] private TextMeshProUGUI coinText;

    public int _coins = 0;

    private void Awake()
    {
        Instance = this;

        if (!PlayerPrefs.HasKey("coin-count")) PlayerPrefs.SetInt("coin-count", 0);
    }

    private void Start()
    {
        SetCoins(PlayerPrefs.GetInt("coin-count"));
    }

    public void SetCoins(int value)
    {
        if (value != _coins)
        {
            PlayerPrefs.SetInt("coin-count", value);

            GlobalEvent.InvokeGlobal("on-coins-change");
        }

        DOVirtual.Int(_coins, value, 1f, (val) => { coinText.text = val.ToString(); });

        _coins = value;
    }

    public void AddCoins(int value)
    {
        SetCoins(_coins + value);
    }

    public void ReduceCoins(int value)
    {
        SetCoins(_coins - value);
    }
}
