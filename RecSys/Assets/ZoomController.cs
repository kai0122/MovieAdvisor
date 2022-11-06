using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CameraPlane == null)
        {
            GameObject cameraPlane = GameObject.Find("BackgroundPlane");
            if (cameraPlane != null)
            {
                CameraPlane = cameraPlane;
                gameObject.SetActive(false);
            }
        }
    }

    GameObject CameraPlane = null;
    Vector3 startPosition = Vector3.zero;

    //public GameObject Target = null;
    Vector3 TargetStartPosition = Vector3.zero;
    Vector3 TargetStartScale = Vector3.zero;

    public void AlterZoom(float _zoomParam)
    {
        if (startPosition == Vector3.zero)
        {
            startPosition = CameraPlane.transform.localPosition;
        }

        if (CameraPlane != null)
        {
            CameraPlane.transform.localPosition = new Vector3(startPosition.x, startPosition.y, startPosition.z - _zoomParam * 1000.0f);
        }

        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject Target in targets)
        {
            if (TargetStartPosition == Vector3.zero)
            {
                TargetStartPosition = Target.transform.localPosition;
            }

            if (TargetStartScale == Vector3.zero)
            {
                TargetStartScale = Target.transform.localScale;
            }

            if (Target != null)
            {
                Target.transform.localPosition = new Vector3(TargetStartPosition.x - _zoomParam * 0.4f, TargetStartPosition.y, TargetStartPosition.z);
                Target.transform.localScale = new Vector3(TargetStartScale.x + TargetStartScale.x * _zoomParam,
                                                          TargetStartScale.y + TargetStartScale.y * _zoomParam,
                                                          TargetStartScale.z + TargetStartScale.z * _zoomParam);
            }
        }
    }
}
