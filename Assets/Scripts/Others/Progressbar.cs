using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progressbar : MonoBehaviour
{
    public static Progressbar Main;

    [SerializeField] private bool main = false;
    [Space]
    [SerializeField] private RectTransform rect;
    [Space]
    [SerializeField] private GlobalEvent events;

    private float _value;

    private void Awake()
    {
        if (main) Main = this;
    }

    public void SetValue(float val)
    {
        if (val == _value) return;

        _value = val;
        
        DOVirtual.Float(rect.localScale.x, _value, 1f, (x) => { rect.localScale = new Vector3(x, 1f, 1f); }).SetEase(Ease.OutSine).Play();

        if (events != null) events.Invoke("on-value-change");
    }
}
