using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{
    //These are the player's Variables, the raw info that defines them
    
    //The Rigidbody2D is a component that gives the player physics, and is what we use to move
    public Rigidbody2D RB;

    //TextMeshPro is a component that draws text on the screen.
    //We use this one to show our score.
    public TextMeshPro ScoreText;
    
    //This will control how fast the player moves
    public float Speed = 5;
    
    //This is how many points we currently have
    public int Score = 0;

    public float jumpSpeed = 10;

    public GameObject portal1;
    public GameObject portal2;
    private Camera cam;
    public int portalTimer;
    public GameObject background;

    Animator m_Animator;

    //Start automatically gets triggered once when the objects turns on/the game starts
    void Start()
    {
        //During setup we call UpdateScore to make sure our score text looks correct
       // UpdateScore();
        cam = Camera.main;
        portal1.transform.position = new Vector3(1000, 1000, 0);
        portal2.transform.position = new Vector3(1000, 1000, 0);


    }

    //Update is a lot like Start, but it automatically gets triggered once per frame
    //Most of an object's code will be called from Update--it controls things that happen in real time
    void Update()
    {
        background.transform.position = new Vector3(transform.position.x, transform.position.y+3, 2);
        Vector3 mousePos = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
        //The code below controls the character's movement
        //First we make a variable that we'll use to record how we want to move
        Vector2 vel = new Vector2(0, RB.velocity.y);
        
        //Then we use if statement to figure out what that variable should look like
        
        //If I hold the right arrow key, the player should move right. . .
        if (Input.GetKey(KeyCode.D))
        {
            vel.x = Speed;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        //If I hold the left arrow, the player should move left. . .
        if (Input.GetKey(KeyCode.A))
        {
            vel.x = -Speed;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (RB.velocity.x == 0)
        {
            gameObject.GetComponent<Animator>().ResetTrigger("Run");
            gameObject.GetComponent<Animator>().SetTrigger("Idle");
        }
        else
        {
            gameObject.GetComponent<Animator>().SetTrigger("Run");
            gameObject.GetComponent<Animator>().ResetTrigger("Idle");

        }
        if (Input.GetKey(KeyCode.Space) && RB.velocity.y == 0)
        {

            vel.y = jumpSpeed;
        }
        /*If I hold the up arrow, the player should move up. . .
        if (Input.GetKey(KeyCode.W))
        {
            vel.y = Speed;
        }
        //If I hold the down arrow, the player should move down. . .
        if (Input.GetKey(KeyCode.S))
        {
            vel.y = -Speed;
        }
        */
        //Finally, I take that variable and I feed it to the component in charge of movement
        RB.velocity = vel;

        //if (gameObject.transform.position.x > 12) { gameObject.transform.position = new Vector2(-10, gameObject.transform.position.y); }
        //if (gameObject.transform.position.x < -12) { gameObject.transform.position = new Vector2(10, gameObject.transform.position.y); }
        //if (gameObject.transform.position.y < -7) { gameObject.transform.position = new Vector2(gameObject.transform.position.x, 6); }
        //if (gameObject.transform.position.y > 7) { gameObject.transform.position = new Vector2(gameObject.transform.position.x, -6); }


        if (Input.GetMouseButtonDown(1))
        {
            if (portalTimer == 0)
            {
                // instantiate first.
                portal1.transform.position = mousePos;
            }

            if (portalTimer == 1)
            {
                portal2.transform.position = mousePos;
            }

            if (portalTimer == 2)
            {
                portal1.transform.position = new Vector3(1000, 1000, 0);
                portal2.transform.position = new Vector3(1000, 1000, 0);
                //destroy both
                portalTimer = 0;
            }
            else portalTimer++;

        }
    }

    //This gets called whenever you bump into another object, like a wall or coin.
    private void OnCollisionEnter2D(Collision2D other)
    {
        //This checks to see if the thing you bumped into had the Hazard tag
        //If it does...
        if (other.gameObject.CompareTag("Hazard"))
        {
            //Run your 'you lose' function!
            Die();
        }
        
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //This checks to see if the thing you bumped into ha the CoinScript script on it
        CoinScript coin = other.gameObject.GetComponent<CoinScript>();
        //If it does, run the code block belows
        if (coin != null)
        {
            //Tell the coin that you bumped into them so they can self destruct or whatever
            coin.GetBumped();
            //Make your score variable go up by one. . .
            Score++;
            //And then update the game's score text
            UpdateScore();
        }

        if (other.gameObject.CompareTag("Hazard"))
        {
            //Run your 'you lose' function!
            Die();
        }

        if (other.gameObject.CompareTag("Portal") && portalTimer == 2)
        {
            transform.position = portal2.transform.position;
        }
    }

    //This function updates the game's score text to show how many points you have
    //Even if your 'score' variable goes up, if you don't update the text the player doesn't know
    public void UpdateScore()
    {
        ScoreText.text = "Score: " + Score;
    }

    //If this function is called, the player character dies. The game goes to a 'Game Over' screen.
    public void Die()
    {
        SceneManager.LoadScene("Game Over");
    }
}
