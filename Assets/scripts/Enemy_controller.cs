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
    public Collider2D col;
    public Animator animator;
    public Animator player_anim;
    public SpriteRenderer alert;
    public float moveSpeed = 2;
    public float rotSpeed;
    private double delta = 0.2;
    private bool is_at_end_of_path = false;
    private bool can_enable_alert;
    private int i = 0;
    float hit_range = 1.5f;
    float timer = 0f;
    void Start()
    {
        can_enable_alert = true;
        alert.enabled = false;
        enemy = spawn;
    }

    // Update is called once per frame

    void FaceNode(Transform node)
    {

        Vector2 direction = new Vector2(node.position.x - enemy.position.x, node.position.y - enemy.position.y);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        Quaternion target_rot = Quaternion.AngleAxis(angle, Vector3.forward);
        enemy.rotation = Quaternion.Slerp(enemy.rotation, target_rot, rotSpeed*Time.deltaTime);
    }

    void Move()
    {

        enemy.position += enemy.up * moveSpeed * Time.deltaTime;
    }

    private bool ReachedNode(Transform node)
    {
        
        if (Vector2.Distance(node.position, enemy.position) < .1f)
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
        if (i >= nodes.Length - 1)
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

    IEnumerator Wait_Then_Attack(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            if (!animator.GetBool("is_dead"))
            {
                animator.SetBool("is_attacking", true);
                if (combat_handler.Melee_combat_handler(enemy, player, "player"))
                {
                    player_anim.SetBool("is_dead", true);
                }
            }

        }

    }


    IEnumerator Wait_Then_run(float time, Quaternion target_rot)
    {
        enemy.rotation = target_rot;
        moveSpeed = 0;
        animator.SetBool("is_moving", false);
        if (can_enable_alert)
        {
            alert.enabled = true;
        }
        yield return new WaitForSeconds(time);
        alert.enabled = false;
        if (!animator.GetBool("is_dead"))
        {

            if (Vector2.Distance(enemy.position, player.position) < hit_range)
            {
                animator.SetBool("is_moving", false);

                moveSpeed = 0;
                StartCoroutine(Wait_Then_Attack(.5f));
            }
            else
            {
                animator.SetBool("is_moving", true);
                moveSpeed = 10;
                animator.SetBool("is_attacking", false);
            }

        }
        can_enable_alert = false;

    }

    void combat()
    {

        Vector2 direction = new Vector2(player.position.x - enemy.position.x, player.position.y - enemy.position.y);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        Quaternion target_rot = Quaternion.AngleAxis(angle, Vector3.forward);

        int mask = 1 << LayerMask.NameToLayer("player");
        mask |= 1 << LayerMask.NameToLayer("Default");

        RaycastHit2D hit = Physics2D.Raycast(enemy.position, direction, Mathf.Infinity, mask);
        RaycastHit2D forward = Physics2D.Raycast(enemy.position, enemy.up, Mathf.Infinity);
        if (hit.collider.tag == player.tag)
        {
            Debug.DrawRay(enemy.position, direction * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
            if (Vector2.Angle(direction, enemy.up) < 80)
            {

                StartCoroutine(Wait_Then_run(.5f, target_rot));
            }
            else
            {
                animator.SetBool("is_attacking", false);
                can_enable_alert = true;
                moveSpeed = 3;
                FaceNode(nodes[i]);

            }
        }
        else
        {
            Debug.DrawRay(enemy.position, direction * 100, Color.white);
            Debug.DrawRay(enemy.position, enemy.up * 100, Color.white);
            //moveSpeed = 2;
            FaceNode(nodes[i]);
            animator.SetBool("is_moving", true);
        }

    }

    void Update()
    {
        if (!animator.GetBool("is_dead"))
        {


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

            combat();

        }
    }

   

    void FixedUpdate()
    {
        if (animator.GetBool("is_dead"))
        {
            if(rb != null)
            {
                Vector2 direction = new Vector2(player.position.x - enemy.position.x, player.position.y - enemy.position.y);
                enemy.up = direction;

                Destroy(rb);
                Destroy(col);
            }
        }
        else
        {

            Move();




            timer = Time.time;
        }
    }


}
