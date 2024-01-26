using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomColorSpotButton : MonoBehaviour
{
    public static List<RoomColorSpotButton> all = new List<RoomColorSpotButton>();
    public static RoomColorSpotButton selected = null;

    //static all

    //active one (a drugie vse skrit i pokazat knopki x i v)

    [SerializeField] private RoomColorSpot colorSpot;

    private void Awake()
    {
        all.Add(this);
    }

    public void SetColorSpot(RoomColorSpot value)
    {
        colorSpot = value;
    }

    public void OnClick()
    {
        if (colorSpot == null) return;

        if (selected != null) selected.OnUnclick();
        selected = this;

        colorSpot.OnSelect();

        //colorSpot.SetColor(ColorManager.ColorKey.pink);//////

        foreach (var b in all)
            b.gameObject.SetActive(false);//////
        //hide all spot buttons
        //show exit and save button of this
    }

    public void OnUnclick()
    {
        selected = null;

        colorSpot.OnUnselect();
    }
}
