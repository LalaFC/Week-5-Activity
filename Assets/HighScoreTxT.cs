using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScoreTxT : MonoBehaviour
{
    public TextMeshProUGUI highScore;
    public TextMeshProUGUI Score;
    private const string Save_Score = "Score";

    // Start is called before the first frame update
    void Start()
    {
        Score.text = "Your Score is " + PlayerPrefs.GetInt("PlayerScore");
        highScore.text = "Highest Score: " + PlayerPrefs.GetInt(Save_Score);
    }

}
