using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_control : MonoBehaviour
{
    public Animator animator_feet;
    public Animator animator_body;
    public GameObject feet_object;
    public Animator enemy_anim;
    public Transform player;
    public Transform enemy;
    public float moveSpeed;
    public Transform feet;

    private bool is_attacking;

    private Vector2 moveDirection;

    private float attack_time = 0.7f;

    private float time = 0f;
    


    public Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!animator_body.GetBool("is_dead"))
        {
            ProcessInputs();
            attack_handler();
        }

    }

    void attack_handler()
    {
        if (is_attacking)
        {
            animator_body.SetBool("is_attacking", true);
            if (combat_handler.Melee_combat_handler(player, enemy, "enemy"))
            {
                print("hit");
                enemy_anim.SetBool("is_dead", true);
            }
        }
        else
        {
            animator_body.SetBool("is_attacking", false);

        }

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
        if (animator_body.GetBool("is_dead"))
        {
            Destroy(rb);
            Destroy(feet_object);
        }
        else
        {
            faceMouse();
            feet.up = rb.velocity.normalized;
            Move();
        }
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        is_attacking = Input.GetMouseButtonDown(0);

        moveDirection = new Vector2(moveX, moveY).normalized;
    }



    void Move()
    {
        animator_feet.SetBool("is_moving", moveDirection.x != 0 || moveDirection.y != 0);
        animator_body.SetBool("is_moving", moveDirection.x != 0 || moveDirection.y != 0);
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }


}   
