using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    //saving
    //last puzzle combination
    //last puzzle id

    [Serializable] private class Level
    {
        [Serializable] public struct ColorKeyArray
        {
            public ColorManager.ColorKey[] colors;
        }

        [Serializable] public struct BoolArray
        {
            public bool[] bools;
        }

        public ColorKeyArray[] bottles;

        public int coins = 0;

        public BoolArray[] hidens;

        //drugie prikoli
    }

    [SerializeField] private Level[] levels;

    private int _levelId;
    //private int _levelId;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("puzzle-level-id")) PlayerPrefs.SetInt("puzzle-level-id", 0);
    }

    private void Start()
    {
        OpenPuzzle(PlayerPrefs.GetInt("puzzle-level-id"));
    }

    private void OpenPuzzle(int id)
    {
        //delete last trash

        _levelId = id;
    }

    //next level ?
}
