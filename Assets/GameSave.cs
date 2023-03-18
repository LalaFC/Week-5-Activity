using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameSave : MonoBehaviour
{
    public GameObject player;


    private const string SAVE_KEY = "SAVE";

    
    private void Start()
    {

    }

    public void Save()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            UnityEngine.Debug.Log("Save Key was pressed.");
            PlayerPrefs.SetString(SAVE_KEY, "HasSaved");
            PlayerPrefs.Save();
            if (PlayerPrefs.HasKey(SAVE_KEY))
            {
                var saveString = PlayerPrefs.GetString(SAVE_KEY);
            }
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {

        }
    }
}
