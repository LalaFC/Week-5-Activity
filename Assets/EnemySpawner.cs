using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPref;
    public Transform SpawnPt;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
    }

    void Update()
    {

    }

    // Update is called once per frame
    void SpawnEnemy()
    {
        Instantiate(EnemyPref, SpawnPt.position, Quaternion.identity);
    }
}
