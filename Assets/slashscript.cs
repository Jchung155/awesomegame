using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slashscript : MonoBehaviour
{
    public GameObject slashPosition;
    public GameObject player;
    private Camera cam;
    public int portalTimer;
    public float lastSlashTime;
    public float slashDelay = 100;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        portalTimer = 0;
        transform.position = new Vector3(1000, 1000, 0);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            if (portalTimer == 0)
            {
                // instantiate first.
            }

            if (portalTimer == 1)
            {
                // instantiate second.
            }

            if (portalTimer == 2)
            {
                //destroy both
                portalTimer = 0;
            }
            else portalTimer++;

        }
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time >= lastSlashTime)
            {
                //add the current time to the button delay
                lastSlashTime = Time.time + slashDelay;

                gameObject.transform.position = slashPosition.transform.position;
                Vector3 mousePos = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
                float angle = Mathf.Atan2(mousePos.y - player.transform.position.y, mousePos.x - player.transform.position.x);
                float deg = (180 / Mathf.PI) * angle;
                gameObject.transform.rotation = Quaternion.Euler(0f, 0f, deg);


            }
        }

    }

}
