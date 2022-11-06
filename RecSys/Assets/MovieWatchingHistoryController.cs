using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieWatchingHistoryController : MonoBehaviour
{
    public ImageLoader Poster1;
    public ImageLoader Poster2;
    public ImageLoader Poster3;
    public ImageLoader Poster4;
    public ImageLoader Poster5;
    public ImageLoader Poster6;
    public ImageLoader Poster7;
    public ImageLoader Poster8;
    public ImageLoader Poster9;
    public ImageLoader Poster10;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateList(List<string> urls, List<string> names)
    {
        for (int i = 0; i < 10; i++)
        {
            if (i == 0)
            {
                if (urls[i] != "")
                {
                    Poster1.ChangeImage(urls[i]);
                    TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("MovieName" + (i + 1).ToString()).GetComponent<TMPro.TextMeshPro>();
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
                    Poster2.ChangeImage(urls[i]);
                    TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("MovieName" + (i + 1).ToString()).GetComponent<TMPro.TextMeshPro>();
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
                    Poster3.ChangeImage(urls[i]);
                    TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("MovieName" + (i + 1).ToString()).GetComponent<TMPro.TextMeshPro>();
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
                    Poster4.ChangeImage(urls[i]);
                    TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("MovieName" + (i + 1).ToString()).GetComponent<TMPro.TextMeshPro>();
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
                    Poster5.ChangeImage(urls[i]);
                    TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("MovieName" + (i + 1).ToString()).GetComponent<TMPro.TextMeshPro>();
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
                    Poster6.ChangeImage(urls[i]);
                    TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("MovieName" + (i + 1).ToString()).GetComponent<TMPro.TextMeshPro>();
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
                    Poster7.ChangeImage(urls[i]);
                    TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("MovieName" + (i + 1).ToString()).GetComponent<TMPro.TextMeshPro>();
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
                    Poster8.ChangeImage(urls[i]);
                    TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("MovieName" + (i + 1).ToString()).GetComponent<TMPro.TextMeshPro>();
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
                    Poster9.ChangeImage(urls[i]);
                    TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("MovieName" + (i + 1).ToString()).GetComponent<TMPro.TextMeshPro>();
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
                    Poster10.ChangeImage(urls[i]);
                    TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("MovieName" + (i + 1).ToString()).GetComponent<TMPro.TextMeshPro>();
                    textMeshPro.text = names[i];
                }
                else
                {
                    break;
                }
            }


        }
    }

    public void UpdatePageNum(int current, int total)
    {
        TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("PageText").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = current.ToString() + "/" + total.ToString();
    }
}
