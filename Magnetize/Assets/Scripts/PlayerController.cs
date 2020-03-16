﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public float moveSpeed = 5f;
    public float pullForce = 100f;
    public float rotateSpeed = 360f;
    //private GameObject closestTower;
    //private GameObject hookedTower;
    //private bool isPulled = false;
    private UIControllerScript uiControl;
    private AudioSource myAudio;
    public bool isCrashed = false;
    private Vector3 startPosition;
    private Quaternion startRotation;
    [SerializeField] private Text deathPointTxt;
    private int deathPoint;



    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;
        startRotation = this.transform.rotation;
        rb2D = this.gameObject.GetComponent<Rigidbody2D>();
        uiControl = GameObject.Find("Canvas").GetComponent<UIControllerScript>();
        myAudio = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCrashed)
        {
            if (!myAudio.isPlaying)
            {
                //restart scene
                restartPosition();
            }
        }
        else
        {
            //Move the object
            rb2D.velocity = -transform.up * moveSpeed;
        }


        /*if (Input.GetKey(KeyCode.Z) && !isPulled)
        {
            if (closestTower != null && hookedTower == null)
            {
                hookedTower = closestTower;
            }

            if (hookedTower)
            {
                float distance = Vector2.Distance(transform.position, hookedTower.transform.position);

                //Gravitation toward tower
                Vector3 pullDirection = (hookedTower.transform.position - transform.position).normalized;
                float newPullForce = Mathf.Clamp(pullForce / distance, 20, 50);
                rb2D.AddForce(pullDirection * newPullForce);

                //Angular velocity
                rb2D.angularVelocity = -rotateSpeed / distance;
                isPulled = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            isPulled = false;
            rb2D.angularVelocity = 0;
        }*/

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            if (!isCrashed)
            {
                //hookedTower = null;
                deathPoint++;
                deathPointTxt.text = deathPoint.ToString();

                //Play SFX
                myAudio.Play();
                rb2D.velocity = new Vector3(0f, 0f, 0f);
                rb2D.angularVelocity = 0f;
                isCrashed = true;

            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            Debug.Log("Level Clear!");
            uiControl.endGame();
        }
    }

    /*public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tower")
        {
            closestTower = collision.gameObject;

            //change closest tower to green
            collision.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        //if (isPulled) return;

        if (collision.gameObject.tag == "Tower")
        {
            hookedTower = null;
            closestTower = null;

            //Change tower color back to normal
            collision.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }*/

    public void restartPosition()
    {
        //Set to start position
        this.transform.position = startPosition;

        //Restart rotation
        this.transform.rotation = startRotation;

        //Set isCrashed to false
        isCrashed = false;

        /*if (closestTower)
        {
            closestTower.GetComponent<SpriteRenderer>().color = Color.white;
            closestTower = null;
        }*/
    }
}
