using System;
using System.Collections;
using System.Collections.Generic;
using Saves;
using UnityEngine;
using UnityEngine.Serialization;

public class FPSMovement : MonoBehaviour
{
    private Transform _transform;
    private Quaternion _cameraRot;
    private Transform _cameraTransform;
    private readonly float rotationClamp = 40;
    [SerializeField] private Camera _mainCamera;
    private float _defaultZoom;
    private float _cutZoom;
    private readonly float zoomSpeed = 4f;
    [SerializeField] private Camera gunCamera;
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float rotateSpeed = 100f;
    [SerializeField] private ControllAxis controllAxis;
    private string verticalAxis, horizontalAxis;
    public bool mouseActive = true;

    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }

    private void Awake()
    {
        _transform = transform;
        SetAxis(controllAxis);
    }

    private void Start()
    {
        _cameraTransform = _mainCamera.transform;
        _cameraRot = _cameraTransform.rotation;
        _defaultZoom = _mainCamera.fieldOfView;
        _cutZoom = _defaultZoom / 2;
    }

    private void Update()
    {
        var forwardMove = _transform.forward * (Input.GetAxis(verticalAxis) * moveSpeed * Time.deltaTime);
        var horizontalMove = _transform.right * (Input.GetAxis(horizontalAxis) * moveSpeed * Time.deltaTime);
        
        _transform.position = _transform.position + forwardMove + horizontalMove;

        if (mouseActive)
        {
            var horizontalRotation = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
            _transform.rotation *= Quaternion.Euler(new Vector3(0, horizontalRotation, 0));

            var verticalRotation = -Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;
            _cameraRot *= Quaternion.Euler(new Vector3(verticalRotation, 0, 0));

            _cameraRot = LockCameraMovement(_cameraRot);
            _cameraTransform.localRotation = _cameraRot;

            float currentZoom = _mainCamera.fieldOfView;

            if (Input.GetMouseButton(1))
            {
                _mainCamera.fieldOfView = Mathf.Lerp(currentZoom, _cutZoom, zoomSpeed * Time.deltaTime);
                gunCamera.fieldOfView = Mathf.Lerp(currentZoom, _cutZoom, zoomSpeed * Time.deltaTime);
            }
            else
            {
                _mainCamera.fieldOfView = Mathf.Lerp(currentZoom, _defaultZoom, zoomSpeed * Time.deltaTime);
                gunCamera.fieldOfView = Mathf.Lerp(currentZoom, _defaultZoom, zoomSpeed * Time.deltaTime);
            }
        }
    }
    
    private Quaternion LockCameraMovement(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;
 
        var angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
        angleX = Mathf.Clamp(angleX, -rotationClamp, rotationClamp);
        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);
 
        return q;
    }

    private void SetAxis(ControllAxis _controllAxis)
    {
        switch (_controllAxis)
        {
            case ControllAxis.Arrows:
                verticalAxis = "ArrowsUD";
                horizontalAxis = "ArrowsLR";
                break;
            case ControllAxis.WASD:
                verticalAxis = "WS";
                horizontalAxis = "AD";
                break;
        }
    }
}

public enum ControllAxis
{
    Arrows,
    WASD
}
