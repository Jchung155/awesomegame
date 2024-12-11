using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class BulletScript : MonoBehaviour
{
    public float yspeed;
    public float xspeed=4;
    public float range = 3;
    public Rigidbody2D RB;
    public GameObject player;
    public float bulletDelay = 2;

    public float bulletTime;
    // Start is called before the first frame update
    void Start()
    {
        bulletTime = Time.time;
        player = GameObject.Find("Player");
        yspeed = Random.Range(-range, range)/20;
        if (player.transform.position.x < transform.position.x)
        {
            xspeed *= -1;
        }
        
        Debug.Log(player.transform.position.x);
    
    }

    // Update is called once per frame
    void Update()
    {
        //    transform.position = new Vector3(transform.position.x + xspeed, transform.position.y + yspeed, 0);

        RB.velocity = new Vector2(xspeed, yspeed);

        if (Time.time > bulletTime + bulletDelay)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
    
        if (other.gameObject.CompareTag("Sword"))
        {
            xspeed *=-1;
            yspeed *=-1;
            transform.gameObject.tag = "Sword";
        }

        if (other.gameObject.CompareTag("Portal") /*&& player.GetComponent(portalTimer) == 2*/)
        {
            transform.position = GameObject.Find("ExitPortal").transform.position;
            transform.gameObject.tag = "Sword";
        }
    }
}
