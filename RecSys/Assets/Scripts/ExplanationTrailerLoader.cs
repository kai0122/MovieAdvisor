using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplanationTrailerLoader : MonoBehaviour
{
    public VideoLoader videoLoader;
    
    // Start is called before the first frame update
    void Start()
    {
        videoLoader = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (videoLoader == null)
        {
            try
            {
                videoLoader = gameObject.transform.Find("MovieTrailer").GetComponent<VideoLoader>();
            }
            catch (NullReferenceException e)
            {
                Debug.Log(e);
            }
        }
    }

    public void SetActive(bool _bool)
    {
        gameObject.SetActive(_bool);
    }

    public VideoLoader GetVideoLoader()
    {
        if (videoLoader == null)
        {
            videoLoader = gameObject.transform.Find("MovieTrailer").GetComponent<VideoLoader>();
        }
        return videoLoader;
    }
}
