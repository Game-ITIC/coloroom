using System;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Itic.Ad
{
    public class AdManager : MonoBehaviour
    {
        public static AdManager Instance;

        public bool HasRewardVideo => _rewardedAd != null && _rewardedAd.CanShowAd();
        
        // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
        [SerializeField] private string _adUnitId = "ca-app-pub-4981577085737170/4506162460";
        [SerializeField] private string _adRewardId = "ca-app-pub-4981577085737170/5340367992";
        [SerializeField] private string _adBannerId = "ca-app-pub-4981577085737170/8972066216";
#elif UNITY_IPHONE
  private string _adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
  private string _adUnitId = "unused";
#endif

        private BannerView _bannerView;
        private InterstitialAd _interstitialAd;
        private RewardedAd _rewardedAd;

        private bool _isInitialized;
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
                _isInitialized = true;
                LoadInterstitialAd();
                LoadRewardedAd();
            });
        }

        /// <summary>
        /// Loads the rewarded ad.
        /// </summary>
        public void LoadRewardedAd()
        {
            if (!_isInitialized)
            {
                return;
            }
            // Clean up the old ad before loading a new one.
            if (_rewardedAd != null)
            {
                _rewardedAd.Destroy();
                _rewardedAd = null;
            }

            Debug.Log("Loading the rewarded ad.");

            // create our request used to load the ad.
            var adRequest = new AdRequest();

            // send the request to load the ad.
            RewardedAd.Load(_adRewardId, adRequest,
                (RewardedAd ad, LoadAdError error) =>
                {
                    // if error is not null, the load request failed.
                    if (error != null || ad == null)
                    {
                        Debug.LogError("Rewarded ad failed to load an ad " +
                                       "with error : " + error);
                        return;
                    }

                    Debug.Log("Rewarded ad loaded with response : "
                              + ad.GetResponseInfo());

                    _rewardedAd = ad;
                });
        }
        
        public void ShowRewardedAd(Action<bool> onComplete)
        {
            const string rewardMsg =
                "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

            if (_rewardedAd != null && _rewardedAd.CanShowAd())
            {
                _rewardedAd.Show((Reward reward) =>
                {
                    // TODO: Reward the user.
                    Debug.Log(string.Format(rewardMsg, reward.Type, reward.Amount));
                    onComplete?.Invoke(reward.Amount > 0);
                });
            }
            else
            {
                onComplete?.Invoke(false);
            }
        }
        
        /// <summary>
        /// Loads the interstitial ad.
        /// </summary>
        public void LoadInterstitialAd()
        {
            if (!_isInitialized)
            {
                return;
            }
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

        /// <summary>
        /// Creates the banner view and loads a banner ad.
        /// </summary>
        public void LoadBannerAd()
        {
            if (!_isInitialized)
            {
                return;
            }
            
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
            if (_interstitialAd != null && _interstitialAd.CanShowAd())
            {
                Debug.Log("Showing interstitial ad.");
                _interstitialAd.Show();
            }
            else
            {
                LoadInterstitialAd();
            }
        }
    }
}