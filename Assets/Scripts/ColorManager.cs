using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public enum ColorKey { red, orange, yellow, green, blue, purple, pink, brown, white, black, gray, lime, lightBlue, lilac, magenta, turquoise, maroon, peachy, golden, silver, sparklyRed, sparklyOrange, sparklyGreen, sparklyBlue, sparklyPurple, sparklyPink, sparklyBlack, sparklyLime, sparklyLightBlue, sparklyLilac, sparklyMagenta, patDots1, patDots2, patDots3, patFlower1, patFlower2, patFlower3, patFlower4, patGeo1, patGeo2, patGeo3, patHeart, patPlaid, patStars1, patStars2}

    [Serializable]
    private class Color
    {
        public ColorKey key;
        public Material material;
    }

    [SerializeField] private Color[] colors;

    public Material GetColor(ColorKey key)
    {
        foreach (var c in colors)
            if (c.key == key) return c.material;

        return null;
    }
}
