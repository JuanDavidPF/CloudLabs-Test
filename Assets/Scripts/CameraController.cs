using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CameraController : MonoBehaviour
{
    Transform _t;
    Rigidbody _rb;
    Vector3 newPosition;

    [SerializeField] private float cameraSpeed = 10000;

    private void Awake()
    {
        Physics.IgnoreLayerCollision(6, 7);
        Physics.IgnoreLayerCollision(7, 8);
        _t = transform;
        _rb = GetComponent<Rigidbody>();
    }//Closes Awake method


    void Update()
    {
        newPosition = _t.position;
        _rb.AddForce(Input.GetAxis("Vertical") * Time.deltaTime * cameraSpeed * transform.forward, ForceMode.Force);
        _rb.AddForce(Input.GetAxis("Horizontal") * Time.deltaTime * cameraSpeed * transform.right, ForceMode.Force);


        _t.position = newPosition;
    }//Closes Update method
}//Closes CameraController class
