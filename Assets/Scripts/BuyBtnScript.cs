using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static ColorManager;

public class BuyBtnScript : MonoBehaviour
{
    int cost = 0;
    ColorManager.ColorKey ck;
    public AudioSource aud1, aud2;
    private Animator anim;
    public string animationName = "btnAnimation";

    private void Awake()
    {
        aud1 = GetComponent<AudioSource>();
        aud1 = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    public void OnPressed()
    {
        if (BuyColor.selected != null)
        {
            cost = BuyColor.selected.cost;
            ck = BuyColor.selected.colorKey;

            if (cost <= CoinManager.Instance._coins)
            {
                CoinManager.Instance.ReduceCoins(cost);
                aud1.Play();
                anim.Play(animationName, -1, 0f);

                List<ColorManager.ColorKey> colorKeys = new List<ColorManager.ColorKey>();
                colorKeys.Add(ck);
                MyPaletteManager.AddColorsStatic(colorKeys.ToArray());
            }
            else
            {
                aud2.Play();
            }
        }
        else
        {

        }      
    }
}
