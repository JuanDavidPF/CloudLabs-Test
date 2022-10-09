using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Camera))]
public class CameraZoom : MonoBehaviour
{

    [SerializeField] private LayerMask inputLayerMask;
    Camera _cam;
    [SerializeField] float zoomSpeed;

    private void Awake()
    {
        _cam = GetComponent<Camera>();
        _cam.eventMask = inputLayerMask;
    }

    void Update()
    {
        _cam.orthographicSize += Input.GetAxis("Mouse ScrollWheel") * -zoomSpeed;
    }
}
