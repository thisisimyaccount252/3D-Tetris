using Assets.Scripts.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    
    public GameObject PivotPoint;
    public float RotationSpeed;

    public float ScrollSpeed;
    public ZoomType ZoomType;

    #region Privates
    private Vector3 startingOffset;
    private Vector3 currentOffset;
    private Quaternion startingRotation;
    private Vector3 startingPosition;
    
    private float minFov = 15;
    private float maxFov = 90;
    //private float cameraDistanceMin = 5;
    //private float cameraDistanceMax = 20;
    //private float cameraDistance = 10;

    #endregion Privates

    #region Controls
    private KeyCode resetCameraKey = KeyCode.R;
    private int rotateCameraMouseButton = 2;
    #endregion Controls

    // Use this for initialization
    void Start () {
        transform.LookAt(PivotPoint.transform);

        // This is the space between the Pivot Point and the camera
        startingOffset = transform.position - PivotPoint.transform.position;
        currentOffset = startingOffset;

        // resetting things
        startingRotation = transform.rotation;
        startingPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(rotateCameraMouseButton))
        {
            RotateCamera();
        }

        if (Input.GetKey(resetCameraKey))
        {
            ResetCamera();
        }

        Zoom(ZoomType);
	}

    void RotateCamera()
    {
        transform.LookAt(PivotPoint.transform);
        transform.RotateAround(PivotPoint.transform.position, Vector3.up, Input.GetAxis("Mouse X") * RotationSpeed);
    }

    void ResetCamera()
    {
        // Reset Camera position and rotation
        transform.rotation = startingRotation;
        transform.position = PivotPoint.transform.position + startingOffset;
    }

    private void Zoom(ZoomType type)
    {
        if (type == ZoomType.FieldOfView)
        {
            FieldOfViewZoom();
        }
        else if (type == ZoomType.MoveCamera)
        {
            CameraZoom();
        }
    }


    /// <summary>
    /// Changes the FOV for a "Zoom" effect
    /// </summary>
    // TODO: Experiement with actually moving the camera instead of just changing the FOV
    private void FieldOfViewZoom()
    {
        float fov = Camera.main.fieldOfView;

        fov += Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;

        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;
    }

    /// <summary>
    /// Moves the camera closer to and further from the Pivot Point.
    /// </summary>
    private void CameraZoom()
    {
        var scrollInput = Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;

        var currentCameraDistance = currentOffset.magnitude;
        Debug.Log(string.Format("Old Magnitude: {0}", currentCameraDistance));
        if (scrollInput != 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, PivotPoint.transform.position, scrollInput);
        }
    }
}
