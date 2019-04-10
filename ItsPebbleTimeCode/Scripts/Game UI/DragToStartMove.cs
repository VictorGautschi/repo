using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragToStartMove : MonoBehaviour {

    public Transform target;
    float speed = 9.0f;

    Vector3 originalPosition;

    private void Start()
    {
        originalPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        if (!GameControl.control.firstTime)
        {
            gameObject.SetActive(false);
        }

        speed = 6.0f;
    }

    void Update () {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed);

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, target.position) < 0.001f)
        {
            // Swap the position of the cylinder.
            target.position = originalPosition;
            originalPosition = transform.position;
        }
	}
}
