using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    float camMin = 15f;
    float camMax = 100f;
    float sensitivty = 10f;


    Vector3 worldPosition;
    Vector3 mouseMoveLastPoint;

    Vector3 camOffset;

    void Update()
    {
        float size = cam.orthographicSize;
        size += Input.GetAxis("Mouse ScrollWheel") * sensitivty * -1;
        size = Mathf.Clamp(size, camMin, camMax);
        cam.orthographicSize = size;
        //cam.orthographicSize = Input.GetAxis("Mouse ScrollWheel");

        if (Input.GetMouseButton(2))
        {
            Vector3 mouseMoveStartPoint = Input.mousePosition;
            Vector3 mouseDelta = cam.ScreenToWorldPoint(mouseMoveLastPoint) - cam.ScreenToWorldPoint(mouseMoveStartPoint);
            cam.transform.position += mouseDelta;
        }

        mouseMoveLastPoint = Input.mousePosition;
    }
}
