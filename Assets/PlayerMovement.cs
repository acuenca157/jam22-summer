using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float velocity;

    Animator animator;
    Vector3 mousePos;
    Camera cam;
    Rigidbody2D rid;
    float horizontal, vertical;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rid = this.GetComponent<Rigidbody2D>();
        cam = Camera.main;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        this.transform.Translate(Vector3.up * vertical * velocity * Time.deltaTime, Space.World);
        this.transform.Translate(Vector3.right * horizontal * velocity * Time.deltaTime, Space.World);

        sr.flipX = horizontal < 0;
        sr.flipY = vertical < 0;

        animator.SetFloat("horizontal", horizontal);
        animator.SetFloat("vertical", vertical);

        //rotateToCam();

    }

    private void rotateToCam()
    {
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z-cam.transform.position.z));
        rid.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((mousePos.y-transform.position.y), (mousePos.x-transform.position.x))*Mathf.Rad2Deg);
    }
}
