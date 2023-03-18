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
    private float timer = 0;
    private bool enemyKilled = false;
    public TextMeshProUGUI scoretext;
    public TextMeshProUGUI highScore;
    private const string Save_Score = "Score";
    public int score, highscore;

    private Vector2 boundary;
    private float _userWt;
    private float _userHt;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpsRemaining = maxJumps;
        highScore.text = "Highest Score: " + PlayerPrefs.GetInt(Save_Score);
        highscore = PlayerPrefs.GetInt(Save_Score);

        boundary = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        _userWt = transform.GetComponent<BoxCollider2D>().bounds.size.x / 2;
        _userHt = transform.GetComponent<BoxCollider2D>().bounds.size.y / 2;
        highscore = PlayerPrefs.GetInt(Save_Score);
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        timer += Time.deltaTime;
        UnityEngine.Debug.Log("timer =" + timer);
        // Move player horizontally

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)
        {

            // Jump by changing the y velocity
            rb.velocity = new Vector2(rb.velocity.x, jump);
            jumpsRemaining--;
        }

        if (timer > 2 && enemyKilled == true)
        {
            enemyKilled = false;
            SpawnEnemy();
            UnityEngine.Debug.Log("Enemy Spawned.");
        }

        //Boundary death
        Vector3 CurrentPos = transform.position;

        if ((CurrentPos.x
            - _userWt) < (boundary.x * -1))
            CurrentPos.x = (boundary.x * -1) + _userWt;

        if ((CurrentPos.x + _userWt) > boundary.x)
            CurrentPos.x = boundary.x - _userWt;

        if ((CurrentPos.y + _userHt) > boundary.y)
            CurrentPos.y = boundary.y - _userHt;

        transform.position = CurrentPos;

        if ((CurrentPos.y - _userHt) < (boundary.y * -1))
        {
            PlayerDied();
        }

        //Switching Avatar Direction
        if (horizontal < 0 && Right == false)
            Flip();
        if (horizontal > 0 && Right == true)
            Flip();

    }
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
                Destroy(Hit.gameObject);
                timer = 0;
                scoretext.text = "Score: " + score;
                enemyKilled = true;
            }
            else
            {
                PlayerDied();
            }
        }

        if (Hit.gameObject.tag == "Bullet")
        {
            PlayerDied();
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

    public void PlayerDied ()
    {
        UnityEngine.Debug.Log("You have Died. T^T");
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt(Save_Score, highscore);
            PlayerPrefs.Save();
        }
        SceneManager.LoadScene(0);
    }

}
