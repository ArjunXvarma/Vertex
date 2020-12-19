using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using UnityEngine;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    string playStoreID = "3804361";
    public bool testMode = false;
    private string myPlacementId = "rewardedVideo";
    Player player;
    public GameObject Player, adsUI;

    void Start()
    {

        Advertisement.AddListener(this);
        Advertisement.Initialize(playStoreID, testMode);
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null)
        {
            Debug.Log("Not Working");
        }
    }
    
    public bool checkAds()
    {
        return Advertisement.IsReady();
    }

    public void displayAds()
    {  
        Advertisement.Show(myPlacementId);
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish (string placementId, ShowResult showResult) {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished) 
        {
            player.setScoreUIActive();
            Player.SetActive(true);
            player.isGameOver = false;
            player.powerup = true;
            player.unPauseMusic();
            if (Player.transform.position.y < -3)
            {
                player.isAdDeath = true;
            }
        } 
        else if (showResult == ShowResult.Skipped) 
        {
            Debug.Log("u skipped the ad");
        } 
        else if (showResult == ShowResult.Failed) 
        {
            Debug.LogWarning ("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsReady (string placementId) {
        // If the ready Placement is rewarded, show the ad:
        if (placementId == myPlacementId) {
            // Optional actions to take when the placement becomes ready(For example, enable the rewarded ads button)
        }
    }

    public void OnUnityAdsDidError (string message) {
        // Log the error.
    }

    public void OnUnityAdsDidStart (string placementId) {
        // Optional actions to take when the end-users triggers an ad.
    } 

    // When the object that subscribes to ad events is destroyed, remove the listener:
    public void OnDestroy() {
        Advertisement.RemoveListener(this);
    }
}
