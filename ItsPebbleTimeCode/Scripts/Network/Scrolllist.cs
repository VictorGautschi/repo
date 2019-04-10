using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Scrolllist : MonoBehaviour {
	// Use this for initialization

	private static Scrolllist instance5;

	public static Scrolllist Instance
	{
		get { return instance5; }
	}
	void Awake() {
		
		//DontDestroyOnLoad (gameObject);
		// If no Player ever existed, we are it.
		if (instance5 == null)
			instance5 = this;
		// If one already exist, it's because it came from another level.
		else if (instance5 != this) {
			Destroy (gameObject);
			return;
		}
	}
//____________________________________________________________

	public GameObject ScrollEntry;
	public GameObject ScrollContain;
	public int yourPosition;
	public GameObject LoadingText;
    public GameObject errorText;
    [HideInInspector]
	public bool loading = true;
    [HideInInspector]
    public bool errorLoading;

    public GameObject playerScorePanel;

	void Update () {
	
		if (!loading)
			LoadingText.SetActive (false);
		else
			LoadingText.SetActive (true);

        if (errorLoading)
            errorText.SetActive(true);
        else
            errorText.SetActive(false);

	}

    public void getScrollEntrys()
    {
        //Destroy Objects that exists, because of a possible Call bevore
        foreach (Transform childTransform in ScrollContain.transform) Destroy(childTransform.gameObject);

        int j = 1;
        for (int i = 0; i < HSController.Instance.onlineHighscore.Length - 1; i++)
        {
            GameObject ScorePanel;
            ScorePanel = Instantiate(ScrollEntry) as GameObject;
            ScorePanel.transform.SetParent(ScrollContain.transform);

            //ScorePanel.transform.parent = ScrollContain.transform;
            ScorePanel.transform.localScale = ScrollContain.transform.localScale;
            Transform ThisScoreName = ScorePanel.transform.Find("ScoreText");
            Text ScoreName = ThisScoreName.GetComponent<Text>();
            //
            Transform ThisScorePoints = ScorePanel.transform.Find("ScorePoints");
            Text ScorePoints = ThisScorePoints.GetComponent<Text>();
            //
            Transform ThisScorePosition = ScorePanel.transform.Find("ScorePosition");
            Text ScorePosition = ThisScorePosition.GetComponent<Text>();

            //first position is yellow
            //if (j==1)
            //{
            //  ScoreName.color=Color.grey;
            //             ScorePoints.color=Color.grey;
            //             ScorePosition.color=Color.grey;
            //}
            ScorePosition.text = j + ". ";
            string helpString = "";

            helpString = helpString + HSController.Instance.onlineHighscore[i] + " ";
            i++;

            ScoreName.text = helpString;

            ScorePoints.text = HSController.Instance.onlineHighscore[i];

            //Debug.Log("ScorePoints.text" + ScorePoints.text);

            // if there are 3 at say 120 points, then it will return the lowest number at that score - think golf championships
            if (HSController.Instance.onlineHighscore[i] == HSController.Instance.score.ToString())
            {
                 //You can't make this grey as it might not be the record you want - you are only here grabbing the score from the database and working out the position
                ScoreName.color=Color.grey;
                ScorePoints.color=Color.grey;
                ScorePosition.color=Color.grey;

                if (helpString.Trim() == GameControl.control.gamerName.Trim())
                {
                    yourPosition = j;
                }

                Transform thisPlayerScorePosition = playerScorePanel.transform.Find("PlayerScorePosition");
                Text playerScorePosition = thisPlayerScorePosition.GetComponent<Text>();

                Transform thisPlayerScoreName = playerScorePanel.transform.Find("PlayerScoreText");
                Text playerScoreName = thisPlayerScoreName.GetComponent<Text>();

                Transform thisPlayerScorePoints = playerScorePanel.transform.Find("PlayerScorePoints");
                Text playerScorePoints = thisPlayerScorePoints.GetComponent<Text>();

                playerScorePosition.text = yourPosition.ToString() + ".";
                playerScoreName.text = GameControl.control.gamerName; // This cannot be from ScoreName.text as it maybe bring up a different name above this one but with the same score
                playerScorePoints.text = HSController.Instance.score.ToString();

                playerScoreName.color = Color.grey;
                playerScorePoints.color = Color.grey;
                playerScorePosition.color = Color.grey;
            }
            j++;
        }
    }
}
