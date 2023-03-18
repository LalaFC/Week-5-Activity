using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float force;
    private float Timer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        Timer += Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer == 10)
        {
            Destroy(this.gameObject);
            UnityEngine.Debug.Log("Bullet Destroyed");
        }    
    }
}
