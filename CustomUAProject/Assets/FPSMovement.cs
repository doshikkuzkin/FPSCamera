using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMovement : MonoBehaviour
{
    private Transform _transform;
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float rotateSpeed = 100f;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        var forwardMove = _transform.forward * (Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime);
        var horizontalMove = _transform.right * (Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime);
        var horizontalRotation = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
        _transform.position = _transform.position + forwardMove + horizontalMove;
        _transform.rotation = _transform.rotation * Quaternion.Euler(new Vector3(0, horizontalRotation, 0));
    }
}
