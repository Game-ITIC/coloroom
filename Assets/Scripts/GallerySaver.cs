using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Recorder.OutputPath;
using System;
using System.IO;
using Unity.VisualScripting;
using static RoomManager;

public class GallerySaver : MonoBehaviour
{
    private List<Room> galleryRooms = new List<Room>();
    public class Gallery
    {
        //prefab of the room
        public string roomName;
        //json of materials
        public ColorManager.ColorKey[] spotColors;
    }

    public Gallery gallery;
    private int counter = 0;

    // Load gallery rooms from JSON file
    public void LoadGallery()
    {
        var g = JsonUtility.FromJson<Gallery>(PlayerPrefs.GetString("gallery"));
        if (g != null) gallery = g;
        galleryRooms = new List<Room>(JsonUtility.FromJson<List<Room>>(PlayerPrefs.GetString("gallery")));
    }

    // Save gallery rooms to JSON file
    public void SaveGallery()
    {
        var rs = JsonUtility.ToJson(gallery);
        PlayerPrefs.SetString("gallery", rs);
        //PlayerPrefs.SetString("my-gallery-data-" + counter, rs);
        //counter += 1;
    }

    // Add current room to gallery before deleting key
    public void AddRoomToGallery(Room room)
    {
        galleryRooms.Add(room);
        SaveGallery();
    }
}
