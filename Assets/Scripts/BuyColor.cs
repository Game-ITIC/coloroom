using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyColor : MonoBehaviour
{
    public int cost;
    static BuyColor selected;
    [SerializeField] GameObject btn;
    private TextMeshProUGUI btnText;

    public void Start()
    {
        btnText = btn.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void OnMouseDown()
    {
        if (selected != null)
        {
            selected.transform.GetChild(0).gameObject.SetActive(false);
            selected = null;
        }
        selected = this;
        transform.GetChild(0).gameObject.SetActive(true);
        btnText.text = selected.cost.ToString();
    }
}
