using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class RoomManager : MonoBehaviour
{
    [SerializeField] bool isOpen = false;
    [Space]
    [SerializeField] private RoomColorSpotButton spotButtonPrefab;
    [SerializeField] private RectTransform spotButtonParent;
    [Space]
    [SerializeField] private GlobalEvent events;

    [Serializable]
    private class Room
    {
        public bool isOpen = false;
        public ColorManager.ColorKey?[] spotColors = {};
    }

    private Room room;
    
    private RoomColorSpot[] _spots;

    private void Awake()
    {
        _spots = GetComponentsInChildren<RoomColorSpot>(false);

        room = new Room();
        room.isOpen = isOpen;
    }

    private void Start()
    {
        CreateSpotButtons();

        LoadRoom();
    }

    private void LoadRoom()
    {
        var r = JsonUtility.FromJson<Room>(PlayerPrefs.GetString("my-room-data-" + gameObject.GetUPID()));
        if (r != null) room = r;

        if (!room.isOpen)
        {
            if (events != null) events.Invoke("on-room-lock");
            GlobalEvent.InvokeGlobal("on-any-room-lock");

            return;
        }

        for (int i = 0; i < room.spotColors.Length; i++)
        {
            if (i >= _spots.Length) break;

            _spots[i].SetColor(room.spotColors[i]);
        }

        if (events != null) events.Invoke("on-room-load");
    }

    private void SaveRoom()
    {
        //save room opened
        //save spots colors

        //colorSpots[0].gameObject.GetUPID();
    }

    private void CreateSpotButtons()
    {
        //delete olds check?

        if (!room.isOpen) return;

        foreach (RoomColorSpot spot in _spots)
        {
            var sb = Instantiate(spotButtonPrefab, spotButtonParent);
            sb.SetColorSpot(spot);

            sb.GetComponent<UIElementOnObject>().SetTarget(spot.transform);
        }
    }

    //update progressbar

    /*private void OnFinish()
    {
        events.Invoke("on-room-finish");
    }*/

    /*public void Open()
    {
        isOpen = true;

        events.Invoke("on-room-open");

        Load();

        Save();
    }*/
}
