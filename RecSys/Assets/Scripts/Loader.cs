using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public ImageLoader imageLoader;
    public TextLoader textLoader;
    public VideoLoader videoLoader;

    // Start is called before the first frame update
    void Start()
    {
        imageLoader = null;
        textLoader = null;
        videoLoader = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (imageLoader == null)
        {
            try
            {
                imageLoader = gameObject.transform.Find("MoviePoster").GetComponent<ImageLoader>();
                textLoader = gameObject.transform.Find("MovieInformation").GetComponent<TextLoader>();
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

    public ImageLoader GetImageLoader()
    {
        if (imageLoader == null)
        {
            imageLoader = gameObject.transform.Find("MoviePoster").GetComponent<ImageLoader>();
        }
        return imageLoader;
    }

    public TextLoader GetTextLoader()
    {
        if (textLoader == null)
        {
            textLoader = gameObject.transform.Find("MovieInformation").GetComponent<TextLoader>();
        }
        return textLoader;
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
