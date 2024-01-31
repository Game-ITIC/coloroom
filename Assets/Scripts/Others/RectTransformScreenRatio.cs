using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(RectTransform))]
public class RectTransformScreenRatio : MonoBehaviour
{
    [SerializeField] private float height = 1f;

    private RectTransform _rt;

    private void Awake()
    {
        _rt = GetComponent<RectTransform>();
    }

    private void Update()
    {
        UpdateRatio();
    }

    private void UpdateRatio()
    {
        float ratio = 1f * Camera.main.pixelWidth / Camera.main.pixelHeight;

        var h = height;
        if (h < 0.1f) h = 0.1f;
        var w = h * ratio;

        _rt.sizeDelta = new Vector2(w, h);
    }
}
