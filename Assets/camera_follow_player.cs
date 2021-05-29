using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_follow_player : MonoBehaviour
{
    public Transform playerObject;
    public float distanceFromObject;

    void Start()
    {
    }
    void Update()
    {
        Vector3 raisedPlayerPos = playerObject.position;
        raisedPlayerPos.z = -10;
        transform.position = raisedPlayerPos;

        
        
    }
}

