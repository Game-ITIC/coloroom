using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiCinemachine : MonoBehaviour
{
    void Start()
    {
        Invoke("FireworksStart", 28.0f);
    }

    void FireworksStart()
    {
        Debug.Log("boom");
        gameObject.SetActive(true);
    }
}
