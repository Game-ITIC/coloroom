using System.Collections;
using System.Collections.Generic;
using Itic.Ad;
using TMPro;
using UnityEngine;

public class RoomSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject _videoRewardButton;
    [SerializeField] private RoomManager[] roomPrefabs;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI title2;
    [SerializeField] private TextMeshProUGUI title3;

    private int _levelId = 0;
    private RoomManager _levelObject;

    private bool _isRewardGot;
    AudioSource aud;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("room-level-id")) PlayerPrefs.SetInt("room-level-id", 0);
    }

    private void Start()
    {
        OpenLevel(PlayerPrefs.GetInt("room-level-id"));
        aud = GetComponent<AudioSource>();
    }

    private void OpenLevel(int id)
    {
        if (_levelObject != null)
            Destroy(_levelObject.gameObject);

        _isRewardGot = false;
        _levelId = id;

        SetTitles();
        
        _levelObject = Instantiate(roomPrefabs[_levelId]);
        
        AdManager.Instance.LoadRewardedAd();
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
        aud.Play();
        
        _videoRewardButton.SetActive(AdManager.Instance.HasRewardVideo);
        
        PlayerPrefs.SetInt("room-level-id", nextId);
    }

    public void ShowRewardVideo()
    {
        AdManager.Instance.ShowRewardedAd(AddCoinAndGoNext);
    }

    private void AddCoinAndGoNext(bool isRewardGot)
    {
        if (isRewardGot)
        {
            CoinManager.Instance.AddCoins(100);
        }

        _isRewardGot = isRewardGot;
        NextLevel();
    }
    
    public void NextLevel()
    {
        int nextId = (_levelId + 1) % roomPrefabs.Length;

        if (!_isRewardGot)
        {
            AdManager.Instance.ShowInterstitialAd();
        }
        
        OpenLevel(nextId);
    }
}
