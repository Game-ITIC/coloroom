using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickmanPoser : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public enum Pose { sit, dance, eat }

    [SerializeField] private Pose pose;

    private void Awake()
    {
        SetPose();
    }

    private void SetPose()
    {
        if (animator != null) animator.SetInteger("pose", (int)pose);
    }

    private void OnValidate()
    {
        Debug.Log("pose = " + (int)pose);

        SetPose();
    }
}
