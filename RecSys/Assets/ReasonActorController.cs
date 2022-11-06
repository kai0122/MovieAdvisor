using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReasonActorController : MonoBehaviour
{
    public ImageLoader actorPoster1;
    public ImageLoader actorPoster2;
    public ImageLoader actorPoster3;

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
        actorPoster1.ChangeImage(url1);
        actorPoster2.ChangeImage(url2);
        actorPoster3.ChangeImage(url3);
    }

    public void UpdatePoster(string url1, string url2)
    {
        actorPoster1.ChangeImage(url1);
        actorPoster2.ChangeImage(url2);
    }

    public void UpdatePoster(string url1)
    {
        actorPoster1.ChangeImage(url1);
    }
}
