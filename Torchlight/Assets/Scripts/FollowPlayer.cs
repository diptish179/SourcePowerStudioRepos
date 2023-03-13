using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FollowPlayer : MonoBehaviour
{
    //public Transform player;
    //public Vector2 offset;

    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void Update()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // for smooth camera follow
        transform.position = smoothedPosition;
    }

  
    //   transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);

}
