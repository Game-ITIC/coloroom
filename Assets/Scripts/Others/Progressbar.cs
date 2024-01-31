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

    private void Awake()
    {
        if (main) Main = this;
    }

    public void SetValue(float val)
    {
        //
        //anim
        rect.localScale = new Vector3(val, 1f, 1f);

        events.Invoke("on-value-change");
    }
}
