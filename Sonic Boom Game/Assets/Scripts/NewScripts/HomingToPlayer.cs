using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingToPlayer : MonoBehaviour
{
    Transform player;

    Vector3 playerPos;

    void Start()
    {
        //translate the local position into world position.
        transform.TransformPoint(0, 1, 0.5f);

        player = GameObject.Find("Bullet_HomingPos").transform;

        playerPos = new Vector3(player.position.x, player.position.y, player.position.z);
        //transform.rotation = Quaternion.LookRotation(playerPos);
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerPos, 3f * Time.deltaTime);
    }
}
