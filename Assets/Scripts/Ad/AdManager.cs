using System;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine;

namespace Itic.Ad
{
    public class AdManager : MonoBehaviour
    {
        public static AdManager Instance;
        
        // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
        [SerializeField] private string _adUnitId = "ca-app-pub-4981577085737170/4506162460";//"ca-app-pub-4981577085737170/4506162460";
        [SerializeField] private string _adBannerId = "ca-app-pub-4981577085737170/8972066216";//""ca-app-pub-4981577085737170/8972066216";
#elif UNITY_IPHONE
  private string _adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
  private string _adUnitId = "unused";
#endif

        private BannerView _bannerView;
        private int _indexOfAd;
        
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void Start()
        {
            // Initialize the Google Mobile Ads SDK.
            MobileAds.Initialize(_ =>
            {
                LoadInterstitialAd();
                LoadBannerAd();
            });
        }


        private InterstitialAd _interstitialAd;

        /// <summary>
        /// Loads the interstitial ad.
        /// </summary>
        public void LoadInterstitialAd()
        {
            // Clean up the old ad before loading a new one.
            if (_interstitialAd != null)
            {
                _interstitialAd.Destroy();
                _interstitialAd = null;
            }

            Debug.Log("Loading the interstitial ad.");

            // create our request used to load the ad.
            var adRequest = new AdRequest();

            // send the request to load the ad.
            InterstitialAd.Load(_adUnitId, adRequest,
                (ad, error) =>
                {
                    // if error is not null, the load request failed.
                    if (error != null || ad == null)
                    {
                        Debug.LogError("interstitial ad failed to load an ad " +
                                       "with error : " + error);
                        return;
                    }

                    Debug.Log("Interstitial ad loaded with response : "
                              + ad.GetResponseInfo());

                    _interstitialAd = ad;
                });
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                ShowInterstitialAd();
            }
        }

        /// <summary>
        /// Creates the banner view and loads a banner ad.
        /// </summary>
        public void LoadBannerAd()
        {
            Debug.Log("Load");
            // create an instance of a banner view first.
            if(_bannerView == null)
            {
                CreateBannerView();
            }

            // create our request used to load the ad.
            var adRequest = new AdRequest();

            // send the request to load the ad.
            Debug.Log("Loading banner ad.");
            _bannerView?.LoadAd(adRequest);
        }
        
        /// <summary>
        /// Creates a 320x50 banner view at top of the screen.
        /// </summary>
        public void CreateBannerView()
        {
            Debug.Log("Creating banner view");

            // If we already have a banner, destroy the old one.
            if (_bannerView != null)
            {
                DestroyBannerView();
            }

            // Create a 320x50 banner at top of the screen
            _bannerView = new BannerView(_adBannerId, AdSize.Banner, AdPosition.Bottom);
        }
        
        /// <summary>
        /// Destroys the banner view.
        /// </summary>
        public void DestroyBannerView()
        {
            if (_bannerView != null)
            {
                Debug.Log("Destroying banner view.");
                _bannerView.Destroy();
                _bannerView = null;
            }
        }
        
        /// <summary>
        /// Shows the interstitial ad.
        /// </summary>
        public void ShowInterstitialAd()
        {
            _indexOfAd++;
            
            if (_indexOfAd < 2)
            {
                return;
            }

            _indexOfAd = 0;
            if (_interstitialAd != null && _interstitialAd.CanShowAd())
            {
                Debug.Log("Showing interstitial ad.");
                _interstitialAd.Show();
            }
            else
            {
                Debug.LogError("Interstitial ad is not ready yet.");
            }
        }
    }
}