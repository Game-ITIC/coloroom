using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBottle : MonoBehaviour
{
    [SerializeField] private ColorManager.ColorKey[] stack = new ColorManager.ColorKey[0];

    //draw

    //on finished

    public bool DripTo(PuzzleBottle other)
    {
        return false;
    }
}
