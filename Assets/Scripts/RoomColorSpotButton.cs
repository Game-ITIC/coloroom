using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomColorSpotButton : MonoBehaviour
{
    public static List<RoomColorSpotButton> all = new List<RoomColorSpotButton>();
    public static RoomColorSpotButton selected = null;

    [SerializeField] private RoomColorSpot colorSpot;
    [Space]
    [SerializeField] private GlobalEvent events;

    private void Awake()
    {
        all.Add(this);
    }

    private void Start()
    {
        Invoke("mode-0");
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

        if (MyPaletteItem.selected == null)
        {
            Invoke("mode-1");
        }
        else
        {
            var c = MyPaletteItem.selected.GetColor();
            colorSpot.SetColor(c);

            Invoke("mode-2");
        }

        foreach (var b in all) if (b != this) b.Invoke("hide");
        //show exit and save button of this
    }

    public void OnUnclick()
    {
        selected = null;

        colorSpot.OnUnselect();

        ///////
    }

    public void OnPaletteSelect()
    {
        var c = MyPaletteItem.selected.GetColor();

        colorSpot.SetColor(c);

        Invoke("mode-2");
    }

    public void OnCancel()
    {
        colorSpot.CancelColor();

        //
        foreach (var b in all) b.Invoke("mode-0");

        OnUnclick();
    }

    public void OnConfirm()
    {
        if (MyPaletteItem.selected.GetColor() == colorSpot.GetColor())
        {
            MyPaletteItem.selected.OnUnclick();
        }
        else
        {
            colorSpot.SaveColor();

            MyPaletteItem.selected.Waste();
        }

        foreach (var b in all) b.Invoke("mode-0");

        OnUnclick();
    }

    public void Invoke(string key)
    {
        if (events != null) events.Invoke(key);
    }
}
