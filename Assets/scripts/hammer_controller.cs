using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hammer_controller : MonoBehaviour
{
    public SpriteRenderer hammer_sprite;
    public GameObject player;
    public Rigidbody2D rb;
    public float force;
    public float rot_force;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("player");

        hammer_sprite.enabled = true;
        rb.AddForce(player.transform.up * force);
        rb.AddTorque(rot_force);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
