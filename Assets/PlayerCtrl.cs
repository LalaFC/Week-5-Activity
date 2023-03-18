using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCtrl : MonoBehaviour
{
    public float speed = 7f;
    public float jump = 7f;
    private Rigidbody2D rb;
    public int maxJumps = 2;
    private int jumpsRemaining = 0;
    bool Right = false;

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


    public void OnCollisionEnter2D(Collision2D Hit)
    {

        if (Hit.gameObject.CompareTag("Ground"))
        {
            jumpsRemaining = maxJumps;
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

}
