using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] bool isOpen = false;
    [Space]
    [SerializeField] private RoomColorSpotButton spotButtonPrefab;
    [SerializeField] private RectTransform spotButtonParent;
    [Space]
    [SerializeField] private GlobalEvent events;

    private RoomColorSpot[] spots;

    private void Awake()
    {
        spots = GetComponentsInChildren<RoomColorSpot>(false);
    }

    private void Start()
    {
        /*if (!isOpen)
        {
            events.Invoke("on-room-lock");
            return;
        }*/

        CreateSpotButtons();

        Load();
    }

    private void Load()
    {
        //get saved colors

        if (events != null) events.Invoke("on-room-load");
    }

    private void Save()
    {
        //save room opened
        //save spots colors

        //colorSpots[0].gameObject.GetUPID();
    }

    private void CreateSpotButtons()
    {
        foreach (RoomColorSpot spot in spots)
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
