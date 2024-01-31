using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

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
        Instance = this;

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
        //position them smart
    }

    private void SaveLevel()
    {
        //read level data from bottles

        PlayerPrefs.SetInt("puzzle-level-id", _levelId);

        PlayerPrefs.SetString("puzzle-level-data", JsonUtility.ToJson(_levelData));
    }

    //next level ?

    public void AfterBottlePourIn(PuzzleBottle bottle)
    {
        //checks
        //CheckComplete
        //proverit pobeda - polniy li odin tsvet ili v drugix ne ostalos etogo tsveta 

        //dobavit nov tsveta v palette push

        //bottle events
    }
}
