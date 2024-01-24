using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [Serializable]
    public class Color
    {
        public string id = "";
        public Material material;
    }

    [SerializeField] private Color[] colors;

    public Material GetColor(string id)
    {
        foreach (var c in colors)
            if (c.id == id) return c.material;

        return null;
    }
}
