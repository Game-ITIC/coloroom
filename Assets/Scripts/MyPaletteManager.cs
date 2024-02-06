using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPaletteManager : MonoBehaviour
{
    public static MyPaletteManager Instance;

    [Serializable] private class Palette
    {
        [Serializable] public class Item
        {
            public Item(ColorManager.ColorKey setColor, int setCount)
            {
                color = setColor;
                count = setCount;
            }

            public ColorManager.ColorKey color;
            public int count = 1;
        }

        public List<Item> items = new List<Item>();
    }

    [SerializeField] private Palette palette;
    [Space]
    [SerializeField] private MyPaletteItem paletteItemPrefab;
    [SerializeField] private RectTransform paletteItemParent;

    private List<MyPaletteItem> _items = new List<MyPaletteItem>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        OpenPalette();
    }

    private void OpenPalette()
    {
        for (int i = 0; i < paletteItemParent.childCount; i++)
            Destroy(paletteItemParent.GetChild(i).gameObject);
        ////sdelat umnee esli yest tsvet ostavit i minusovat...

        var p = JsonUtility.FromJson<Palette>(PlayerPrefs.GetString("my-palette-data"));
        if (p != null) palette = p;

        foreach (var item in palette.items)
        {
            if (item.count < 1) continue;

            var i = Instantiate(paletteItemPrefab, paletteItemParent);
            i.Set(item.color, item.count);

            _items.Add(i);
        }
    }

    private void SavePalette()
    {
        palette.items.Clear();
        foreach (var i in _items)
        {
            if (i.GetCount() < 1) continue;
            palette.items.Add(new Palette.Item(i.GetColor(), i.GetCount()));
        }

        PlayerPrefs.SetString("my-palette-data", JsonUtility.ToJson(palette));
    }

    private void AddItem()
    {
        //
    }

    //(just for this session add item with watching ad feature   function)

    private void RemoveItem()
    {
        //
    }

    public void OnPaletteChange()
    {
        SavePalette();
    }


    public static void AddColorsStatic(ColorManager.ColorKey[] colors)
    {
        Debug.Log(colors.Length);

        Palette p = JsonUtility.FromJson<Palette>(PlayerPrefs.GetString("my-palette-data"));
        if (p == null) p = new Palette();

        foreach (var color in colors)
        {
            bool added = false;

            for (int i = 0; i < p.items.Count; i++)
                if (p.items[i].color == color)
                {
                    p.items[i].count++;

                    added = true;

                    break;
                }

            if (!added)
                p.items.Add(new Palette.Item(color, 1));
        }

        Debug.Log(p.items.Count);

        PlayerPrefs.SetString("my-palette-data", JsonUtility.ToJson(p));
    }
}
