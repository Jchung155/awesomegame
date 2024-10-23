using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallscript : MonoBehaviour
{
    public Rigidbody2D RB;
    public float Speed = 0.4f;
    float newSpeed;
    Vector2 newPos;
    // Start is called before the first frame update
    void Start()
    {
        newPos = new Vector2(Random.Range(-10, 10), Random.Range(-5, 5));
        newSpeed = Speed;
    }

    // Update is called once per frame
    void Update()
    {
        newSpeed += 0.0005f;

        if (Mathf.Abs(gameObject.transform.position.x - newPos.x) <= 1 && Mathf.Abs(gameObject.transform.position.y - newPos.y) <= 1) {
            newPos = new Vector2(Random.Range(-10, 10), Random.Range(-5, 5));
            newSpeed = Speed;
            
        }
        Vector2 vel = (newPos - (Vector2) gameObject.transform.position);
      //  vel.Normalize();
      //  Debug.Log(newPos);

        RB.velocity = vel * newSpeed;

        if (gameObject.transform.position.x > 12) { gameObject.transform.position = new Vector2(-10, gameObject.transform.position.y); }
        if (gameObject.transform.position.x < -12) { gameObject.transform.position = new Vector2(10, gameObject.transform.position.y); }
        if (gameObject.transform.position.y < -7) { gameObject.transform.position = new Vector2(gameObject.transform.position.x, 6); }
        if (gameObject.transform.position.y > 7) { gameObject.transform.position = new Vector2(gameObject.transform.position.x, -6); }

    }
}
