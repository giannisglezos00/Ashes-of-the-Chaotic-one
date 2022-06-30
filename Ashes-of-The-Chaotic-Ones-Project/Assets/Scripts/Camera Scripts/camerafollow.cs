using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float damping;
    public Camera cam;
    public float defaultcamzoom = 1.285f;
    public float zoomoutcamzoom = 2f;
    public float zoomspeed;
    private Vector3 velocity = Vector3.zero;

    public bool zoomactive;

    void FixedUpdate()
    {
        Vector3 movePosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, damping);

    }
    void Update()
    {
        if ( Input.GetKey(KeyCode.LeftShift) )
        {
            zoomactive = true;
        }
        else
        {
            zoomactive = false;
        }

       
    }
    private void LateUpdate()
    {
        if(zoomactive )
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomoutcamzoom, zoomspeed);
        }
        
        else
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, defaultcamzoom, zoomspeed);
        }
    }
}
