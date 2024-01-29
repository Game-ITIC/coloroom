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
    public Transform[] chunksArray;
    bool isFinished=false, isFull, isEmpty, isBlocked, isMatching;
    bool lastActiveIs3, lastActiveIs2, lastActiveIs1;
    int lastActiveIndex;


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
    }


    void Update()
    {
        //array of chunks (children) of each bottle
        if (gameObject != null)
        {
            chunksArray = gameObject.GetComponentsInChildren<Transform>(true);
        }

        //check if bottle is finished
        if (chunk3.activeSelf && chunk2.activeSelf && chunk1.activeSelf && chunk0.activeSelf)
        {
            isFull = true;

            if (color0.sharedMaterial.color == color1.sharedMaterial.color && color1.sharedMaterial.color == color2.sharedMaterial.color && color2.sharedMaterial.color == color3.sharedMaterial.color)
            {
                isFinished = true;
                chunksArray[5].gameObject.SetActive(true);
            }
            else
            {
                isFinished = false;
            }
        }
        else
        {
            isFull = false;
            isFinished = false;
        }

        //last active chunk
        if (!chunksArray[4].gameObject.activeSelf)
        {
            isEmpty = true;
            isFull = false;
        }
        else if (!chunksArray[3].gameObject.activeSelf)
        {
            lastActiveIndex = 4;
            isEmpty = false;
            isFull = false;
        }
        else if (!chunksArray[2].gameObject.activeSelf)
        {
            lastActiveIndex = 3;
            isEmpty = false;
            isFull = false;
        }
        else if (!chunksArray[1].gameObject.activeSelf)
        {
            lastActiveIndex = 2;
            isEmpty = false;
            isFull = false;
        }
        else
        {
            lastActiveIndex = 1;
            isFull = true;
            isEmpty = false;
        }
       

        //check if two consequent materials are same
        if (color1.sharedMaterial.color == color2.sharedMaterial.color)
        {
            //Debug.Log("Object1 and Object2 are same.");
        }
        else
        {
            //Debug.Log("Object1 and Object2 are lohs.");
        }
    }

    private void OnMouseDown()
    {
        //if bottle not finished, lift the bottle and select
        if (!isFinished && selected==null)
        {
            selected = this;
            transform.position = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);
        }
        else if (selected == this)
        {
            selected.transform.position = new Vector3(selected.transform.position.x, selected.transform.position.y - 1.0f, selected.transform.position.z);
            selected = null;
        }
        else 
        {
            //checking conditions of colors and chunks
            if (isFull)
            {
                isBlocked = true;
                Debug.Log("Debil, kuda lyosh!!!");
            }
            else if (isEmpty)
            {
                chunksArray[4].gameObject.SetActive(true);
                chunksArray[4].GetComponent<Renderer>().material = selected.chunksArray[selected.lastActiveIndex].GetComponent<Renderer>().material;
                selected.chunksArray[selected.lastActiveIndex].gameObject.SetActive(false);

                if(selected.chunksArray[selected.lastActiveIndex+1].GetComponent<Renderer>().sharedMaterial.color == selected.chunksArray[selected.lastActiveIndex].GetComponent<Renderer>().sharedMaterial.color)
                {
                    chunksArray[3].gameObject.SetActive(true);
                    chunksArray[3].GetComponent<Renderer>().material = selected.chunksArray[selected.lastActiveIndex].GetComponent<Renderer>().material;
                    selected.chunksArray[selected.lastActiveIndex+1].gameObject.SetActive(false);
                }
                if (selected.chunksArray[selected.lastActiveIndex + 2].GetComponent<Renderer>().sharedMaterial.color == selected.chunksArray[selected.lastActiveIndex].GetComponent<Renderer>().sharedMaterial.color)
                {
                    chunksArray[2].gameObject.SetActive(true);
                    chunksArray[2].GetComponent<Renderer>().material = selected.chunksArray[selected.lastActiveIndex].GetComponent<Renderer>().material;
                    selected.chunksArray[selected.lastActiveIndex + 2].gameObject.SetActive(false);
                }

                isEmpty = false;
            }
            else
            {             
                if (chunksArray[lastActiveIndex].GetComponent<Renderer>().sharedMaterial.color == selected.chunksArray[selected.lastActiveIndex].GetComponent<Renderer>().sharedMaterial.color)
                {
                    chunksArray[lastActiveIndex - 1].gameObject.SetActive(true);
                    chunksArray[lastActiveIndex - 1].GetComponent<Renderer>().material = selected.chunksArray[selected.lastActiveIndex].GetComponent<Renderer>().material;
                    selected.chunksArray[selected.lastActiveIndex].gameObject.SetActive(false);

                    if (selected.chunksArray[selected.lastActiveIndex + 1].GetComponent<Renderer>().sharedMaterial.color == selected.chunksArray[selected.lastActiveIndex].GetComponent<Renderer>().sharedMaterial.color)
                    {
                        if (chunksArray[lastActiveIndex - 2].gameObject.CompareTag("color"))
                        {
                            chunksArray[lastActiveIndex - 2].gameObject.SetActive(true);
                            chunksArray[lastActiveIndex - 2].GetComponent<Renderer>().material = selected.chunksArray[selected.lastActiveIndex].GetComponent<Renderer>().material;
                            selected.chunksArray[selected.lastActiveIndex + 1].gameObject.SetActive(false);
                        }
                    }
                    if (selected.chunksArray[selected.lastActiveIndex + 2].GetComponent<Renderer>().sharedMaterial.color == selected.chunksArray[selected.lastActiveIndex].GetComponent<Renderer>().sharedMaterial.color)
                    {
                        if (chunksArray[lastActiveIndex - 3].gameObject.CompareTag("color"))
                        {
                            chunksArray[lastActiveIndex - 3].gameObject.SetActive(true);
                            chunksArray[lastActiveIndex - 3].GetComponent<Renderer>().material = selected.chunksArray[selected.lastActiveIndex].GetComponent<Renderer>().material;
                            selected.chunksArray[selected.lastActiveIndex + 2].gameObject.SetActive(false);
                        }
                    }
                }
            }
            selected.transform.position = new Vector3(selected.transform.position.x, selected.transform.position.y - 1.0f, selected.transform.position.z);
            selected = null;
        }

    }
}
