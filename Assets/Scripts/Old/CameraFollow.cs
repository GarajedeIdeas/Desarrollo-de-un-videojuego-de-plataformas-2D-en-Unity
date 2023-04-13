using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothTargetTime;//representa el tiempo que va a tardar la cámara en llegar a su destino

    Vector3 offset;
    Vector3 smoothDampVelocity;
    void Start()
    {
        offset = transform.position - player.position;    
    }

    void Update()
    {
        Vector3 targetCamera = player.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, 
            targetCamera,ref smoothDampVelocity, smoothTargetTime);
    }
}
