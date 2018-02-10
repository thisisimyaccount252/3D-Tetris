using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject PivotPoint;
    public float RotationSpeed;

    private Vector3 offset;
    private Quaternion startingRotation;
    private Vector3 startingPosition;

    #region Controls
    private KeyCode resetCameraKey = KeyCode.R;
    private int rotateCameraMouseButton = 2;
    #endregion Controls

    // Use this for initialization
    void Start () {
        transform.LookAt(PivotPoint.transform);

        // This is the space between the Pivot Point and the camera
        offset = transform.position - PivotPoint.transform.position;

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
        transform.position = PivotPoint.transform.position + offset;
    }
}
