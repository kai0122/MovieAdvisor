using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReasonSimilarController : MonoBehaviour
{
    public ImageLoader moviePoster1;
    public ImageLoader moviePoster2;
    public ImageLoader moviePoster3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePoster(string url1, string url2, string url3)
    {
        moviePoster1.ChangeImage(url1);
        moviePoster2.ChangeImage(url2);
        moviePoster3.ChangeImage(url3);
    }

    public void UpdatePoster(string url1, string url2)
    {
        moviePoster1.ChangeImage(url1);
        moviePoster2.ChangeImage(url2);
    }

    public void UpdatePoster(string url1)
    {
        moviePoster1.ChangeImage(url1);
    }
}
