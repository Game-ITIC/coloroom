using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MyPaletteItem : MonoBehaviour
{
    public static List<MyPaletteItem> all = new List<MyPaletteItem>();
    public static MyPaletteItem selected = null;

    [SerializeField] private ColorManager.ColorKey color;
    [SerializeField] private int count = 1;
    //timer
    //ad feature
    [Space]
    [SerializeField] private MeshRendererMaterials renderMaterial;
    [SerializeField] private TextMeshProUGUI countText;
    [Space]
    [SerializeField] private GlobalEvent events;

    private void Awake()
    {
        all.Add(this);
    }

    public void Set(ColorManager.ColorKey newColor, int newCount)
    {
        color = newColor;

        renderMaterial.SetMaterial(ColorManager.Instance.GetColor(color));

        count = newCount;

        countText.text = count.ToString();
    }

    public ColorManager.ColorKey GetColor()
    {
        return color;
    }

    public int GetCount()
    {
        return count;
    }

    //added

    public void Waste()
    {
        count--;

        countText.text = count.ToString();

        OnUnclick();

        if (count <= 0)
        {
            Invoke("on-waste");

            MyPaletteManager.Instance.OnPaletteChange();
        }
    }

    public void OnClick()
    {
        if (selected != null)
        {
            if (selected == this)
                return;
            else
                selected.OnUnclick();
        }

        selected = this;

        if (RoomColorSpotButton.selected != null)
            RoomColorSpotButton.selected.OnPaletteSelect();

        Invoke("on-select");
    }

    public void OnUnclick()
    {
        selected = null;

        Invoke("on-unselect");
    }

    public void Invoke(string key)
    {
        if (events != null) events.Invoke(key);
    }
}
