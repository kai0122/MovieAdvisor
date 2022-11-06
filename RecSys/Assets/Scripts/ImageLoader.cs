using System.Collections;
using System.Collections.Generic;
using TMDbLib.Objects.General;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour
{
    private string url = "https://scontent-bru2-1.cdninstagram.com/v/t51.2885-15/e35/p1080x1080/121358734_832832167462810_5086468503339664830_n.jpg?_nc_ht=scontent-bru2-1.cdninstagram.com&_nc_cat=110&_nc_ohc=4SFaFv6O8HEAX8K_qd4&tp=19&oh=03f1145353ec3c92412684fde1dc9755&oe=5FB35AFE";
    //public UnityEngine.UI.RawImage rawImage;
    private int movingTime;
    private int movingCount;
    private Vector3 distance;
    private Vector3 originalPosition;

    private Touch touch;
    public bool hitImage;

    void Start()
    {
        StartCoroutine(DownloadImage(url));

        movingTime = 0;
        movingCount = 0;
        originalPosition = gameObject.transform.localPosition;

        hitImage = false;
    }

    private void Update()
    {
#pragma warning disable CS0618 // 類型或成員已經過時
        if (GameObject.Find("Filter") == null)
        {
#pragma warning restore
            OnImageZoomDetection();
        }
    }

    private void OnImageZoomDetection()
    {
        if (movingTime != 0)
        {
            if (movingCount < movingTime)
            {
                gameObject.transform.localPosition += distance;
                movingCount++;
            }
            else
            {
                movingCount = 0;
                movingTime = 0;
            }
        }

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

                if (Physics.Raycast(ray, out hit))
                {
                    //explanationController.ShowNextMovieForSelection();
#pragma warning disable CS0618 // 類型或成員已經過時
                    if (hit.transform.gameObject.active)
                    {
#pragma warning restore
                        if (hit.transform.gameObject.tag == "Poster")
                        {
                            RawImage arFrontImage = GameObject.Find("ARFrontImage").GetComponent<RawImage>();
                            arFrontImage.enabled = true;
                            arFrontImage.material = new Material(Shader.Find("Unlit/Transparent"));
                            arFrontImage.material.mainTexture = hit.transform.gameObject.GetComponent<ImageLoader>().myImageTexture;
                        }
                    }
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                RawImage arFrontImage = GameObject.Find("ARFrontImage").GetComponent<RawImage>();
                if (arFrontImage.enabled == true)
                {
                    arFrontImage.enabled = false;
                }
            }

        }

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                //explanationController.ShowNextMovieForSelection();
#pragma warning disable CS0618 // 類型或成員已經過時
                if (hit.collider != null)
                {
#pragma warning restore
                    if (hit.transform.gameObject.tag == "Poster")
                    {
                        GameObject hitGameObject = (GameObject)EditorUtility.InstanceIDToObject(hit.transform.gameObject.GetInstanceID());

                        RawImage arFrontImage = GameObject.Find("ARFrontImage").GetComponent<RawImage>();
                        arFrontImage.enabled = true;
                        arFrontImage.material = new Material(Shader.Find("Unlit/Transparent"));
                        arFrontImage.material.mainTexture = hit.transform.gameObject.GetComponent<ImageLoader>().myImageTexture;
                    }
                }
            }
        }

#endif
    }

    public void ChangeImage(string _newUrl)
    {
        if (_newUrl != "")
        {
            url = _newUrl;
            Destroy(GetComponent<Renderer>().material.mainTexture);
            StartCoroutine(DownloadImage(url));
        }
    }

    public void RotateImage(int _angle)
    {
        if (_angle > 0)
        {
            movingTime = _angle;
            gameObject.transform.localPosition = originalPosition;
            distance = (new Vector3(-0.8f, 0, 6) - gameObject.transform.localPosition) / movingTime;
        }
        else
        {
            _angle = -_angle;
            movingTime = _angle;
            distance = (originalPosition - gameObject.transform.localPosition) / movingTime;
        }
    }

    private Texture myImageTexture = null;
    IEnumerator DownloadImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
        {
            Renderer rend = GetComponent<Renderer>();
            rend.material = new Material(Shader.Find("Unlit/Transparent"));
            rend.material.mainTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            myImageTexture = rend.material.mainTexture;
        }
    }
}
