using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;


public class HSController : MonoBehaviour
{
    private ScoreManager scoreManager;
    private string secretKey = "wX4R7Fgz320"; // Edit this value and make sure it's the same as the one stored on the server
    string addScoreURL = "vormikestudios.com/addscore.php?"; //be sure to add a ? to your url
    string highScoreURL = "vormikestudios.com/display.php";
    string playerScoreURL = "vormikestudios.com/displayplayerscore.php?";

    [HideInInspector]
    public string uniqueID;
    [HideInInspector]
    public string name3;
    [HideInInspector]
    public int score;

    [HideInInspector]
    public int currentHighScore;
    [HideInInspector]
    public string[] onlineHighscore;

    private static HSController instance6;

    public static HSController Instance
    {
        get { return instance6; }
    }

    void Awake()
    {
        //DontDestroyOnLoad (gameObject);
        // If no Player ever existed, we are it.
        if (instance6 == null)
            instance6 = this;
        // If one already exist, it's because it came from another level.
        else if (instance6 != this)
        {
            Destroy(gameObject);
            return;
        }

        scoreManager = ScoreManager.Instance();
#if UNITY_IOS
        uniqueID = UnityEngine.iOS.Device.advertisingIdentifier;
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass clsUnity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject objActivity = clsUnity.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject objResolver = objActivity.Call<AndroidJavaObject>("getContentResolver");
        AndroidJavaClass clsSecure = new AndroidJavaClass("android.provider.Settings$Secure");
        string android_id = clsSecure.CallStatic<string>("getString", objResolver, "android_id");
        uniqueID = android_id + SystemInfo.deviceName;
#endif

        //Debug.Log(uniqueID);
        if (uniqueID == "")
        {
            uniqueID = SystemInfo.deviceUniqueIdentifier;
        }
        //Debug.Log(uniqueID);

    }

    void Start()
    {

        GameControl.control.Load();

        //Debug.Log("Gamer Name_____________________" + GameControl.control.gamerName);

        if (GameControl.control.gamerName != "" && SceneManager.GetActiveScene().name != "02a Game_Mode")
        {
            StartCoroutine(RunGetAndPostScoresLoop());

            //HSController.Instance.StartGetScores ();
        }

        if (SceneManager.GetActiveScene().name == "02a Game_Mode")
        {
            StartCoroutine(StartGetPlayerScore());
        }

        //Application.RequestAdvertisingIdentifierAsync(
        //   (string advertisingId, bool trackingEnabled, string error) =>
        //   { Debug.Log("advertisingId " + advertisingId + " " + trackingEnabled + " " + error); }
        //);
    }

    public string Md5Sum(string strToEncrypt)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }

    public IEnumerator RunGetAndPostScoresLoop()
    {
        // To run one after the other
        yield return StartCoroutine(GetPlayerScore());

        yield return StartCoroutine(PostScores());

        yield return StartCoroutine(GetScores());
    }

    public IEnumerator StartGetPlayerScore()
    {
        yield return StartCoroutine(GetPlayerScore());
    }

    IEnumerator GetPlayerScore()
    {
        string get_url = playerScoreURL + "&uniqueID=" + uniqueID;

        //Debug.Log("get_url " + get_url);

        //WWW hs_get = new WWW("http://" + get_url);
        UnityWebRequest hs_get = UnityWebRequest.Get("https://" + get_url);


        yield return hs_get.SendWebRequest();

        if (hs_get.isNetworkError || hs_get.isHttpError)
        {
            Debug.Log("There was an error getting the player score: " + hs_get.error);
        }
        else
        {
            //Debug.Log("hs_get.downloadHandler.text " + hs_get.downloadHandler.text);

            // get the current high score from the database - convert to an int
            int.TryParse(hs_get.downloadHandler.text, out currentHighScore);
            //Debug.Log("currentHighScore " + currentHighScore);

            if (currentHighScore > GameControl.control.bestScore)
            {
                GameControl.control.bestScore = currentHighScore;
                GameControl.control.Save();
            }

            if (currentHighScore == 0)
            {
                currentHighScore = GameControl.control.bestScore;
            }
        }
    }

    //set actual values before posting score
    public void UpdateOnlineHighScoreData()
    {
        name3 = GameControl.control.gamerName;

        if (scoreManager.totalScore > currentHighScore)
        {
            score = scoreManager.totalScore;
        }
        else
        {
            score = currentHighScore;
        }
    }

    IEnumerator PostScores()
    {
        UpdateOnlineHighScoreData();
        //This connects to a server side php script that will add the name and score to a MySQL DB.
        // Supply it with a string representing the players name and the players score.
        string hash = Md5Sum(name3 + score + secretKey);
        //string post_url = addScoreURL + "name=" + WWW.EscapeURL(name) + "&score=" + score + "&hash=" + hash;
        // string post_url = addScoreURL + "&uniqueID=" + uniqueID + "&name=" + WWW.EscapeURL (name3) + "&score=" + score + "&hash=" + hash;
        string post_url = addScoreURL + "&uniqueID=" + uniqueID + "&name=" + UnityWebRequest.EscapeURL(name3) + "&score=" + score + "&hash=" + hash;

        //Debug.Log ("post url " + post_url);
        // Post the URL to the site and create a download object to get the result.
        // WWW hs_post = new WWW("http://" + post_url);
        UnityWebRequest hs_post = UnityWebRequest.Get("https://" + post_url);


        yield return hs_post.SendWebRequest(); // Wait until the download is done

        if (hs_post.isNetworkError || hs_post.isHttpError)
        {
            print("There was an error posting the high score: " + hs_post.error);
        }
        else
        {
            // Success!
            //Debug.Log(post_url);
        }
    }

    // Get the scores from the MySQL DB to display in a GUIText.
    IEnumerator GetScores()
    {
        if (Scrolllist.Instance != null)
        {
            Scrolllist.Instance.loading = true;
        }

        // WWW hs_get = new WWW("http://" + highScoreURL);
        UnityWebRequest hs_get = UnityWebRequest.Get("https://" + highScoreURL);


        yield return hs_get.SendWebRequest();

        if (hs_get.isNetworkError || hs_get.isHttpError)
        {
            Debug.Log("There was an error getting the high score: " + hs_get.error);
            Scrolllist.Instance.errorLoading = true;
        }
        else
        {
            //Debug.Log("hs_get.downloadHandler.text 2" + hs_get.downloadHandler.text);
            //Change .text into string to use Substring and Split
            string help = hs_get.downloadHandler.text;

            //help= help.Substring(5, hs_get.text.Length-5);
            //200 is maximum length of highscore - 100 Positions (name+score)

            onlineHighscore = help.Split(";"[0]);
        }

        if (Scrolllist.Instance != null)
        {
            Scrolllist.Instance.loading = false;
            Scrolllist.Instance.getScrollEntrys();
        }
    }

    public void StartGetScores()
    {
        StartCoroutine(GetScores());
    }

    public void StartPostScores()
    {
        StartCoroutine(PostScores());
    }

    public void StartRunGetAndPostScoresLoop()
    {
        StartCoroutine(RunGetAndPostScoresLoop());
    }
}


/* using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class HSController : MonoBehaviour
{
    private ScoreManager scoreManager;
    private string secretKey = "wX4R7Fgz320"; // Edit this value and make sure it's the same as the one stored on the server
    string addScoreURL = "vormike.000webhostapp.com/addscore.php?"; //be sure to add a ? to your url
    string highScoreURL = "vormike.000webhostapp.com/display.php";
    string playerScoreURL = "vormike.000webhostapp.com/displayplayerscore.php?";

    [HideInInspector]
    public string uniqueID;
    [HideInInspector]
    public string name3;
    [HideInInspector]
    public int score;

    [HideInInspector]
    public int currentHighScore;
    [HideInInspector]
    public string[] onlineHighscore;

    private static HSController instance6;

    public static HSController Instance
    {
        get { return instance6; }
    }

    void Awake()
    {
        

        //DontDestroyOnLoad (gameObject);
        // If no Player ever existed, we are it.
        if (instance6 == null)
            instance6 = this;
        // If one already exist, it's because it came from another level.
        else if (instance6 != this)
        {
            Destroy(gameObject);
            return;
        }

        scoreManager = ScoreManager.Instance();
#if UNITY_IOS
        uniqueID = UnityEngine.iOS.Device.advertisingIdentifier;
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass clsUnity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject objActivity = clsUnity.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject objResolver = objActivity.Call<AndroidJavaObject>("getContentResolver");
        AndroidJavaClass clsSecure = new AndroidJavaClass("android.provider.Settings$Secure");
        string android_id = clsSecure.CallStatic<string>("getString", objResolver, "android_id");
        uniqueID = android_id + SystemInfo.deviceName;
#endif

        //Debug.Log(uniqueID);
        if (uniqueID == "")
        {
            uniqueID = SystemInfo.deviceUniqueIdentifier;
        }
        //Debug.Log(uniqueID);

	}

	void Start(){

        GameControl.control.Load();

        //Debug.Log("Gamer Name_____________________" + GameControl.control.gamerName);

        if(GameControl.control.gamerName != "" && SceneManager.GetActiveScene().name != "02a Game_Mode"){
            
            StartCoroutine(RunGetAndPostScoresLoop());

            //HSController.Instance.StartGetScores ();
        }

        if (SceneManager.GetActiveScene().name == "02a Game_Mode")
        {
            StartCoroutine(StartGetPlayerScore());
        }

        //Application.RequestAdvertisingIdentifierAsync(
        //   (string advertisingId, bool trackingEnabled, string error) =>
        //   { Debug.Log("advertisingId " + advertisingId + " " + trackingEnabled + " " + error); }
        //);
	}

	public string Md5Sum(string strToEncrypt)
	{
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
		byte[] bytes = ue.GetBytes(strToEncrypt);
		
		// encrypt bytes
		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] hashBytes = md5.ComputeHash(bytes);
		
		// Convert the encrypted bytes back to a string (base 16)
		string hashString = "";
		
		for (int i = 0; i < hashBytes.Length; i++)
		{
			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
		}
		
		return hashString.PadLeft(32, '0');
	}

    public IEnumerator RunGetAndPostScoresLoop()
    {
        // To run one after the other
        yield return StartCoroutine(GetPlayerScore());

        yield return StartCoroutine(PostScores());

        yield return StartCoroutine(GetScores());
    }

    public IEnumerator StartGetPlayerScore()
    {
        yield return StartCoroutine(GetPlayerScore());
    }

    IEnumerator GetPlayerScore()
    {
        string get_url = playerScoreURL + "&uniqueID=" + uniqueID;

        //Debug.Log("get_url " + get_url);

        WWW hs_get = new WWW("http://" + get_url);

        yield return hs_get;

        if (hs_get.error != null)
        {
            Debug.Log("There was an error getting the player score: " + hs_get.error);
        }
        else
        {
            // get the current high score from the database - convert to an int
            int.TryParse(hs_get.text, out currentHighScore);
            //Debug.Log("currentHighScore " + currentHighScore);

            if (currentHighScore > GameControl.control.bestScore)
            {
                GameControl.control.bestScore = currentHighScore;
                GameControl.control.Save();
            }

            if (currentHighScore == 0)
            {
                currentHighScore = GameControl.control.bestScore;
            }
        }
    }

    //set actual values before posting score
    public void UpdateOnlineHighScoreData()
    {
        name3 = GameControl.control.gamerName;

        if (scoreManager.totalScore > currentHighScore)
        {
            score = scoreManager.totalScore;
        }
        else
        {
            score = currentHighScore;
        }
    }
	
	IEnumerator PostScores()
	{
        UpdateOnlineHighScoreData ();
		//This connects to a server side php script that will add the name and score to a MySQL DB.
		// Supply it with a string representing the players name and the players score.
		string hash = Md5Sum(name3 + score + secretKey);
		//string post_url = addScoreURL + "name=" + WWW.EscapeURL(name) + "&score=" + score + "&hash=" + hash;
        string post_url = addScoreURL + "&uniqueID=" + uniqueID + "&name=" + WWW.EscapeURL (name3) + "&score=" + score + "&hash=" + hash;
		//Debug.Log ("post url " + post_url);
		// Post the URL to the site and create a download object to get the result.
		WWW hs_post = new WWW("http://" + post_url);

		yield return hs_post; // Wait until the download is done
		
		if (hs_post.error != null)
		{
			print("There was an error posting the high score: " + hs_post.error);
        } 
        else
        {
            // Success!
            //Debug.Log(post_url);
        }
	}
	
	// Get the scores from the MySQL DB to display in a GUIText.
	IEnumerator GetScores()
	{
        if (Scrolllist.Instance != null)
        {
            Scrolllist.Instance.loading = true;
        }

		WWW hs_get = new WWW("http://" + highScoreURL);

		yield return hs_get;
		
		if (hs_get.error != null)
		{
			Debug.Log("There was an error getting the high score: " + hs_get.error);
            Scrolllist.Instance.errorLoading = true;
		}
		else
		{
			//Change .text into string to use Substring and Split
			string help = hs_get.text;

			//help= help.Substring(5, hs_get.text.Length-5);
			//200 is maximum length of highscore - 100 Positions (name+score)

			onlineHighscore = help.Split(";"[0]);
        }

        if (Scrolllist.Instance != null)
        {
            Scrolllist.Instance.loading = false;
            Scrolllist.Instance.getScrollEntrys();
        }
	}

    public void StartGetScores()
    {
        StartCoroutine(GetScores());
    }

    public void StartPostScores()
    {
        StartCoroutine(PostScores());
    }

    public void StartRunGetAndPostScoresLoop()
    {
        StartCoroutine(RunGetAndPostScoresLoop());
    }
} */
