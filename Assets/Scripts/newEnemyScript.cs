using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newEnemyScript : MonoBehaviour
{
    // Start is called before the first frame update

    public bool attack;
    public float speed = 1.5f;
    public Rigidbody2D RB;

    public float lastShotTime;
    public float shotDelay = 0.5f;

    public float turnTime;
    public float turnDelay = 3;

    public GameObject player;

  
    public GameObject bullet;
    void Start()
    {
        attack = false;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //idle mode
        
        if (attack == false)
        {
            
            RB.velocity = new Vector2(speed, RB.velocity.y);

            if (Time.time >= turnTime)
            {
                //add the current time to the button delay
                turnTime = Time.time + turnDelay;

                //This line of code actually spawns the object. Ignore 'Quaternion.identity' for now
                speed *= -1;
                if (speed < 0)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }
                else GetComponent<SpriteRenderer>().flipX = false;

            }


        }
        else
        {
            if (Time.time >= lastShotTime)
            {
                //add the current time to the button delay
                lastShotTime = Time.time + shotDelay;

                //This line of code actually spawns the object. Ignore 'Quaternion.identity' for now
                Instantiate(bullet, transform.position, Quaternion.identity);

            }

        }

        //attack mode

        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < 5)
        {
            attack = true;
        }
        else if (distance > 10)
        {
            attack = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //This checks to see if the thing you bumped into has the CoinScript script on it
        PlayerScript player = other.gameObject.GetComponent<PlayerScript>();
        //If it does, run the code block belows
        if (player != null)
        {
            attack = true;
        }
    
    
       if (other.gameObject.CompareTag("Sword"))
         {
            Destroy(gameObject);
         }

        if (other.gameObject.CompareTag("Portal") /*&& player.GetComponent(portalTimer) == 2*/)
        {
            transform.position = GameObject.Find("ExitPortal").transform.position;
            //transform.gameObject.tag = "Sword";
        }

    }
}


