using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReasonActorFullListController : MonoBehaviour
{
    public ImageLoader actorPoster1;
    public ImageLoader actorPoster2;
    public ImageLoader actorPoster3;
    public ImageLoader actorPoster4;
    public ImageLoader actorPoster5;
    public ImageLoader actorPoster6;
    public ImageLoader actorPoster7;
    public ImageLoader actorPoster8;
    public ImageLoader actorPoster9;
    public ImageLoader actorPoster10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateActorFullList(List<string> urls, List<string> names)
    {
        for(int i = 0; i < 10; i++)
        {
            if (i == 0)
            {
                if (urls[i] != "")
                {
                    actorPoster1.ChangeImage(urls[i]);
                    TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("ActorName" + (i + 1).ToString()).GetComponent<TMPro.TextMeshPro>();
                    textMeshPro.text = names[i];
                }
                else
                {
                    break;
                }
            }
            else if (i == 1)
            {
                if (urls[i] != "")
                {
                    actorPoster2.ChangeImage(urls[i]);
                    TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("ActorName" + (i + 1).ToString()).GetComponent<TMPro.TextMeshPro>();
                    textMeshPro.text = names[i];
                }
                else
                {
                    break;
                }
            }
            else if (i == 2)
            {
                if (urls[i] != "")
                {
                    actorPoster3.ChangeImage(urls[i]);
                    TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("ActorName" + (i + 1).ToString()).GetComponent<TMPro.TextMeshPro>();
                    textMeshPro.text = names[i];
                }
                else
                {
                    break;
                }
            }
            else if (i == 3)
            {
                if (urls[i] != "")
                {
                    actorPoster4.ChangeImage(urls[i]);
                    TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("ActorName" + (i + 1).ToString()).GetComponent<TMPro.TextMeshPro>();
                    textMeshPro.text = names[i];
                }
                else
                {
                    break;
                }
            }
            else if (i == 4)
            {
                if (urls[i] != "")
                {
                    actorPoster5.ChangeImage(urls[i]);
                    TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("ActorName" + (i + 1).ToString()).GetComponent<TMPro.TextMeshPro>();
                    textMeshPro.text = names[i];
                }
                else
                {
                    break;
                }
            }
            else if (i == 5)
            {
                if (urls[i] != "")
                {
                    actorPoster6.ChangeImage(urls[i]);
                    TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("ActorName" + (i + 1).ToString()).GetComponent<TMPro.TextMeshPro>();
                    textMeshPro.text = names[i];
                }
                else
                {
                    break;
                }
            }
            else if (i == 6)
            {
                if (urls[i] != "")
                {
                    actorPoster7.ChangeImage(urls[i]);
                    TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("ActorName" + (i + 1).ToString()).GetComponent<TMPro.TextMeshPro>();
                    textMeshPro.text = names[i];
                }
                else
                {
                    break;
                }
            }
            else if (i == 7)
            {
                if (urls[i] != "")
                {
                    actorPoster8.ChangeImage(urls[i]);
                    TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("ActorName" + (i + 1).ToString()).GetComponent<TMPro.TextMeshPro>();
                    textMeshPro.text = names[i];
                }
                else
                {
                    break;
                }
            }
            else if (i == 8)
            {
                if (urls[i] != "")
                {
                    actorPoster9.ChangeImage(urls[i]);
                    TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("ActorName" + (i + 1).ToString()).GetComponent<TMPro.TextMeshPro>();
                    textMeshPro.text = names[i];
                }
                else
                {
                    break;
                }
            }
            else if (i == 9)
            {
                if (urls[i] != "")
                {
                    actorPoster10.ChangeImage(urls[i]);
                    TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("ActorName" + (i + 1).ToString()).GetComponent<TMPro.TextMeshPro>();
                    textMeshPro.text = names[i];
                }
                else
                {
                    break;
                }
            }

            
        }
    }
}
