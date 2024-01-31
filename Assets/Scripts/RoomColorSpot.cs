using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomColorSpot : MonoBehaviour
{
    [SerializeField] private MeshRendererMaterials[] meshRenderers;
    [Space]
    [SerializeField] private Material whiteMaterial; 
    [Space]
    [SerializeField] private GlobalEvent events;

    private ColorManager.ColorKey? _colorKey = null;
    private ColorManager.ColorKey? _newColorKey = null;

    private void Awake()
    {
        SetMaterial(whiteMaterial);
    }

    private void SetMaterial(Material mat)
    {
        foreach (var mr in meshRenderers)
            mr.SetMaterial(mat);
    }

    public void SetColor(ColorManager.ColorKey? colorKey)
    {
        Material mat = null;

        if (colorKey != null)
            mat = ColorManager.Instance.GetColor(colorKey.Value);

        if (mat == null)
            mat = whiteMaterial;
            
        //SetMaterial(mat);
        this.DelayedAction(0.2f, () => SetMaterial(mat));

        _newColorKey = colorKey;

        if (events != null) events.Invoke("on-spot-color-set");
        GlobalEvent.InvokeGlobal("on-any-spot-color-set");

        ////
        foreach (var mr in meshRenderers)
            TweenAnims.ObjectPulse(mr.meshRenderer.transform);
    }

    public void SaveColor()
    {
        _colorKey = _newColorKey;

        if (events != null) events.Invoke("on-spot-color-save");
        GlobalEvent.InvokeGlobal("on-any-spot-color-save");
    }

    public void CancelColor()
    {
        if (_newColorKey != _colorKey) SetColor(_colorKey);

        if (events != null) events.Invoke("on-spot-color-cancel");
        GlobalEvent.InvokeGlobal("on-any-spot-color-cancel");
    }

    public ColorManager.ColorKey? GetColor()
    {
        return _colorKey;
    }

    public void OnSelect()
    {
        foreach (var mr in meshRenderers)
            SelectOutliner.Instance.Select(mr.meshRenderer.gameObject);
    }

    public void OnUnselect()
    {
        foreach (var mr in meshRenderers)
            SelectOutliner.Instance.Unselect(mr.meshRenderer.gameObject);
    }
}
