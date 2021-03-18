using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_control : MonoBehaviour
{
    public Animator animator_feet;
    public Animator animator_body;
    public float moveSpeed;
    public Transform feet;


    private Vector2 moveDirection;


    public Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        faceMouse();
        feet.up = rb.velocity.normalized;
    }

    void faceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2( mousePosition.x - transform.position.x, mousePosition.y - transform.position.y );
        transform.up = direction;
    }

    void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        animator_feet.SetBool("is_moving", moveDirection.x != 0 || moveDirection.y != 0);
        animator_body.SetBool("is_moving", moveDirection.x != 0 || moveDirection.y != 0);
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }


}   
