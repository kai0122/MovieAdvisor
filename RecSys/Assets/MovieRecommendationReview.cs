using System.Collections;
using System.Collections.Generic;
using TMDbLib.Objects.Reviews;
using UnityEngine;

public class MovieRecommendationReview : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int currentReviewIndex = 0;
    List<ReviewBase> currentReviews;
    public void ShowReview(List<ReviewBase> reviews)
    {
        currentReviews = reviews;

        string subReview = "";
        foreach(string str in reviews[currentReviewIndex].Content.Split('.'))
        {
            if ((subReview + str).Length <= 600)
            {
                subReview += str + ".";
            }
            else
            {
                break;
            }
        }

        int newReviewLength = subReview.Length;
        gameObject.transform.Find("Review").GetComponent<TMPro.TextMeshPro>().text = reviews[currentReviewIndex].Author + ":\n\n" + subReview;
        gameObject.transform.Find("Plane").localScale = new Vector3(gameObject.transform.Find("Plane").localScale.x,
                                                                    gameObject.transform.Find("Plane").localScale.y,
                                                                    0.005f * (subReview.Split('\n').Length + 1 + (float)newReviewLength / 70.0f));
    }

    public void ShowReview()
    {
        string subReview = "";
        foreach (string str in currentReviews[currentReviewIndex].Content.Split('.'))
        {
            if ((subReview + str).Length <= 600)
            {
                subReview += str + ".";
            }
            else
            {
                break;
            }
        }

        int newReviewLength = subReview.Length;
        gameObject.transform.Find("Review").GetComponent<TMPro.TextMeshPro>().text = currentReviews[currentReviewIndex].Author + ":\n\n" + subReview;
        gameObject.transform.Find("Plane").localScale = new Vector3(gameObject.transform.Find("Plane").localScale.x,
                                                                    gameObject.transform.Find("Plane").localScale.y,
                                                                    0.005f * (subReview.Split('\n').Length + 1 + (float)newReviewLength / 70.0f));
    }

    public void NextReview()
    {
        if (currentReviewIndex + 1 < currentReviews.Count)
        {
            currentReviewIndex += 1;
        }
        else
        {
            currentReviewIndex = 0;
        }
        ShowReview();
    }

    public void PreviousReview()
    {
        if (currentReviewIndex - 1 >= 0)
        {
            currentReviewIndex -= 1;
        }
        else
        {
            currentReviewIndex = currentReviews.Count - 1;
        }
        ShowReview();
    }
}
