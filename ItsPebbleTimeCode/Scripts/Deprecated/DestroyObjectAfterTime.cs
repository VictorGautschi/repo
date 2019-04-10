using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectAfterTime : MonoBehaviour
{
    
    float time = 10.0f;

    void Update()
    {
        if (Time.timeSinceLevelLoad >= time)
        {
            Destroy(gameObject);
        }
    }
}
