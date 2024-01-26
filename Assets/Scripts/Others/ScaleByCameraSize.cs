using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ScaleByCameraSize : MonoBehaviour
{
    [SerializeField] private float scaleFactor = 1f;

    private Camera _c;
    private Transform _t;
    private float _camSize;

    private void Awake()
    {
        _c = Camera.main;

        _t = transform;
    }

    private void Update()
    {
        //if (_c.orthographicSize == _camSize) return;
        
        _camSize = _c.orthographicSize;

        _t.localScale = Vector3.one * _camSize * scaleFactor;
    }
}
