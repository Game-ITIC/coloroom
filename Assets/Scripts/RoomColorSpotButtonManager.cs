using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Recorder.OutputPath;

public class RoomColorSpotButtonManager : MonoBehaviour
{
    public static RoomColorSpotButtonManager Instance;

    [SerializeField] private RoomColorSpotButton spotButtonPrefab;
    [SerializeField] private RectTransform spotButtonParent;

    private void Awake()
    {
        Instance = this;
    }

    public void Generate(RoomColorSpot[] spots)
    {
        for (int i = 0; i < spotButtonParent.childCount; i++)
            Destroy(spotButtonParent.GetChild(i).gameObject);

        foreach (RoomColorSpot spot in spots)
        {
            var sb = Instantiate(spotButtonPrefab, spotButtonParent);
            sb.SetColorSpot(spot);
            spot.SetColorSpotButton(sb);

            sb.GetComponent<UIElementOnObject>().SetTarget(spot.transform);
        }
    }
}
