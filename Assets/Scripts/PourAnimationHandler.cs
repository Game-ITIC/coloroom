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
        //bc.transform.position = new Vector3(bc.transform.position.x, bc.transform.position.y + 5.0f, bc.transform.position.z);
        Debug.Log("curr pos: " + bc.transform.position);
        Debug.Log("orig pos: " + bc.originalPosition);
        Debug.Log("aboba pos: " + transform.position);
        bc.transform.position = bc.originalPosition;
        bc.OnPourAnimationComplete(gameObject);
        bc.GoBackPos(bc.transform.position);
    }
}
