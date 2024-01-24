using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPaletteManager : MonoBehaviour
{
    [Serializable] private class Palette
    {
        [Serializable] public class Item
        {
            public Item(ColorManager.ColorKey setColor)
            {
                color = setColor;
                count = 1;
            }

            public ColorManager.ColorKey color;
            public int count = 0;
        }

        public List<Item> items = new List<Item>();
    }

    [SerializeField] private Palette palette;
    //[Space]
    //prefabs

    private MyPaletteItem[] _items;

    private void Awake()
    {
        //
    }

    private void Start()
    {
        OpenPalette();
    }

    private void OpenPalette()
    {
        //delete olds

        var p = JsonUtility.FromJson<Palette>(PlayerPrefs.GetString("my-palette-data"));
        if (p != null) palette = p;

        //draw palette items
    }

    private void SavePalette()
    {
        //read from items into palette

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


    public static void AddColorsStatic(ColorManager.ColorKey[] colors)
    {
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
                p.items.Add(new Palette.Item(color));
        }

        PlayerPrefs.SetString("my-palette-data", JsonUtility.ToJson(p));
    }
}
