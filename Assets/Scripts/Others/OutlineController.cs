using System;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class OutlineController : MonoBehaviour
{
    [Serializable]
    private class OutlineStyle
    {
        public Color color = Color.white;
        public float width = 1f;
    }

    [SerializeField] private OutlineStyle[] styles = new OutlineStyle[1];
    [Space]
    [SerializeField] private bool SetFirstOnAwake = false;

    private Outline _o;

    private void Awake()
    {
        _o = GetComponent<Outline>();

        if (SetFirstOnAwake) SetStyle(0);
    }

    public void SetStyle(int id)
    {
        if (id < 0 || id >= styles.Length) return;

        _o.OutlineColor = styles[id].color;
        _o.OutlineWidth = styles[id].width;
    }
}
