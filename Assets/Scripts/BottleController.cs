using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class BottleController : MonoBehaviour
{
    GameObject chunk3, chunk2, chunk1, chunk0;
    Renderer color3, color2, color1, color0;

    private GameObject target;
    static BottleController selected;
    bool isFinished=false, isFull, isEmpty, isBlocked=false;
    bool lastActiveIs3, lastActiveIs2, lastActiveIs1;



    // Start is called before the first frame update
    void Start()
    {
        chunk3 = transform.GetChild(3).gameObject;
        chunk2 = transform.GetChild(2).gameObject;
        chunk1 = transform.GetChild(1).gameObject;
        chunk0 = transform.GetChild(0).gameObject;

        color3 = chunk3.GetComponent<Renderer>();
        color2 = chunk2.GetComponent<Renderer>();
        color1 = chunk1.GetComponent<Renderer>();
        color0 = chunk0.GetComponent<Renderer>();



        //check if bottle is finished
        if (chunk3.activeSelf && chunk2.activeSelf && chunk1.activeSelf && chunk0.activeSelf)
        {
            isFull = true;

            if(color0.sharedMaterial.name == color1.sharedMaterial.name && color1.sharedMaterial.name == color2.sharedMaterial.name && color2.sharedMaterial.name == color3.sharedMaterial.name)
            {
                Debug.Log("banka full i same");
                isFinished = true;
            }
            else
            {
                Debug.Log("banka full i not same");
            }
        }
        else
        {
            Debug.Log("banka not full");
        }


        //check if materials are same
        if (color1.sharedMaterial.name == color2.sharedMaterial.name)
            {
                Debug.Log("Object1 and Object2 are same.");
            }
            else
            {
                Debug.Log("Object1 and Object2 are lohs.");
            }

       
    }

    // Update is called once per frame
    void Update()
    {
        //check last active child
        if (!chunk3.activeSelf)
        {
            isEmpty = true;
            Debug.Log("empty");
        }
        else if (!chunk2.activeSelf)
        {
            lastActiveIs1 = false;
            lastActiveIs2 = false;
            lastActiveIs3 = true;
            Debug.Log("last active is 3");
        }
        else if (!chunk1.activeSelf)
        {
            lastActiveIs1 = false;
            lastActiveIs2 = true;
            lastActiveIs3 = false;
            Debug.Log("last active is 2");
        }
        else if (!chunk0.activeSelf)
        {
            lastActiveIs1 = true;
            lastActiveIs2 = false;
            lastActiveIs3 = false;
            Debug.Log("last active is 1");
        }
        else
        {
            Debug.Log("full");
        }

    }

    private void OnMouseDown()
    {
        //if bottle not finished, lift the bottle and select
        if (!isFinished && selected==null)
        {
            selected = this;
            transform.position = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);
            Debug.Log("ya vibral");
        }
        else if (selected != null)
        {
            Debug.Log("chto-to proishodit");
            //checking conditions of colors and chunks
            if (isFull)
            {
                isBlocked = true;
                Debug.Log("Debil, kuda lyosh!!!");
            }
            else
            {
                Debug.Log("Molodez, nalil");
            }
            //selected moves towards target and plays animation 
            //change color of the target
            selected.transform.position = new Vector3(selected.transform.position.x, selected.transform.position.y - 1.0f, selected.transform.position.z);
            selected = null;
        }

    }
}
