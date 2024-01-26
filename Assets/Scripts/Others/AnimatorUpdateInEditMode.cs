using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Animator))]
public class AnimatorUpdateInEditMode : MonoBehaviour
{
#if UNITY_EDITOR

    private Animator _a;
    private bool _update = false;

    private void Awake()
    {
        _a = GetComponent<Animator>();
    }

    void Update()
    {
        if (!_update) return;
        _update = false;

        _a.Update(Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        if (Application.isPlaying) return;

        UnityEditor.EditorApplication.QueuePlayerLoopUpdate();
        UnityEditor.SceneView.RepaintAll();

        _update = true;
    }

#endif
}
