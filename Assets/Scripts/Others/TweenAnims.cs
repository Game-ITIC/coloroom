using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class TweenAnims
{
    private class TweenAnimObject
    {
        public TweenAnimObject(int newId, Vector3 newScale)
        {
            id = newId;
            scale = newScale;
        }

        public int id;
        public Vector3 scale;
    }

    private static List<TweenAnimObject> _objects = new List<TweenAnimObject>();

    private static TweenAnimObject GetObject(Transform t)
    {
        var id = t.GetInstanceID();

        foreach (var o in _objects)
            if (o.id == id)
                return o;

        _objects.Add(new TweenAnimObject(id, t.localScale));
        return _objects[_objects.Count - 1];
    }

    public static void ObjectPulse(Transform t)
    {
        var ts = GetObject(t).scale;

        DOVirtual.Float(1f, 0.9f, 0.2f, (value) => { t.localScale = ts * value; }).SetEase(Ease.InQuad).OnComplete(() => DOVirtual.Float(0.9f, 1f, 0.3f, (value) => { t.localScale = ts * value; }).SetEase(Ease.OutQuad)).Play();
    }

    public static void ObjectPulseOut(Transform t)
    {
        var ts = GetObject(t).scale;

        DOVirtual.Float(1f, 1.1f, 0.4f, (value) => { t.localScale = ts * value; }).SetEase(Ease.InQuad).OnComplete(() => DOVirtual.Float(1.1f, 1f, 0.6f, (value) => { t.localScale = ts * value; }).SetEase(Ease.OutQuad)).Play();
    }
}
