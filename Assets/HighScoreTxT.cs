using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScoreTxT : MonoBehaviour
{
    public TextMeshProUGUI highScore;
    private const string Save_Score = "Score";

    // Start is called before the first frame update
    void Start()
    {
        highScore.text = "Highest Score: " + PlayerPrefs.GetInt(Save_Score);
    }

}
