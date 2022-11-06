using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieRecommendationPublicityDetailController : MonoBehaviour
{
    public ImageLoader moviePoster1;
    public ImageLoader moviePoster2;
    public ImageLoader moviePoster3;

    public GameObject moreSelection;

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
        if (OpenPublicityJustification_ing)
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
                OpenPublicityJustification_ing = false;
            }
        }

        if (ClosePublicityJustification_ing)
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
                ClosePublicityJustification_ing = false;
                gameObject.SetActive(false);
            }
        }
    }

    public bool OpenPublicityJustification_ing = false;
    public bool ClosePublicityJustification_ing = false;

    public void OpenPublicityJustification()
    {
        gameObject.SetActive(true);
        //OpenPublicityJustification_ing = true;
    }

    public void ClosePublicityJustification()
    {
        //gameObject.SetActive(true);
        //ClosePublicityJustification_ing = true;
        gameObject.SetActive(false);
    }

    public void ChangeReasonTargetMovieName(string _name, int totalNum, int userWatchedNum)
    {
        TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("MovieName").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = _name;

        textMeshPro = gameObject.transform.Find("Title").GetComponent<TMPro.TextMeshPro>();
        //textMeshPro.text = "Because you watched " + totalNum.ToString() + " out of " + userWatchedNum.ToString() + " movies: ";
        textMeshPro.text = totalNum.ToString();
    }

    public void ChangeReasonMovies(string name1, string name2 = "", string name3 = "")
    {
        gameObject.transform.Find("MovieName1").GetComponent<TMPro.TextMeshPro>().text = name1;
        gameObject.transform.Find("MovieName2").GetComponent<TMPro.TextMeshPro>().text = name2;
        gameObject.transform.Find("MovieName3").GetComponent<TMPro.TextMeshPro>().text = name3;
    }

    public void UpdatePoster(string url1, string url2, string url3, int totalNum)
    {
        moviePoster1.ChangeImage(url1);
        moviePoster2.ChangeImage(url2);
        moviePoster3.ChangeImage(url3);

        if (totalNum > 3)
        {
            moreSelection.SetActive(true);
        }
        else
        {
            moreSelection.SetActive(false);
        }
    }

    public void UpdatePoster(string url1, string url2)
    {
        moviePoster1.ChangeImage(url1);
        moviePoster2.ChangeImage(url2);

        moreSelection.SetActive(false);
    }

    public void UpdatePoster(string url1)
    {
        moviePoster1.ChangeImage(url1);

        moreSelection.SetActive(false);
    }

    public List<ImageLoader> posters;

    public void UpdateRecommendationFullList(List<string> movieNames)
    {
        if (movieNames.Count < 15)
        {
            string text = "";
            foreach (string name in movieNames)
            {
                text += name + "\n";
            }
            TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("MovieNames1").GetComponent<TMPro.TextMeshPro>();
            textMeshPro.text = text;

            textMeshPro = gameObject.transform.Find("MovieNames2").GetComponent<TMPro.TextMeshPro>();
            textMeshPro.text = "";
        }
        else
        {
            string text = "";
            foreach (string name in movieNames.GetRange(0, 15))
            {
                text += name + "\n";
            }
            TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("MovieNames1").GetComponent<TMPro.TextMeshPro>();
            textMeshPro.text = text;

            text = "";
            foreach (string name in movieNames.GetRange(15, movieNames.Count - 15))
            {
                text += name + "\n";
            }
            textMeshPro = gameObject.transform.Find("MovieNames2").GetComponent<TMPro.TextMeshPro>();
            textMeshPro.text = text;
        }
    }

    private int currentPage;
    List<string> names;
    List<string> posterUrls;
    public void UpdateRecommendationFullList_part(List<string> movieNames, List<string> _posterUrls)
    {
        currentPage = -3;
        names = movieNames;
        posterUrls = _posterUrls;
    }

    public void ShowRecommendationFullList_part()
    {
        currentPage += 3;

        for (int i = 0; i < 3; i++)
        {
            if (currentPage + i < names.Count)
            {
                posters[i].gameObject.SetActive(true);
                posters[i].ChangeImage(posterUrls[currentPage + i]);
                gameObject.transform.Find("MovieName" + (i + 1).ToString()).GetComponent<TMPro.TextMeshPro>().text = names[currentPage + i];
            }
            else if (i == 0 && names.Count != 0)
            {
                currentPage = 0;
                posters[i].gameObject.SetActive(true);
                posters[i].ChangeImage(posterUrls[currentPage + i]);
                gameObject.transform.Find("MovieName" + (i + 1).ToString()).GetComponent<TMPro.TextMeshPro>().text = names[currentPage + i];
            }
            else if (names.Count != 0)
            {
                posters[i].gameObject.SetActive(false);
                gameObject.transform.Find("MovieName" + (i + 1).ToString()).GetComponent<TMPro.TextMeshPro>().text = "";
                if (i == 2)
                {
                    currentPage = -3;
                }
            }
        }
    }
}
