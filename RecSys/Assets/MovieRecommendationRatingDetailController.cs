using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieRecommendationRatingDetailController : MonoBehaviour
{
    public ScoreStarController TMDbStarController;
    public ScoreStarController IMDbStarController;
    public ScoreStarController RottenTomatoStarController;
    public ScoreStarController MetacriticStarController;

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
        if (OpenRatingJustification_ing)
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
                OpenRatingJustification_ing = false;
            }
        }

        if (CloseRatingJustification_ing)
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
                CloseRatingJustification_ing = false;
                gameObject.SetActive(false);
            }
        }
    }

    public bool OpenRatingJustification_ing = false;
    public bool CloseRatingJustification_ing = false;

    public void OpenRatingJustification()
    {
        gameObject.SetActive(true);
        //OpenRatingJustification_ing = true;
    }

    public void CloseRatingJustification()
    {
        //gameObject.SetActive(true);
        //CloseRatingJustification_ing = true;
        gameObject.SetActive(false);
    }

    public void ChangeDetailRatingScore(string _movieName, double TMDbScore, double IMDbScore, int rottenTomatoeScore, int metaCriticScore)
    {
        gameObject.transform.Find("TMDbScore").GetComponent<TMPro.TextMeshPro>().text = (TMDbScore / 2.0f).ToString("0.0") + " / 5";
        gameObject.transform.Find("IMDbScore").GetComponent<TMPro.TextMeshPro>().text = (IMDbScore / 2.0f).ToString("0.0") + " / 5";
        gameObject.transform.Find("RottenTomatoScore").GetComponent<TMPro.TextMeshPro>().text = (rottenTomatoeScore/20.0f).ToString("0.0") + " / 5";
        //gameObject.transform.Find("MetacriticScore").GetComponent<TMPro.TextMeshPro>().text = metaCriticScore.ToString() + " / 100";

        //TMDbStarController.ChangeStarScore((int)(TMDbScore / 10.0f * 5));
        //IMDbStarController.ChangeStarScore((int)(IMDbScore / 10.0f * 5));
        //RottenTomatoStarController.ChangeStarScore((int)((double) rottenTomatoeScore / 100.0f * 5));
        //MetacriticStarController.ChangeStarScore((int)((double) metaCriticScore / 100.0f * 5));

        gameObject.transform.Find("RatingFunction").GetComponent<TMPro.TextMeshPro>().text = ((TMDbScore / 2.0f) + (IMDbScore / 2.0f) + (rottenTomatoeScore / 20.0f)).ToString("0.0") +
                                                                                             " / 3 = " + (((TMDbScore / 2.0f) + (IMDbScore / 2.0f) + (rottenTomatoeScore / 20.0f)) / 3.0f).ToString("0.0");

        gameObject.transform.Find("RatingHint").GetComponent<TMPro.TextMeshPro>().text = "Average movie rating calculated for " + _movieName.ToUpper() + " from movie rating websites:";
    }

    public void ChangeDetailRatingScore(double TMDbScore, double IMDbScore, int rottenTomatoeScore, int metaCriticScore)
    {
        gameObject.transform.Find("TMDbScore").GetComponent<TMPro.TextMeshPro>().text = (TMDbScore / 2.0f).ToString("0.0") + " / 5";
        gameObject.transform.Find("IMDbScore").GetComponent<TMPro.TextMeshPro>().text = (IMDbScore / 2.0f).ToString("0.0") + " / 5";
        gameObject.transform.Find("RottenTomatoScore").GetComponent<TMPro.TextMeshPro>().text = (rottenTomatoeScore / 20.0f).ToString("0.0") + " / 5";
        //gameObject.transform.Find("MetacriticScore").GetComponent<TMPro.TextMeshPro>().text = metaCriticScore.ToString() + " / 100";

        TMDbStarController.ChangeStarScore((int)(TMDbScore / 10.0f * 5));
        IMDbStarController.ChangeStarScore((int)(IMDbScore / 10.0f * 5));
        RottenTomatoStarController.ChangeStarScore((int)((double)rottenTomatoeScore / 100.0f * 5));
        //MetacriticStarController.ChangeStarScore((int)((double) metaCriticScore / 100.0f * 5));

        gameObject.transform.Find("RatingFunction").GetComponent<TMPro.TextMeshPro>().text = ((TMDbScore / 2.0f) + (IMDbScore / 2.0f) + (rottenTomatoeScore / 20.0f)).ToString("0.0") +
                                                                                             " / 3 = " + (((TMDbScore / 2.0f) + (IMDbScore / 2.0f) + (rottenTomatoeScore / 20.0f)) / 3.0f).ToString("0.0");

    }
}
