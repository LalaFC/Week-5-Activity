using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMec : MonoBehaviour
{

    private Vector2 Direction;
    public GameObject bullet;
    public Transform bulletPos;
    private float timer = 4;

    public float speed = 2;
    public float GroundDist = 2;
    public float EnemyRange = 4;
    private bool _isRight;
    public Transform Detector;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Vector2 Movement = Vector2.right * speed * Time.deltaTime;
        transform.Translate(Movement);


        RaycastHit2D groundCheck = Physics2D.Raycast(Detector.position, Vector2.down, GroundDist);

        if (groundCheck.collider == false)
        {
            if (_isRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                _isRight = false;
                Direction = Vector2.left;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                _isRight = true;
                Direction = Vector2.right;
            }
        }
        LazerBeam();
    }

    void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
        timer = 0;


    }



    public void LazerBeam()
    {

        UnityEngine.Debug.DrawRay(Detector.position, (Direction * EnemyRange), Color.red, 1f);
        var rays = Physics2D.RaycastAll(Detector.position, Direction, EnemyRange);

        foreach (var ray in rays)
        {
            if (ray.collider == null)
                continue;

            if (ray.collider.gameObject.CompareTag("Player") && (timer > 3))
            {
                if (timer > 3)
                {
                    shoot();
                    UnityEngine.Debug.Log("Player Detected.");
                }
            }
        }

    }

}
