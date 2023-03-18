using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameStarter : MonoBehaviour
{
    public Button playButton;

    private void OnEnable()
    {
        playButton.onClick.AddListener(StartGame);
    }

    private void OnDisable()
    {
        playButton.onClick.RemoveListener(StartGame);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
