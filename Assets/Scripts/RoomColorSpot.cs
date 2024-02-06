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

    private ColorManager.ColorKey _colorKey = ColorManager.ColorKey.none;
    private ColorManager.ColorKey _newColorKey = ColorManager.ColorKey.none;
    private RoomColorSpotButton _button;

    private void Awake()
    {
        SetMaterial(whiteMaterial);
    }

    private void SetMaterial(Material mat)
    {
        foreach (var mr in meshRenderers)
            mr.SetMaterial(mat);
    }

    public void SetInitColor(ColorManager.ColorKey colorKey)
    {
        Material mat = null;

        if (colorKey != ColorManager.ColorKey.none)
            mat = ColorManager.Instance.GetColor(colorKey);

        if (mat == null)
            mat = whiteMaterial;

        SetMaterial(mat);

        if (colorKey != ColorManager.ColorKey.none)
            if (events != null) events.Invoke("on-spot-color-set");

        _newColorKey = colorKey;
        _colorKey = colorKey;
    }

    public void SetColor(ColorManager.ColorKey colorKey)
    {
        Material mat = null;

        if (colorKey != ColorManager.ColorKey.none)
            mat = ColorManager.Instance.GetColor(colorKey);

        if (mat == null)
            mat = whiteMaterial;
            
        this.DelayedAction(0.2f, () => SetMaterial(mat));;

        _newColorKey = colorKey;

        if (events != null) events.Invoke("on-spot-color-set");
        GlobalEvent.InvokeGlobal("on-any-spot-color-set");

        foreach (var mr in meshRenderers)
            TweenAnims.ObjectPulse(mr.meshRenderer.transform);
    }

    public void SaveColor()
    {
        _colorKey = _newColorKey;

        RoomManager.Active.OnRoomChange();

        if (events != null) events.Invoke("on-spot-color-save");
        GlobalEvent.InvokeGlobal("on-any-spot-color-save");

        foreach (var mr in meshRenderers)
            TweenAnims.ObjectPulseOut(mr.meshRenderer.transform);
    }

    public void CancelColor()
    {
        if (_newColorKey != _colorKey) SetColor(_colorKey);

        if (events != null) events.Invoke("on-spot-color-cancel");
        GlobalEvent.InvokeGlobal("on-any-spot-color-cancel");
    }

    public ColorManager.ColorKey GetColor()
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

    public void SetColorSpotButton(RoomColorSpotButton btn)
    {
        _button = btn;

        _button.gameObject.SetActive(gameObject.activeSelf);
    }

    private void OnEnable()
    {
        if (_button) _button.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        if (_button) _button.gameObject.SetActive(false);
    }
}
