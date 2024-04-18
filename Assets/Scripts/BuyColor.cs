using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyColor : MonoBehaviour
{
    public int cost;
    public ColorManager.ColorKey colorKey;
    public static BuyColor selected;
    [SerializeField] GameObject btn;
    private TextMeshProUGUI btnText;
    private Image img1, img2;
    private AudioSource aud;

    public float scaleFactor = 1f;

    public void Start()
    {
        btnText = btn.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>();
        img1 = btn.transform.GetChild(1).gameObject.GetComponent<Image>();
        img2 = btn.transform.GetChild(2).gameObject.GetComponent<Image>();
        aud = GetComponent<AudioSource>();
    }

    public void OnMouseDown()
    {
        if (selected != null)
        {
            selected.transform.GetChild(3).gameObject.SetActive(false);
            selected.transform.localScale /= scaleFactor;
            selected = null;
        }
        selected = this;
        transform.GetChild(3).gameObject.SetActive(true);
        btnText.text = selected.cost.ToString();

        aud.Play();
        transform.localScale *= scaleFactor;
        BtnColorChanger();
    }

    public void Update()
    {
        //BtnColorChanger();
    }

    public void BtnColorChanger()
    {
        if (selected.cost <= CoinManager.Instance._coins)
        {
            img1.color = new Color(0.1f, 0.75f, 0f, 1f);
            img2.color = new Color(0.12f, 0.89f, 0f, 1f);
        }
        else
        {
            img1.color = new Color(0.75f, 0.09f, 0f, 1f);
            img2.color = new Color(1f, 0.25f, 0.17f, 1f);
        }
    }
}
