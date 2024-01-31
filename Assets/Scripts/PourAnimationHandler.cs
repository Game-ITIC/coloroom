using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourAnimationHandler : MonoBehaviour
{
    public BottleController bc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPourAnimationComplete(GameObject go)
    {
        bc.OnPourAnimationComplete(gameObject);
    }
}
