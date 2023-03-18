using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerCtrl : MonoBehaviour
{
    public float speed = 7f;
    public float jump = 7f;
    private Rigidbody2D rb;
    public int maxJumps = 2;
    private int jumpsRemaining = 0;
    bool Right = false;
    public GameObject EnemyPref;
    public Transform SpawnPt;
    private float timer = 4;
    public TextMeshProUGUI scoretext;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpsRemaining = maxJumps;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        // Move player horizontally

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)
        {

            // Jump by changing the y velocity
            rb.velocity = new Vector2(rb.velocity.x, jump);
            jumpsRemaining--;
        }

        //Switching Avatar Direction
        if (horizontal < 0 && Right == false)
            Flip();
        if (horizontal > 0 && Right == true)
            Flip();

    }

    private const string Save_Score = "Score";
    private int score;
    public void OnCollisionEnter2D(Collision2D Hit)
    {

        if (Hit.gameObject.CompareTag("Ground"))
        {
            jumpsRemaining = maxJumps;
        }

        if (Hit.gameObject.tag == "Enemy")
        {
            Vector3 hit = Hit.contacts[0].normal;

            float angle = Vector3.Angle(hit, Vector3.up);

            if (Mathf.Approximately(angle, 0))
            {
                UnityEngine.Debug.Log("Enemy killed.");
                score += 1;
                PlayerPrefs.SetInt(Save_Score, score);
                Destroy(Hit.gameObject);
                scoretext.text = "Score: " + score;
                timer += Time.deltaTime;
                if (timer > 3)
                SpawnEnemy();
            }
            else
            {
                UnityEngine.Debug.Log("You have Died. T^T");
                SceneManager.LoadScene(0);
            }
        }

        if (Hit.gameObject.tag == "Bullet")
        {
            UnityEngine.Debug.Log("You have Died. T^T");
            SceneManager.LoadScene(0);
        }

    }
    void Flip()
    {
        Vector3 ObjectDir = gameObject.transform.localScale;
        ObjectDir.x *= -1;
        gameObject.transform.localScale = ObjectDir;
        Right = !Right;
    }
    void SpawnEnemy()
    {
        Instantiate(EnemyPref, SpawnPt.position, Quaternion.identity);
    }
}
