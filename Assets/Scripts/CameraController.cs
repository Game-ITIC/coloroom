using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform cameraTarget;
    //[SerializeField] private Transform camera;
    [Space]
    [SerializeField] private float rotateFactor = 45f;
    [SerializeField] private Vector2 rotateXLimits = new Vector2(0f, 90f);
    [SerializeField] private Vector2 rotateYLimits = new Vector2(-90f, 0f);

    private Camera _c;
    
    private bool _allowRotate = false;
    private Vector3 _ctAngles;

    private void Awake()
    {
        _c = Camera.main;

        _ctAngles = cameraTarget.localEulerAngles;
    }

    private void Update()
    {
        if (Input.touchCount == 0) return;

        var t = Input.GetTouch(0);

        switch (t.phase)
        {
            case TouchPhase.Began:
                if (CheckUITouch.IsPointerOverUIObject()) break;

                _allowRotate = true;
                break;
            case TouchPhase.Moved:
                if (!_allowRotate) break;

                var deltaPos = t.deltaPosition;
                var delta = new Vector2(deltaPos.x / Screen.width, deltaPos.y / Screen.height);

                var eulerAngles = _ctAngles + new Vector3(-delta.y, delta.x, 0f) * rotateFactor;
                eulerAngles.x = Mathf.Clamp(eulerAngles.x, rotateXLimits[0], rotateXLimits[1]); 
                eulerAngles.y = Mathf.Clamp(eulerAngles.y, rotateYLimits[0], rotateYLimits[1]);
                _ctAngles = eulerAngles;

                cameraTarget.localEulerAngles = _ctAngles;
                break;
            case TouchPhase.Ended:
                _allowRotate = false;
                break;
        }
    }
}
