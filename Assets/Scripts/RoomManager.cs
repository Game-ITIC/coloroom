using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Active;

    [SerializeField] string roomId = "room-1";
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
        public ColorManager.ColorKey[] spotColors = {};
    }

    private Room room;
    
    private RoomColorSpot[] _spots;

    private void Awake()
    {
        _spots = GetComponentsInChildren<RoomColorSpot>(true);

        room = new Room();
        room.isOpen = isOpen;
    }

    private void Start()
    {
        LoadRoom();

        StartRoom();/////////////nado vizivat i kameru dvigat na nego
    }

    private void LoadRoom()
    {
        var r = JsonUtility.FromJson<Room>(PlayerPrefs.GetString("my-room-data-" + roomId));
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

            _spots[i].SetInitColor(room.spotColors[i]);
        }

        if (events != null) events.Invoke("on-room-load");

        UpdateProgressbar();
    }

    private void SaveRoom()
    {
        room.spotColors = new ColorManager.ColorKey[_spots.Length];
        for (int i = 0; i < _spots.Length; i++)
            room.spotColors[i] = _spots[i].GetColor();

        var rs = JsonUtility.ToJson(room);
        PlayerPrefs.SetString("my-room-data-" + roomId, rs);
        

        //save room opened
        //save spots colors

        //colorSpots[0].gameObject.GetUPID();
    }

    private void StartRoom()
    {
        Active = this;

        CreateSpotButtons();
    }

    private void CreateSpotButtons()
    {
        //delete olds check?

        if (!room.isOpen) return;

        foreach (RoomColorSpot spot in _spots)
        {
            var sb = Instantiate(spotButtonPrefab, spotButtonParent);
            sb.SetColorSpot(spot);
            spot.SetColorSpotButton(sb);

            sb.GetComponent<UIElementOnObject>().SetTarget(spot.transform);
        }
    }

    private void UpdateProgressbar()
    {
        int counterMax = _spots.Length;
        int counter = 0;

        foreach (var s in _spots)
            Debug.Log(s.GetColor()) ;

        foreach (var s in _spots)
            if (s.GetColor() != ColorManager.ColorKey.none) counter++;

        float val = 1f * counter / counterMax;

        this.DelayedAction(1f, () => Progressbar.Main.SetValue(val));
    }

    public void OnRoomChange()
    {
        UpdateProgressbar();

        SaveRoom();

        //check win level
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
