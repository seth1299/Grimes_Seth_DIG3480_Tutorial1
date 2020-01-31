using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb2d;
    private int totalYellows;
    private int count;
    private int lives;
    public Text countText;
    public Text winText;
    public Text livesText;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        totalYellows = 0;
        count = 0;
        lives = 3;
        setCountText();
        winText.text = "";
        setLivesText();
        setLivesText();
    }

    void FixedUpdate()
    {
        if (lives > 0)
        {
            float moveHorizontal = Input.GetAxis("Horizontal") / (Time.deltaTime * 5);
            float moveVertical = Input.GetAxis("Vertical") / (Time.deltaTime * 5);
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            rb2d.AddForce(movement * speed);
        }
        else
        {
            speed = 0;
        }
        if (Input.GetKey("escape"))
            Application.Quit();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp") && lives > 0)
        {
            other.gameObject.SetActive(false);
            totalYellows++;
            count++;
            setCountText();
            if ( totalYellows == 12 )
            {
                gameObject.transform.position = new Vector2(50, -50);
            }
            if ( totalYellows == 20 )
            {
                winText.text = "You win! Game created by Seth Grimes!";
            }


        }
        else if (other.gameObject.CompareTag("Enemy") && lives > 0 && totalYellows < 20)
        {
            other.gameObject.SetActive(false);
            lives--;
            count--;
            setCountText();
            setLivesText();
            if ( lives == 0 )
                winText.text = "You lose! Game created by Seth Grimes!";
        }
    }

    void setCountText()
    {
        countText.text = "Count: " + count.ToString();
    }

    void setLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
    }



}