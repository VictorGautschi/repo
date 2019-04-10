using UnityEngine;
using System.Collections;
using UnityEngine.Monetization;

public class AdManager : MonoBehaviour
{
    [HideInInspector]
    public string placementId = "video";

    public int showAdEveryXTimes;

    #if UNITY_IOS
        private string gameId = "3090982";
    #elif UNITY_ANDROID
        private string gameId = "3090983";
    #endif

    bool testMode = false; // This must be false when LIVE and true for testing

    int levelCountToAd;

    public static AdManager adManager;

    public static AdManager Instance()
    {
        if (!adManager)
        {
            adManager = FindObjectOfType(typeof(AdManager)) as AdManager;
            if (!adManager)
            {
                Debug.LogError("There needs to be one active MusicManager script on a GameObject in your scene.");
            }
        }
        return adManager;
    }

    void Awake()
    {
        levelCountToAd = 0;

        GameObject[] objs = GameObject.FindGameObjectsWithTag("Ad Manager");
        if (objs.Length > 1)
            Destroy(this.gameObject);

        DontDestroyOnLoad(gameObject);
        //Debug.Log ("Don't destroy on load: " + name);
    }

    private void Start()
    {
        Monetization.Initialize(gameId, testMode);
#if UNITY_ANDROID
        // Android should not show ads for now
        showAdEveryXTimes = 100;
#else
        showAdEveryXTimes = 5;
#endif
    }

    public void ShowAd()
    {
        levelCountToAd += 1;

        // This will show every 4 (not 5)
        if (levelCountToAd % showAdEveryXTimes == 0)
        {

#if UNITY_EDITOR
            StartCoroutine(WaitForAd());
#endif

            StartCoroutine(ShowAdWhenReady());

            levelCountToAd += 1;

            Debug.Log("levelCountToAd " + levelCountToAd);
        }
    }

    private IEnumerator ShowAdWhenReady()
    {
        while (!Monetization.IsReady(placementId))
        {
            yield return null;
        }

        ShowAdPlacementContent ad = null;
        ad = Monetization.GetPlacementContent(placementId) as ShowAdPlacementContent;

        if (ad != null)
        {
            ad.Show();
        }
    }

    IEnumerator WaitForAd()
    {
        float currentTimeScale = Time.timeScale;
        // Debug.Log("currentTimeScale" + currentTimeScale);

        Time.timeScale = 0f;
        yield return null;

        while (Monetization.isInitialized)
            yield return null;

        Time.timeScale = currentTimeScale;

        /* timeScale is 0 before levels, so maybe just set to 1 here? */

        /* Music needs to stop here as well while ad is playing, but not just for editor, always */
    }
}
