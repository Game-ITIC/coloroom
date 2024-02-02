using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomSceneManager : MonoBehaviour
{
    [SerializeField] private RoomManager[] roomPrefabs;
    [SerializeField] private TextMeshProUGUI title;

    private int _levelId = 0;
    private RoomManager _levelObject;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("room-level-id")) PlayerPrefs.SetInt("room-level-id", 0);
    }

    private void Start()
    {
        OpenLevel(PlayerPrefs.GetInt("room-level-id"));
    }

    private void OpenLevel(int id)
    {
        if (_levelObject != null)
            Destroy(_levelObject);

        _levelId = id;

        if (title != null) title.text = "Room " + (_levelId + 1);

        _levelObject = Instantiate(roomPrefabs[_levelId]);

        GlobalEvent.InvokeGlobal("on-level-open");
    }

    public void NextLevel()
    {
        int nextId = (_levelId + 1) % roomPrefabs.Length;

        OpenLevel(nextId);
    }
}
