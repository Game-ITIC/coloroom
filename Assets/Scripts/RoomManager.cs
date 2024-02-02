using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using static UnityEditor.Progress;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Active;

    [Serializable]
    private class Room
    {
        public ColorManager.ColorKey[] spotColors = {};
    }

    private Room room;
    
    private RoomColorSpot[] _spots;
    private float _progress = 0f;

    private void Awake()
    {
        _spots = GetComponentsInChildren<RoomColorSpot>(true);

        room = new Room();
    }

    private void Start()
    {
        LoadRoom();
    }

    private void LoadRoom()
    {
        var r = JsonUtility.FromJson<Room>(PlayerPrefs.GetString("my-room-data-" + name));
        if (r != null) room = r;

        for (int i = 0; i < room.spotColors.Length; i++)
        {
            if (i >= _spots.Length) break;

            _spots[i].SetInitColor(room.spotColors[i]);
        }

        UpdateProgressValue();
        UpdateProgressbar();

        Active = this;

        RoomColorSpotButtonManager.Instance.Generate(_spots);

        GlobalEvent.InvokeGlobal("on-room-load");
    }

    private void SaveRoom()
    {
        room.spotColors = new ColorManager.ColorKey[_spots.Length];
        for (int i = 0; i < _spots.Length; i++)
            room.spotColors[i] = _spots[i].GetColor();

        var rs = JsonUtility.ToJson(room);
        PlayerPrefs.SetString("my-room-data-" + name, rs);
    }

    private void UpdateProgressValue()
    {
        int counterMax = _spots.Length;
        int counter = 0;

        foreach (var s in _spots)
            if (s.GetColor() != ColorManager.ColorKey.none) counter++;

        _progress = 1f * counter / counterMax;
    }

    private void UpdateProgressbar()
    {
        this.DelayedAction(1f, () => Progressbar.Main.SetValue(_progress));
    }

    private void CheckFinish()
    {
        if (_progress < 1f) return;

        PlayerPrefs.DeleteKey("my-room-data-" + name);

        this.DelayedAction(2f, () => GlobalEvent.InvokeGlobal("on-room-finish"));
    }

    public void OnRoomChange()
    {
        UpdateProgressValue();
        UpdateProgressbar();

        SaveRoom();

        CheckFinish();
    }
}
