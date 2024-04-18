using UnityEngine;
using Cinemachine;

public class CinemachineMaterialChanger : MonoBehaviour
{
    public int seconds;
    public Material[] mat;
    //private Renderer rend;
    private Renderer[] rends;
    void Start()
    {
        //mat = GetComponent<Material>();
        //rend = GetComponent<Renderer>();
        rends = GetComponentsInChildren<Renderer>();
        Invoke("ChangeMat", seconds);
    }

    void ChangeMat()
    {
        //rend.material = mat;
        for (int i = 0; i < rends.Length; i++)
        {
            rends[i].materials = mat;
        }
    }
}