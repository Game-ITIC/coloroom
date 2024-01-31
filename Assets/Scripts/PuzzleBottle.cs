using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBottle : MonoBehaviour
{
    private const int size = 4;

    [SerializeField] private Stack<ColorManager.ColorKey> colorStack = new Stack<ColorManager.ColorKey>();
    [Space]
    public GlobalEvent events;

    //private Vector3 _pos

    //draw

    public bool PourOut(PuzzleBottle intoOther)
    {
        //anim

        return false;
    }

    public bool PourIn(ColorManager.ColorKey color)
    {
        //anim

        PuzzleManager.Instance.AfterBottlePourIn(this);

        return false;
    }
}
