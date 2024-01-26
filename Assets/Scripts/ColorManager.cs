using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public static ColorManager Instance;

    public enum ColorKey { pink, blue, gold }

    [Serializable]
    private class Color
    {
        public ColorKey key;
        public Material material;
    }

    [SerializeField] private Color[] colors;

    private void Awake()
    {
        Instance = this;
    }

    public Material GetColor(ColorKey key)
    {
        foreach (var c in colors)
            if (c.key == key) return c.material;

        return null;
    }
}
