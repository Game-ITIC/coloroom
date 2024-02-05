using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomSceneManager : MonoBehaviour
{
    [SerializeField] private RoomManager[] roomPrefabs;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI title2;
    [SerializeField] private TextMeshProUGUI title3;

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
            Destroy(_levelObject.gameObject);

        _levelId = id;

        SetTitles();

        _levelObject = Instantiate(roomPrefabs[_levelId]);
    }

    private void SetTitles()
    {
        if (title != null) title.text = roomPrefabs[_levelId].GetRoomName();

        int nextId = (_levelId + 1) % roomPrefabs.Length;
        if (title2 != null) title2.text = roomPrefabs[nextId].GetRoomName();

        nextId = (nextId + 1) % roomPrefabs.Length;
        if (title3 != null) title3.text = roomPrefabs[nextId].GetRoomName();
    }

    public void OnLevelFinish()
    {
        int nextId = (_levelId + 1) % roomPrefabs.Length;

        PlayerPrefs.SetInt("room-level-id", nextId);
    }

    public void NextLevel()
    {
        int nextId = (_levelId + 1) % roomPrefabs.Length;

        OpenLevel(nextId);
    }
}
