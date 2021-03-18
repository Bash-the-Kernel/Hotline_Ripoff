using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_controller : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform enemy;
    public Transform[] nodes;
    public Transform player;
    public Transform spawn;
    public Rigidbody2D rb;
    public float moveSpeed = 2;
    public float rotSpeed;
    private double delta = 0.1;
    private bool is_at_end_of_path = false;
    private int i = 0;
    void Start()
    {
        enemy = spawn;
    }

    // Update is called once per frame

    void FaceNode(Transform node)
    {

        Vector2 direction = new Vector2(node.position.x - enemy.position.x, node.position.y - enemy.position.y);
        enemy.up = Vector3.Slerp(enemy.up, direction, Time.deltaTime * rotSpeed);
    }

    void Move()
    {
        rb.velocity = new Vector2(moveSpeed*Mathf.Cos(enemy.localEulerAngles.z),moveSpeed*Mathf.Sin(enemy.localEulerAngles.z));
        //print(moveSpeed*Mathf.Cos(enemy.localEulerAngles.z));
        //print(enemy.rotation.z);
    }

    private bool ReachedNode(Transform node)
    {

        if (Mathf.Abs(node.position.x - enemy.position.x) < delta  && Mathf.Abs(node.position.y - enemy.position.y) < delta)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void should_turn_back()
    {
        if(i >= nodes.Length - 1)
        {
            i = nodes.Length - 1;
            is_at_end_of_path = true;
        }
        if (i <= 0)
        {
            i = 0;
            is_at_end_of_path = false;
        }
    }

    void Update()
    {
        //print(i);
        Vector2 direction = new Vector2(player.position.x - enemy.position.x, player.position.y - enemy.position.y);

        RaycastHit2D hit = Physics2D.Raycast(enemy.position, direction, Mathf.Infinity);
        if (hit.collider.tag == player.tag)
        {
            Debug.DrawRay(enemy.position, direction * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            moveSpeed = 0;
            enemy.up = Vector3.Slerp(enemy.up, direction, rotSpeed * Time.deltaTime);
        }
        else
        {
            Debug.DrawRay(enemy.position, direction * 100, Color.white);
            Debug.Log("Did not Hit");
            moveSpeed = 2;
            FaceNode(nodes[i]);
        }
    }

    void FixedUpdate()
    {
        Move();

        
        

        should_turn_back();
        if (ReachedNode(nodes[i]))
        {
            if (is_at_end_of_path)
            {
                i--;
            }
            else
            {
                i++;
            }
        }
    }


}
