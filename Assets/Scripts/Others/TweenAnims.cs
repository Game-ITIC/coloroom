using DG.Tweening;
using UnityEngine;

public class TweenAnims
{
    public static void ObjectPulse(Transform t)
    {
        var ts = t.localScale;
        //DOVirtual.Float(1f, 0.8f, 0.2f, (value) => { t.localScale = ts * value; }).SetEase(Ease.InCubic).OnComplete(() => DOVirtual.Float(0.8f, 1f, 0.2f, (value) => { t.localScale = ts * value; }).SetEase(Ease.OutCubic)).Play();
        DOVirtual.Float(1f, 0.9f, 0.2f, (value) => { t.localScale = ts * value; }).SetEase(Ease.InQuad).OnComplete(() => DOVirtual.Float(0.9f, 1f, 0.3f, (value) => { t.localScale = ts * value; }).SetEase(Ease.OutQuad)).Play();
        //DOVirtual.Float(0.8f, 1f, 0.4f, (value) => { t.localScale = ts * value; }).SetEase(Ease.OutQuad).Play();
    }
}
