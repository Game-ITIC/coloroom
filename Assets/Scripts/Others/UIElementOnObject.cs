using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UIElementOnObject : MonoBehaviour
{
    [SerializeField] private Transform target;

    private RectTransform _rt;
    private Canvas _canvas;
    private Camera _c;

    private void Awake()
    {
        _rt = GetComponent<RectTransform>();

        _canvas = GetComponentInParent<Canvas>();

        _c = Camera.main;
    }

    private void LateUpdate()
    {
        var vpos = _c.WorldToScreenPoint(target.position);
        var cpos = vpos / _canvas.scaleFactor;

        _rt.anchoredPosition = new Vector2(cpos.x, cpos.y);
    }

    public void SetTarget(Transform targ)
    {
        target = targ;
    }
}
