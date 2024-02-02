using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Serializable]
    public class Level
    {
        public GameObject levelPrefab;
        public int bottles=1, coins;
    }

    [SerializeField] private Level[] levels = new Level[0];

    private int _levelId = 0;
    private GameObject _levelObject = null;
    private bool levelComp = false;
    AudioSource aud;
    public GameObject winWindow;

    private void Awake()
    {
        instance = this;

        if (!PlayerPrefs.HasKey("level-id")) PlayerPrefs.SetInt("level-id", _levelId);

        if (!PlayerPrefs.HasKey("home-tutorial-watched")) PlayerPrefs.SetInt("home-tutorial-watched", 0);
    }

    private void Start()
    {
        aud = GetComponent<AudioSource>();
        LevelStart(PlayerPrefs.GetInt("level-id"));
    }

    public void CheckWin()
    {
        if (GameObject.FindGameObjectsWithTag("finBottle").Length == levels[_levelId].bottles)
        {
            if (!levelComp)
            {
                Debug.Log(GameObject.FindGameObjectsWithTag("finBottle").Length);
                foreach (GameObject obj in GameObject.FindGameObjectsWithTag("finBottle"))
                {
                    Debug.Log(obj);
                }
                levelComp = true;
                Invoke("LevelWin", 0.5f);
                //open view with button 
                //add coins and colors
                //on button - next level
            }
        }
    }

    private void LevelWin()
    {
        aud.Play();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("bottle"))
        {
            obj.GetComponent<Collider>().enabled = false;
        }
        winWindow.SetActive(true);
        //Invoke("LevelNext", 3.0f);
    }

    private void LevelStart(int id)
    {
        winWindow.SetActive(false);
        levelComp = false;
        if (_levelObject != null) Destroy(_levelObject);

        _levelId = id;

        _levelObject = Instantiate(levels[_levelId].levelPrefab, null);

        //GlobalEvent.InvokeGlobal("on-level-start");
    }

    public void LevelRestart()
    {
        LevelStart(_levelId);
    }

    public void LevelNext()
    {
        int nextLevel = (_levelId + 1) % levels.Length;

        LevelStart(nextLevel);
    }

    public void SaveLevel()
    {
        int nextLevel = (_levelId + 1) % levels.Length;

        PlayerPrefs.SetInt("level-id", nextLevel);
    }

    public void HomeScene()
    {
        if (PlayerPrefs.GetInt("home-tutorial-watched") == 0)
        {
            PlayerPrefs.SetInt("home-tutorial-watched", 1);

            SceneManager.LoadScene(2, LoadSceneMode.Single);

            return;
        }

        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
