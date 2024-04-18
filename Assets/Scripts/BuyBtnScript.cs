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

    public void OnPressed()
    {
        if (BuyColor.selected != null)
        {
            cost = BuyColor.selected.cost;
            ck = BuyColor.selected.colorKey;

            if (cost <= CoinManager.Instance._coins)
            {
                CoinManager.Instance.ReduceCoins(cost);

                List<ColorManager.ColorKey> colorKeys = new List<ColorManager.ColorKey>();
                colorKeys.Add(ck);
                MyPaletteManager.AddColorsStatic(colorKeys.ToArray());
            }
            else
            {
                
            }
        }
        else
        {

        }      
    }
}
