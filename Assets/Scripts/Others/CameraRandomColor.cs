using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraRandomColor : MonoBehaviour
{
    [SerializeField] private Color[] colors = { Color.white };

    private Camera _c;

    private void Awake()
    {
        _c = GetComponent<Camera>();
    }

    private void Start()
    {
        Change();
    }

    public void Change()
    {
        var rand = Random.Range(0, colors.Length);

        _c.backgroundColor = colors[rand];
    }
}
