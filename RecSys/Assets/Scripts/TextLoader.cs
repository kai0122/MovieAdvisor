using System.Collections;
using System.Collections.Generic;
using TMDbLib.Objects.General;
using UnityEngine;
using UnityEngine.Networking;



public class TextLoader : MonoBehaviour
{
    private string text = "";
    //public UnityEngine.UI.RawImage rawImage;
    private int movingTime;
    private int movingCount;
    private Vector3 distance;
    private Vector3 originalPosition;

    void Start()
    {
        //TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("Text").GetComponent<TMPro.TextMeshPro>();
        //textMeshPro.text = text;
        //textMeshPro.color = Color.white;

        movingTime = 0;
        movingCount = 0;
        originalPosition = gameObject.transform.localPosition;
    }

    void Update()
    {
        if (movingTime != 0)
        {
            if (movingCount < movingTime)
            {
                //Debug.Log(movingCount);
                gameObject.transform.localPosition += distance;
                movingCount++;
            }
            else
            {
                movingCount = 0;
                movingTime = 0;
            }
        }
    }

    public void ChangeText(string _text)
    {
        text = _text;

        TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("Text").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = text;
        textMeshPro.color = Color.white;
    }

    public void RotateText(int _angle)
    {
        if (_angle > 0)
        {
            movingTime = _angle;
            gameObject.transform.localPosition = originalPosition;
            distance = (new Vector3(0.8f, 0, 6) - gameObject.transform.localPosition) / movingTime;
        }
        else
        {
            _angle = -_angle;
            movingTime = _angle;
            distance = (originalPosition - gameObject.transform.localPosition) / movingTime;
        }
        
    }
}
