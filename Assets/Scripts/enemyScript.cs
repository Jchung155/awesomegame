using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    public Rigidbody2D RB;
    public GameObject player;
    public float speed = 0.4f;
    // Start is called before the first frame update
    void Start()
    {
        Killed();
    }

    // Update is called once per frame
    void Update()
    {

        //Vector2 vel = new Vector2(player.transform.position.x - RB.transform.position.x, GameObject.Find("Player").transform.position.y- RB.transform.position.y);
        //gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, player.transform.position, Speed * Time.deltaTime);
        Vector2 vel = (player.transform.position - gameObject.transform.position);
        speed += 0.0005f;
        RB.velocity = vel * speed;

      


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("h");
        if (other.gameObject.CompareTag("Sword"))
        { 
            Killed();
        }


    }

    public void Killed()
    {
        int x = 0; int y = 0;
        speed = 0.4f;
        while (((x < 10) && (x > -10)) || ((y > -5) && (y<5))) {
            x = UnityEngine.Random.Range(-15, 15);
            y = UnityEngine.Random.Range(-8, 8);
            Debug.Log(x);
            Debug.Log(y);
        }
        gameObject.transform.position = new Vector2(x, y);

    }
}
