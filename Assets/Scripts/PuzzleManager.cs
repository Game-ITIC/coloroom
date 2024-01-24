using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
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
    [Space]
    //prefabs..

    private int _levelId;
    private Level _levelData;
    private PuzzleBottle _bottles;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("puzzle-level-id")) PlayerPrefs.SetInt("puzzle-level-id", 0);
    }

    private void Start()
    {
        OpenLevel(PlayerPrefs.GetInt("puzzle-level-id"));
    }

    private void OpenLevel(int id)
    {
        //delete last trash

        _levelId = id;

        _levelData = JsonUtility.FromJson<Level>(PlayerPrefs.GetString("puzzle-level-data"));
        if (_levelData == null) _levelData = levels[id];

        //generate bottles
    }

    private void SaveLevel()
    {
        //read level data from bottles

        PlayerPrefs.SetInt("puzzle-level-id", _levelId);

        PlayerPrefs.SetString("puzzle-level-data", JsonUtility.ToJson(_levelData));
    }

    //next level ?
}
