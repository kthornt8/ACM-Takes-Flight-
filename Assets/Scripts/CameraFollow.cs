using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the rocket's transform
    public float smoothSpeed = 0.5f; // Speed at which the camera follows the rocket

    private Vector3 initialOffset; // Initial offset between the rocket and camera

    private void Start()
    {
        initialOffset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        Vector3 targetPosition = target.position + initialOffset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
    }
}