using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieRecommendationActorDetailController : MonoBehaviour
{
    public ImageLoader moviePoster1;
    public ImageLoader moviePoster2;
    public ImageLoader moviePoster3;

    public GameObject fullList1;
    public GameObject fullList2;
    public GameObject fullList3;

    public float startX;
    public float startY;
    public float startZ;
    public float endX;
    public float endY;
    public float endZ;
    private float shiftX = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OpenActorJustification_ing)
        {
            if (Vector3.Distance(gameObject.transform.localPosition, new Vector3(endX, endY, endZ)) > shiftX)
            {
                gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x + shiftX,
                                                                 gameObject.transform.localPosition.y,
                                                                 gameObject.transform.localPosition.z);
            }
            else
            {
                gameObject.transform.localPosition = new Vector3(endX, endY, endZ);
                OpenActorJustification_ing = false;
            }
        }
        
        if (CloseActorJustification_ing)
        {
            if (Vector3.Distance(gameObject.transform.localPosition, new Vector3(startX, startY, startZ)) > shiftX)
            {
                gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x - shiftX,
                                                                 gameObject.transform.localPosition.y,
                                                                 gameObject.transform.localPosition.z);
            }
            else
            {
                gameObject.transform.localPosition = new Vector3(startX, startY, startZ);
                CloseActorJustification_ing = false;
                gameObject.SetActive(false);
            }
        }
    }

    public bool OpenActorJustification_ing = false;
    public bool CloseActorJustification_ing = false;

    public void OpenActorJustification()
    {
        gameObject.SetActive(true);
        //OpenActorJustification_ing = true;
    }

    public void CloseActorJustification()
    {
        //gameObject.SetActive(true);
        //CloseActorJustification_ing = true;
        gameObject.SetActive(false);
    }

    public void UpdateMovieNumber(string _movieName, int _sum, string num1 = "0", string num2 = "0", string num3 = "0")
    {
        gameObject.transform.Find("ActorHint").GetComponent<TMPro.TextMeshPro>().text = "The Amount of movies performed by actors in movies that you liked that also performed in " + _movieName.ToUpper() + ": " + _sum.ToString();

        if (num1 == "0")
        {
            fullList1.SetActive(false);
            gameObject.transform.Find("MovieName1").GetComponent<TMPro.TextMeshPro>().text = "";
            moviePoster1.gameObject.SetActive(false);
            gameObject.transform.Find("ActorMore1").transform.gameObject.SetActive(false);

            gameObject.transform.Find("Text").GetComponent<TMPro.TextMeshPro>().text = "Because you watched 0 movies acted by any actors/actresses in this movie.";
        }
        else
        {
            fullList1.SetActive(true);
            fullList1.GetComponent<TMPro.TextMeshPro>().text = "(" + num1 + " movie)";
            moviePoster1.gameObject.SetActive(true);
            gameObject.transform.Find("ActorMore1").transform.gameObject.SetActive(true);
        }

        if (num2 == "0")
        {
            fullList2.SetActive(false);
            gameObject.transform.Find("MovieName2").GetComponent<TMPro.TextMeshPro>().text = "";
            moviePoster2.gameObject.SetActive(false);
            gameObject.transform.Find("ActorMore2").transform.gameObject.SetActive(false);
        }
        else
        {
            fullList2.SetActive(true);
            fullList2.GetComponent<TMPro.TextMeshPro>().text = "(" + num2 + " movie)";
            moviePoster2.gameObject.SetActive(true);
            gameObject.transform.Find("ActorMore2").transform.gameObject.SetActive(true);
        }

        if (num3 == "0")
        {
            fullList3.SetActive(false);
            gameObject.transform.Find("MovieName3").GetComponent<TMPro.TextMeshPro>().text = "";
            moviePoster3.gameObject.SetActive(false);
            gameObject.transform.Find("ActorMore3").transform.gameObject.SetActive(false);
        }
        else
        {
            fullList3.SetActive(true);
            fullList3.GetComponent<TMPro.TextMeshPro>().text = "(" + num3 + " movie)";
            moviePoster3.gameObject.SetActive(true);
            gameObject.transform.Find("ActorMore3").transform.gameObject.SetActive(true);
        }
    }

    public void UpdateMovieNumber(string num1 = "0", string num2 = "0", string num3 = "0")
    {
        if (num1 == "0")
        {
            fullList1.SetActive(false);
            gameObject.transform.Find("MovieName1").GetComponent<TMPro.TextMeshPro>().text = "";
            moviePoster1.gameObject.SetActive(false);
            gameObject.transform.Find("ActorMore1").transform.gameObject.SetActive(false);

            gameObject.transform.Find("Text").GetComponent<TMPro.TextMeshPro>().text = "Because you watched 0 movies acted by any actors/actresses in this movie.";
        }
        else
        {
            fullList1.SetActive(true);
            fullList1.GetComponent<TMPro.TextMeshPro>().text = "(" + num1 + " movie)";
            moviePoster1.gameObject.SetActive(true);
            gameObject.transform.Find("ActorMore1").transform.gameObject.SetActive(true);
        }

        if (num2 == "0")
        {
            fullList2.SetActive(false);
            gameObject.transform.Find("MovieName2").GetComponent<TMPro.TextMeshPro>().text = "";
            moviePoster2.gameObject.SetActive(false);
            gameObject.transform.Find("ActorMore2").transform.gameObject.SetActive(false);
        }
        else
        {
            fullList2.SetActive(true);
            fullList2.GetComponent<TMPro.TextMeshPro>().text = "(" + num2 + " movie)";
            moviePoster2.gameObject.SetActive(true);
            gameObject.transform.Find("ActorMore2").transform.gameObject.SetActive(true);
        }

        if (num3 == "0")
        {
            fullList3.SetActive(false);
            gameObject.transform.Find("MovieName3").GetComponent<TMPro.TextMeshPro>().text = "";
            moviePoster3.gameObject.SetActive(false);
            gameObject.transform.Find("ActorMore3").transform.gameObject.SetActive(false);
        }
        else
        {
            fullList3.SetActive(true);
            fullList3.GetComponent<TMPro.TextMeshPro>().text = "(" + num3 + " movie)";
            moviePoster3.gameObject.SetActive(true);
            gameObject.transform.Find("ActorMore3").transform.gameObject.SetActive(true);
        }
    }

    public void UpdateName(string name1 = "", string name2 = "", string name3 = "")
    {
        gameObject.transform.Find("MovieName1").GetComponent<TMPro.TextMeshPro>().text = name1;
        gameObject.transform.Find("MovieName2").GetComponent<TMPro.TextMeshPro>().text = name2;
        gameObject.transform.Find("MovieName3").GetComponent<TMPro.TextMeshPro>().text = name3;
    }

    public void UpdatePoster(string url1 = "", string url2 = "", string url3 = "")
    {
        moviePoster1.ChangeImage(url1);
        moviePoster2.ChangeImage(url2);
        moviePoster3.ChangeImage(url3);
    }
}
