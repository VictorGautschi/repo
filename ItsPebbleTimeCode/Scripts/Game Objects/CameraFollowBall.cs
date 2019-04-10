using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowBall : MonoBehaviour {

    public GameObject player;
    private Vector3 _offset;
    public float smoothSpeed = 0.125f;

    Vector3 velocity = Vector3.one;

    private void Start()
    {
        _offset = transform.position - player.transform.position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // Now we destroy the ball when it hits a bomb so have to check for null here
        if(player != null)
        {
            Vector3 desiredPosition = player.transform.position + _offset;
            //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}
