using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameSave : MonoBehaviour
{
    public GameObject player;
    private float pX;
    private float pY;

    public void Start()
    {
        pX = player.transform.position.x;
        pY= player.transform.position.y;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Save();
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            Load();
        }
    }

    public void Save()
    {
        float CpX, CpY;
        CpX = player.transform.position.x;
        CpY = player.transform.position.y;

        UnityEngine.Debug.Log("Save Key was pressed.");
        PlayerPrefs.SetFloat("PostionX", CpX);
        PlayerPrefs.SetFloat("PostionY", CpY);
        PlayerPrefs.Save();
    }
    public void Load()
    {
        if (PlayerPrefs.HasKey("PostionX") && PlayerPrefs.HasKey("PostionY"))
        {
            player.transform.position = new Vector2(PlayerPrefs.GetFloat("PostionX"), PlayerPrefs.GetFloat("PostionY"));
        }
        else
        {
            player.transform.position = new Vector2(pX, pY);
        }
    }
}
