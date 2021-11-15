using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;


public class postac : MonoBehaviour
{

    public float movementspeed;
    public bool flip = true;
    private Rigidbody2D rbBody;

    private Vector2 direction;
    private Animator anim;

    public bool run;
    public Rigidbody2D rb;
    Vector2 movement;
    public float speeed;

    void Flip()
    {
        flip = !flip;
        Vector3 scale = gameObject.transform.localScale;
        scale.x *= -1;
        gameObject.transform.localScale = scale;
    }
    /* void Turning()
    {
        float pozmove = Input.GetAxis("Horizontal");
        Vector3 skala = gameObject.transform.localScale;

        if (pozmove < 0 && flip == true)
        {
            Flip();
        }

        if (pozmove > 0 && flip == false)
        {
            Flip();
        }
    }
    */
    void Start()
    {
        rbBody = GetComponent<Rigidbody2D>();
        // anim = GetComponent<Animator>();
    }


    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


        //Turning();//

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {

            anim.SetBool("run", true);
        }
        else
        {
            anim.SetBool("run", false);
        };
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speeed * Time.fixedDeltaTime);
    }

}