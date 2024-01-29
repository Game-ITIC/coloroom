using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPaletteItem : MonoBehaviour
{
    public static List<MyPaletteItem> all = new List<MyPaletteItem>();
    public static MyPaletteItem selected = null;

    [SerializeField] private ColorManager.ColorKey color;
    //count //i yesli coount 0 to waste nelza
    //timer
    //ad feature
    [Space]
    [SerializeField] private MeshRendererMaterials renderMaterial;
    [Space]
    [SerializeField] private GlobalEvent events;

    private void Awake()
    {
        all.Add(this);
    }

    public void SetColor(ColorManager.ColorKey value)
    {
        color = value;

        renderMaterial.SetMaterial(ColorManager.Instance.GetColor(color));
    }

    public ColorManager.ColorKey GetColor()
    {
        return color;
    }

    //added

    //wasted

    public void OnClick()
    {
        if (selected != null) selected.OnUnclick();
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
