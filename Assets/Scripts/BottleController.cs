using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class BottleController : MonoBehaviour
{
    GameObject chunk3, chunk2, chunk1, chunk0;
    Renderer color3, color2, color1, color0;
    Animator anima;

    private GameObject target;
    static BottleController selected;
    public Transform[] chunksArray;
    bool isFinished=false, isFull, isEmpty, isBlocked, isMatching;
    bool lastActiveIs3, lastActiveIs2, lastActiveIs1;
    int lastActiveIndex;
    float upDock = 1.0f;
    public Vector3 originalPosition;


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

        anima = GetComponent<Animator>();

        originalPosition = transform.position;
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
    }
    private void OnMouseDown()
    {

        //selecting a bottle
        if (!isFinished && selected==null)
        {
            selected = this;
            transform.position = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);
        }
        //if it's the same bottle
        else if (selected == this)
        {
            selected.transform.position = new Vector3(selected.transform.position.x, selected.transform.position.y - 1.0f, selected.transform.position.z);
            selected = null;
        }
        //if it's another available bottle
        else 
        {
            //no space (but not finished)
            if (isFull)
            {
                isBlocked = true;
                selected.anima.SetTrigger("isBlocked");
            } //empty bottle
            else if (isEmpty)
            {
                selected.transform.position = new Vector3(chunksArray[7].transform.position.x, chunksArray[7].transform.position.y + upDock, chunksArray[7].transform.position.z);
                chunksArray[4].gameObject.SetActive(true);
                chunksArray[4].GetComponent<Renderer>().material = selected.chunksArray[selected.lastActiveIndex].GetComponent<Renderer>().material;
                selected.chunksArray[selected.lastActiveIndex].GetComponent<Animator>().SetTrigger("isReducing");
                selected.anima.SetTrigger("isPouring");

                if (selected.chunksArray[selected.lastActiveIndex+1].GetComponent<Renderer>().sharedMaterial.color == selected.chunksArray[selected.lastActiveIndex].GetComponent<Renderer>().sharedMaterial.color)
                {
                        selected.transform.position = new Vector3(chunksArray[7].transform.position.x, chunksArray[7].transform.position.y + upDock, chunksArray[7].transform.position.z);
                        chunksArray[3].gameObject.SetActive(true);
                        chunksArray[3].GetComponent<Renderer>().material = selected.chunksArray[selected.lastActiveIndex].GetComponent<Renderer>().material;
                        selected.chunksArray[selected.lastActiveIndex + 1].GetComponent<Animator>().SetTrigger("isReducing");
                        selected.anima.SetTrigger("isPouring");
                }
                if (selected.chunksArray[selected.lastActiveIndex + 2].GetComponent<Renderer>().sharedMaterial.color == selected.chunksArray[selected.lastActiveIndex].GetComponent<Renderer>().sharedMaterial.color)
                {     
                        selected.transform.position = new Vector3(chunksArray[7].transform.position.x, chunksArray[7].transform.position.y + upDock, chunksArray[7].transform.position.z);
                        chunksArray[2].gameObject.SetActive(true);
                        chunksArray[2].GetComponent<Renderer>().material = selected.chunksArray[selected.lastActiveIndex].GetComponent<Renderer>().material;
                        selected.chunksArray[selected.lastActiveIndex + 2].GetComponent<Animator>().SetTrigger("isReducing");
                        selected.anima.SetTrigger("isPouring");
                }
                isEmpty = false;
            } //pouring (non empty)
            else
            {             
                if (chunksArray[lastActiveIndex].GetComponent<Renderer>().sharedMaterial.color == selected.chunksArray[selected.lastActiveIndex].GetComponent<Renderer>().sharedMaterial.color)
                {
                    selected.transform.position = new Vector3(chunksArray[7].transform.position.x, chunksArray[7].transform.position.y + upDock, chunksArray[7].transform.position.z);
                    chunksArray[lastActiveIndex - 1].gameObject.SetActive(true);
                    chunksArray[lastActiveIndex - 1].GetComponent<Renderer>().material = selected.chunksArray[selected.lastActiveIndex].GetComponent<Renderer>().material;
                    selected.chunksArray[selected.lastActiveIndex].GetComponent<Animator>().SetTrigger("isReducing");
                    selected.anima.SetTrigger("isPouring");

                    if (selected.chunksArray[selected.lastActiveIndex + 1].GetComponent<Renderer>().sharedMaterial.color == selected.chunksArray[selected.lastActiveIndex].GetComponent<Renderer>().sharedMaterial.color)
                    {
                        if (chunksArray[lastActiveIndex - 2].gameObject.CompareTag("color"))
                        {
                            selected.transform.position = new Vector3(chunksArray[7].transform.position.x, chunksArray[7].transform.position.y + upDock, chunksArray[7].transform.position.z);
                            chunksArray[lastActiveIndex - 2].gameObject.SetActive(true);
                            chunksArray[lastActiveIndex - 2].GetComponent<Renderer>().material = selected.chunksArray[selected.lastActiveIndex].GetComponent<Renderer>().material;
                            selected.chunksArray[selected.lastActiveIndex + 1].GetComponent<Animator>().SetTrigger("isReducing");
                            selected.anima.SetTrigger("isPouring");
                        }
                    }
                    if (selected.chunksArray[selected.lastActiveIndex + 2].GetComponent<Renderer>().sharedMaterial.color == selected.chunksArray[selected.lastActiveIndex].GetComponent<Renderer>().sharedMaterial.color)
                    {
                        if (chunksArray[lastActiveIndex - 3].gameObject.CompareTag("color"))
                        {
                            selected.transform.position = new Vector3(chunksArray[7].transform.position.x, chunksArray[7].transform.position.y + upDock, chunksArray[7].transform.position.z);
                            chunksArray[lastActiveIndex - 3].gameObject.SetActive(true);
                            chunksArray[lastActiveIndex - 3].GetComponent<Renderer>().material = selected.chunksArray[selected.lastActiveIndex].GetComponent<Renderer>().material;
                            selected.chunksArray[selected.lastActiveIndex + 2].GetComponent<Animator>().SetTrigger("isReducing");
                            selected.anima.SetTrigger("isPouring");
                        }
                    }
                }
                else
                {
                    selected.anima.SetTrigger("isBlocked");
                }
            }
            selected.transform.position = new Vector3(selected.transform.position.x, selected.transform.position.y - 1.0f, selected.transform.position.z);
            selected = null;
        }

    }

    public void OnPourAnimationComplete(GameObject go)
    {
       go.SetActive(false);
       //selected.transform.position = new Vector3(selected.transform.position.x, selected.transform.position.y + 1.0f, selected.transform.position.z);
       //transform.position = originalPosition;
       //Debug.Log("orig pos: " + originalPosition);
    }

    public void GoBackPos(Vector3 pos)
    {
        transform.position = pos;
    }
}
