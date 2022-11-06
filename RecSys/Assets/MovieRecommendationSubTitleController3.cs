using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieRecommendationSubTitleController3 : MonoBehaviour
{
    public GameObject actorScoreBar;
    public GameObject actorScoreBarBase;
    public GameObject genreScoreBar;
    public GameObject genreScoreBarBase;
    public GameObject publicityScoreBar;
    public GameObject publicityScoreBarBase;
    public GameObject ratingScoreBar;
    public GameObject ratingScoreBarBase;

    public Material greenMaterial;
    public Material redMaterial;

    private float barLength = 0.05f;

    public UserLikedMoviesController userLikedMoviesController;
    private int MIN_NUM = 30;


    // Start is called before the first frame update
    void Start()
    {
        startPosition_ActorBlock = ActorBlock.transform.localPosition;
        startPosition_GenreBlock = GenreBlock.transform.localPosition;
        startPosition_PublicityBlock = PublicityBlock.transform.localPosition;
        startPosition_RatingBlock = RatingBlock.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        MIN_NUM = userLikedMoviesController.GetUserLikedMovieAmount();
    }

    public GameObject ActorBlock;
    public GameObject GenreBlock;
    public GameObject PublicityBlock;
    public GameObject RatingBlock;

    private Vector3 startPosition_ActorBlock = new Vector3(0, 0, 0);
    private Vector3 startPosition_GenreBlock = new Vector3(0, 0, 0);
    private Vector3 startPosition_PublicityBlock = new Vector3(0, 0, 0);
    private Vector3 startPosition_RatingBlock = new Vector3(0, 0, 0);

    public void ExpendActorWhy_1()
    {
        GenreBlock.transform.localPosition = startPosition_GenreBlock - new Vector3(0, 0, 0.61f);
        PublicityBlock.transform.localPosition = startPosition_PublicityBlock - new Vector3(0, 0, 0.61f);
        RatingBlock.transform.localPosition = startPosition_RatingBlock - new Vector3(0, 0, 0.61f);
    }

    public void CloseActorWhy_1()
    {
        GenreBlock.transform.localPosition = startPosition_GenreBlock;
        PublicityBlock.transform.localPosition = startPosition_PublicityBlock;
        RatingBlock.transform.localPosition = startPosition_RatingBlock;
    }

    public void ExpendActorWhyMore_1()
    {
        GenreBlock.transform.localPosition = startPosition_GenreBlock - new Vector3(0, 0, 1.07f);
        PublicityBlock.transform.localPosition = startPosition_PublicityBlock - new Vector3(0, 0, 1.07f);
        RatingBlock.transform.localPosition = startPosition_RatingBlock - new Vector3(0, 0, 1.07f);
    }

    public void CloseActorWhyMore_1()
    {
        if (!(ActorBlock.transform.localPosition == startPosition_ActorBlock &&
            GenreBlock.transform.localPosition == startPosition_GenreBlock &&
            PublicityBlock.transform.localPosition == startPosition_PublicityBlock &&
            RatingBlock.transform.localPosition == startPosition_RatingBlock))
        {
            GenreBlock.transform.localPosition = startPosition_GenreBlock - new Vector3(0, 0, 0.61f);
            PublicityBlock.transform.localPosition = startPosition_PublicityBlock - new Vector3(0, 0, 0.61f);
            RatingBlock.transform.localPosition = startPosition_RatingBlock - new Vector3(0, 0, 0.61f);
        }
    }

    public void ExpendGenreWhy_1()
    {
        PublicityBlock.transform.localPosition = startPosition_PublicityBlock - new Vector3(0, 0, 0.578f);
        RatingBlock.transform.localPosition = startPosition_RatingBlock - new Vector3(0, 0, 0.578f);
    }

    public void CloseGenreWhy_1()
    {
        PublicityBlock.transform.localPosition = startPosition_PublicityBlock;
        RatingBlock.transform.localPosition = startPosition_RatingBlock;
    }

    public void ExpendGenreWhyMore_1()
    {
        PublicityBlock.transform.localPosition = startPosition_PublicityBlock - new Vector3(0, 0, 1.094f);
        RatingBlock.transform.localPosition = startPosition_RatingBlock - new Vector3(0, 0, 1.094f);
    }

    public void CloseGenreWhyMore_1()
    {
        if (!(ActorBlock.transform.localPosition == startPosition_ActorBlock &&
            GenreBlock.transform.localPosition == startPosition_GenreBlock &&
            PublicityBlock.transform.localPosition == startPosition_PublicityBlock &&
            RatingBlock.transform.localPosition == startPosition_RatingBlock))
        {
            PublicityBlock.transform.localPosition = startPosition_PublicityBlock - new Vector3(0, 0, 0.578f);
            RatingBlock.transform.localPosition = startPosition_RatingBlock - new Vector3(0, 0, 0.578f);
        }
    }

    public void ExpendPublicityWhy_1()
    {
        RatingBlock.transform.localPosition = startPosition_RatingBlock - new Vector3(0, 0, 0.189f);
    }

    public void ExpendPublicityWhy_2()
    {
        RatingBlock.transform.localPosition = startPosition_RatingBlock - new Vector3(0, 0, 0.259f);
    }

    public void ClosePublicityWhy_1()
    {
        RatingBlock.transform.localPosition = startPosition_RatingBlock;
    }

    public void ExpendPublicityWhyMore_1()
    {
        RatingBlock.transform.localPosition = startPosition_RatingBlock - new Vector3(0, 0, 0.588f);
    }

    public void ExpendPublicityWhyMore_2()
    {
        RatingBlock.transform.localPosition = startPosition_RatingBlock - new Vector3(0, 0, 0.633f);
    }

    public void ClosePublicityWhyMore_1()
    {
        if (!(ActorBlock.transform.localPosition == startPosition_ActorBlock &&
            GenreBlock.transform.localPosition == startPosition_GenreBlock &&
            PublicityBlock.transform.localPosition == startPosition_PublicityBlock &&
            RatingBlock.transform.localPosition == startPosition_RatingBlock))
        {
            RatingBlock.transform.localPosition = startPosition_RatingBlock - new Vector3(0, 0, 0.189f);
        }
    }

    public void ChangeRecommendationSubTitleActor3(int score, int baseScore, int totalScore)
    {
        ChangeRecommendationSubTitleActor2(score, baseScore, totalScore);

        float length = ((float)baseScore / MIN_NUM) * barLength;
        float shiftX = (-(barLength - length) / 2) * 10 + 0.37f;

        ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localScale = new Vector3(length, ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.y, ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.z);
        ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition = new Vector3(shiftX, ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.y, ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.z);

        length = barLength - length;
        shiftX = ((barLength - length) / 2) * 10 + 0.37f;

        ActorBlock.transform.Find("BarChartBackgroundRight").transform.localScale = new Vector3(length, ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.y, ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.z);
        ActorBlock.transform.Find("BarChartBackgroundRight").transform.localPosition = new Vector3(shiftX, ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.y, ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.z);
    }

    public void DirectUpdateActorColor(float percentage)
    {
        float length = percentage * barLength;
        float shiftX = (-(barLength - length) / 2) * 10 + 0.37f;

        ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localScale = new Vector3(length, ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.y, ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.z);
        ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition = new Vector3(shiftX, ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.y, ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.z);

        length = barLength - length;
        shiftX = ((barLength - length) / 2) * 10 + 0.37f;

        ActorBlock.transform.Find("BarChartBackgroundRight").transform.localScale = new Vector3(length, ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.y, ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.z);
        ActorBlock.transform.Find("BarChartBackgroundRight").transform.localPosition = new Vector3(shiftX, ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.y, ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.z);

        actorScoreBarBase.transform.Find("Score").GetComponent<TMPro.TextMeshPro>().text = (percentage * MIN_NUM).ToString("0");
    }

    public void DirectUpdateActorColor2(float percentage, float percent_movie)
    {
        if (percent_movie >= percentage)
        {
            actorScoreBar.GetComponent<Renderer>().material = greenMaterial;
        }
        else
        {
            actorScoreBar.GetComponent<Renderer>().material = redMaterial;
        }

        float length = percentage * barLength;
        float shiftX = (-(barLength - length) / 2) * 10 + 0.103f;

        ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localScale = new Vector3(length, ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.y, ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.z);
        ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition = new Vector3(shiftX, ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.y, ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.z);

        length = barLength - length;
        shiftX = ((barLength - length) / 2) * 10 + 0.103f;

        ActorBlock.transform.Find("BarChartBackgroundRight").transform.localScale = new Vector3(length, ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.y, ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.z);
        ActorBlock.transform.Find("BarChartBackgroundRight").transform.localPosition = new Vector3(shiftX, ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.y, ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.z);

        ActorBlock.transform.Find("ActorScoreBase").GetComponent<TMPro.TextMeshPro>().text = (percentage * MIN_NUM).ToString("0");
    }

    public void ChangeRecommendationSubTitleGenre3(int score, int baseScore, int totalScore)
    {
        ChangeRecommendationSubTitleGenre2(score, baseScore, totalScore);

        float length = ((float)baseScore / MIN_NUM) * barLength;
        float shiftX = (-(barLength - length) / 2) * 10 + 0.37f;

        GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localScale = new Vector3(length, GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.y, GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.z);
        GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition = new Vector3(shiftX, GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.y, GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.z);

        length = barLength - length;
        shiftX = ((barLength - length) / 2) * 10 + 0.37f;

        GenreBlock.transform.Find("BarChartBackgroundRight").transform.localScale = new Vector3(length, GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.y, GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.z);
        GenreBlock.transform.Find("BarChartBackgroundRight").transform.localPosition = new Vector3(shiftX, GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.y, GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.z);

    }

    public void DirectUpdateGenreColor(float percentage)
    {
        float length = percentage * barLength;
        float shiftX = (-(barLength - length) / 2) * 10 + 0.37f;

        GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localScale = new Vector3(length, GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.y, GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.z);
        GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition = new Vector3(shiftX, GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.y, GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.z);

        length = barLength - length;
        shiftX = ((barLength - length) / 2) * 10 + 0.37f;

        GenreBlock.transform.Find("BarChartBackgroundRight").transform.localScale = new Vector3(length, GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.y, GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.z);
        GenreBlock.transform.Find("BarChartBackgroundRight").transform.localPosition = new Vector3(shiftX, GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.y, GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.z);

        genreScoreBarBase.transform.Find("Score").GetComponent<TMPro.TextMeshPro>().text = (percentage * MIN_NUM).ToString("0");
    }

    public void DirectUpdateGenreColor2(float percentage, float percent_movie)
    {
        if (percent_movie >= percentage)
        {
            genreScoreBar.GetComponent<Renderer>().material = greenMaterial;
        }
        else
        {
            genreScoreBar.GetComponent<Renderer>().material = redMaterial;
        }

        float length = percentage * barLength;
        float shiftX = (-(barLength - length) / 2) * 10 + 0.103f;

        GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localScale = new Vector3(length, GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.y, GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.z);
        GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition = new Vector3(shiftX, GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.y, GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.z);

        length = barLength - length;
        shiftX = ((barLength - length) / 2) * 10 + 0.103f;

        GenreBlock.transform.Find("BarChartBackgroundRight").transform.localScale = new Vector3(length, GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.y, GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.z);
        GenreBlock.transform.Find("BarChartBackgroundRight").transform.localPosition = new Vector3(shiftX, GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.y, GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.z);

        GenreBlock.transform.Find("GenreScoreBase").GetComponent<TMPro.TextMeshPro>().text = (percentage * MIN_NUM).ToString("0");
    }

    public void ChangeRecommendationSubTitlePublicity3(int score, int baseScore, int totalScore)
    {
        ChangeRecommendationSubTitlePublicity2(score, baseScore, totalScore);

        float length = ((float)baseScore / MIN_NUM) * barLength;
        float shiftX = (-(barLength - length) / 2) * 10 + 0.37f;

        PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localScale = new Vector3(length, PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.y, PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.z);
        PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition = new Vector3(shiftX, PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.y, PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.z);

        length = barLength - length;
        shiftX = ((barLength - length) / 2) * 10 + 0.37f;

        PublicityBlock.transform.Find("BarChartBackgroundRight").transform.localScale = new Vector3(length, PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.y, PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.z);
        PublicityBlock.transform.Find("BarChartBackgroundRight").transform.localPosition = new Vector3(shiftX, PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.y, PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.z);

    }

    public void DirectUpdatePublicityColor(float percentage)
    {
        float length = percentage * barLength;
        float shiftX = (-(barLength - length) / 2) * 10 + 0.37f;

        PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localScale = new Vector3(length, PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.y, PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.z);
        PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition = new Vector3(shiftX, PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.y, PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.z);

        length = barLength - length;
        shiftX = ((barLength - length) / 2) * 10 + 0.37f;

        PublicityBlock.transform.Find("BarChartBackgroundRight").transform.localScale = new Vector3(length, PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.y, PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.z);
        PublicityBlock.transform.Find("BarChartBackgroundRight").transform.localPosition = new Vector3(shiftX, PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.y, PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.z);

        publicityScoreBarBase.transform.Find("Score").GetComponent<TMPro.TextMeshPro>().text = (percentage * MIN_NUM).ToString("0");
    }

    public void DirectUpdatePublicityColor2(float percentage, float percent_movie)
    {
        if (percent_movie >= percentage)
        {
            publicityScoreBar.GetComponent<Renderer>().material = greenMaterial;
        }
        else
        {
            publicityScoreBar.GetComponent<Renderer>().material = redMaterial;
        }

        float length = percentage * barLength;
        float shiftX = (-(barLength - length) / 2) * 10 + 0.103f;

        PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localScale = new Vector3(length, PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.y, PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.z);
        PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition = new Vector3(shiftX, PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.y, PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.z);

        length = barLength - length;
        shiftX = ((barLength - length) / 2) * 10 + 0.103f;

        PublicityBlock.transform.Find("BarChartBackgroundRight").transform.localScale = new Vector3(length, PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.y, PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.z);
        PublicityBlock.transform.Find("BarChartBackgroundRight").transform.localPosition = new Vector3(shiftX, PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.y, PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.z);

        PublicityBlock.transform.Find("PublicityScoreBase").GetComponent<TMPro.TextMeshPro>().text = (percentage * MIN_NUM).ToString("0");
    }

    public void ChangeRecommendationSubTitleRating3(double score, int baseScore, int totalScore)
    {
        ChangeRecommendationSubTitleRating2(score, baseScore, totalScore);

        float length = ((float)baseScore / 100) * barLength;
        float shiftX = (-(barLength - length) / 2) * 10 + 0.37f;

        RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localScale = new Vector3(length, RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.y, RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.z);
        RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition = new Vector3(shiftX, RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.y, RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.z);

        length = barLength - length;
        shiftX = ((barLength - length) / 2) * 10 + 0.37f;

        RatingBlock.transform.Find("BarChartBackgroundRight").transform.localScale = new Vector3(length, RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.y, RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.z);
        RatingBlock.transform.Find("BarChartBackgroundRight").transform.localPosition = new Vector3(shiftX, RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.y, RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.z);

    }

    public void DirectUpdateRatingColor(float percentage)
    {
        float length = percentage * barLength;
        float shiftX = (-(barLength - length) / 2) * 10 + 0.37f;

        RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localScale = new Vector3(length, RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.y, RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.z);
        RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition = new Vector3(shiftX, RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.y, RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.z);

        length = barLength - length;
        shiftX = ((barLength - length) / 2) * 10 + 0.37f;

        RatingBlock.transform.Find("BarChartBackgroundRight").transform.localScale = new Vector3(length, RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.y, RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.z);
        RatingBlock.transform.Find("BarChartBackgroundRight").transform.localPosition = new Vector3(shiftX, RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.y, RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.z);

        ratingScoreBarBase.transform.Find("Score").GetComponent<TMPro.TextMeshPro>().text = (percentage * 5).ToString("0.0");
    }

    public void DirectUpdateRatingColor2(float percentage, float percent_movie)
    {
        if (percent_movie >= percentage)
        {
            ratingScoreBar.GetComponent<Renderer>().material = greenMaterial;
        }
        else
        {
            ratingScoreBar.GetComponent<Renderer>().material = redMaterial;
        }

        float length = percentage * barLength;
        float shiftX = (-(barLength - length) / 2) * 10 + 0.103f;

        RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localScale = new Vector3(length, RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.y, RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.z);
        RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition = new Vector3(shiftX, RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.y, RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.z);

        length = barLength - length;
        shiftX = ((barLength - length) / 2) * 10 + 0.103f;

        RatingBlock.transform.Find("BarChartBackgroundRight").transform.localScale = new Vector3(length, RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.y, RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.z);
        RatingBlock.transform.Find("BarChartBackgroundRight").transform.localPosition = new Vector3(shiftX, RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.y, RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.z);

        RatingBlock.transform.Find("RatingScoreBase").GetComponent<TMPro.TextMeshPro>().text = (percentage * 5).ToString("0.0");
    }

    public void ChangeRecommendationSubTitleActor4(int score, int baseScore, int totalScore)
    {
        if (score >= baseScore)
        {
            actorScoreBar.GetComponent<Renderer>().material = greenMaterial;
        }
        else
        {
            actorScoreBar.GetComponent<Renderer>().material = redMaterial;
        }

        float length = ((float)baseScore / MIN_NUM) * barLength;
        float shiftX = (-(barLength - length) / 2) * 10 + 0.103f;

        ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localScale = new Vector3(length, ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.y, ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.z);
        ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition = new Vector3(shiftX, ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.y, ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.z);

        length = barLength - length;
        shiftX = ((barLength - length) / 2) * 10 + 0.103f;

        ActorBlock.transform.Find("BarChartBackgroundRight").transform.localScale = new Vector3(length, ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.y, ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.z);
        ActorBlock.transform.Find("BarChartBackgroundRight").transform.localPosition = new Vector3(shiftX, ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.y, ActorBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.z);

        score = score > MIN_NUM ? MIN_NUM : score;
        length = ((float)score / MIN_NUM) * barLength;
        shiftX = (-(barLength - length) / 2) * 10 + 0.103f;

        actorScoreBar.transform.localScale = new Vector3(length, actorScoreBar.transform.localScale.y, actorScoreBar.transform.localScale.z);
        actorScoreBar.transform.localPosition = new Vector3(shiftX, actorScoreBar.transform.localPosition.y, actorScoreBar.transform.localPosition.z);

        length = barLength - length;
        shiftX = ((barLength - length) / 2) * 10 + 0.103f;

        ActorBlock.transform.Find("BarChartBackgroundRight_Up").transform.localScale = new Vector3(length, actorScoreBar.transform.localScale.y, actorScoreBar.transform.localScale.z);
        ActorBlock.transform.Find("BarChartBackgroundRight_Up").transform.localPosition = new Vector3(shiftX, actorScoreBar.transform.localPosition.y, actorScoreBar.transform.localPosition.z);


        TMPro.TextMeshPro textMeshPro = ActorBlock.transform.Find("ActorScore").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = score.ToString();

        float newX = ((float)baseScore / MIN_NUM) * 0.48f + -0.14f;

        actorScoreBarBase.transform.localPosition = new Vector3(newX, actorScoreBarBase.transform.localPosition.y, actorScoreBarBase.transform.localPosition.z);
        textMeshPro = ActorBlock.transform.Find("ActorScoreBase").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = (baseScore).ToString("0");
    }

    public void ChangeRecommendationSubTitleGenre4(int score, int baseScore, int totalScore)
    {
        if (score >= baseScore)
        {
            genreScoreBar.GetComponent<Renderer>().material = greenMaterial;
        }
        else
        {
            genreScoreBar.GetComponent<Renderer>().material = redMaterial;
        }

        float length = ((float)baseScore / MIN_NUM) * barLength;
        float shiftX = (-(barLength - length) / 2) * 10 + 0.103f;

        GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localScale = new Vector3(length, GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.y, GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.z);
        GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition = new Vector3(shiftX, GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.y, GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.z);

        length = barLength - length;
        shiftX = ((barLength - length) / 2) * 10 + 0.103f;

        GenreBlock.transform.Find("BarChartBackgroundRight").transform.localScale = new Vector3(length, GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.y, GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.z);
        GenreBlock.transform.Find("BarChartBackgroundRight").transform.localPosition = new Vector3(shiftX, GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.y, GenreBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.z);

        score = score > MIN_NUM ? MIN_NUM : score;
        length = ((float)score / MIN_NUM) * barLength;
        shiftX = (-(barLength - length) / 2) * 10 + 0.103f;

        genreScoreBar.transform.localScale = new Vector3(length, genreScoreBar.transform.localScale.y, genreScoreBar.transform.localScale.z);
        genreScoreBar.transform.localPosition = new Vector3(shiftX, genreScoreBar.transform.localPosition.y, genreScoreBar.transform.localPosition.z);

        length = barLength - length;
        shiftX = ((barLength - length) / 2) * 10 + 0.103f;

        GenreBlock.transform.Find("BarChartBackgroundRight_Up").transform.localScale = new Vector3(length, genreScoreBar.transform.localScale.y, genreScoreBar.transform.localScale.z);
        GenreBlock.transform.Find("BarChartBackgroundRight_Up").transform.localPosition = new Vector3(shiftX, genreScoreBar.transform.localPosition.y, genreScoreBar.transform.localPosition.z);

        TMPro.TextMeshPro textMeshPro = GenreBlock.transform.Find("GenreScore").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = score.ToString();

        float newX = ((float)baseScore / MIN_NUM) * 0.48f + -0.14f;

        genreScoreBarBase.transform.localPosition = new Vector3(newX, genreScoreBarBase.transform.localPosition.y, genreScoreBarBase.transform.localPosition.z);
        textMeshPro = GenreBlock.transform.Find("GenreScoreBase").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = (baseScore).ToString("0");
    }

    public void ChangeRecommendationSubTitlePublicity4(int score, int baseScore, int totalScore)
    {
        if (score >= baseScore)
        {
            publicityScoreBar.GetComponent<Renderer>().material = greenMaterial;
        }
        else
        {
            publicityScoreBar.GetComponent<Renderer>().material = redMaterial;
        }

        float length = ((float)baseScore / MIN_NUM) * barLength;
        float shiftX = (-(barLength - length) / 2) * 10 + 0.103f;

        PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localScale = new Vector3(length, PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.y, PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.z);
        PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition = new Vector3(shiftX, PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.y, PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.z);

        length = barLength - length;
        shiftX = ((barLength - length) / 2) * 10 + 0.103f;

        PublicityBlock.transform.Find("BarChartBackgroundRight").transform.localScale = new Vector3(length, PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.y, PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.z);
        PublicityBlock.transform.Find("BarChartBackgroundRight").transform.localPosition = new Vector3(shiftX, PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.y, PublicityBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.z);

        score = score > MIN_NUM ? MIN_NUM : score;
        length = ((float)score / MIN_NUM) * barLength;
        shiftX = (-(barLength - length) / 2) * 10 + 0.103f;

        publicityScoreBar.transform.localScale = new Vector3(length, publicityScoreBar.transform.localScale.y, publicityScoreBar.transform.localScale.z);
        publicityScoreBar.transform.localPosition = new Vector3(shiftX, publicityScoreBar.transform.localPosition.y, publicityScoreBar.transform.localPosition.z);

        length = barLength - length;
        shiftX = ((barLength - length) / 2) * 10 + 0.103f;

        PublicityBlock.transform.Find("BarChartBackgroundRight_Up").transform.localScale = new Vector3(length, publicityScoreBar.transform.localScale.y, publicityScoreBar.transform.localScale.z);
        PublicityBlock.transform.Find("BarChartBackgroundRight_Up").transform.localPosition = new Vector3(shiftX, publicityScoreBar.transform.localPosition.y, publicityScoreBar.transform.localPosition.z);

        TMPro.TextMeshPro textMeshPro = PublicityBlock.transform.Find("PublicityScore").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = score.ToString();

        float newX = ((float)baseScore / MIN_NUM) * 0.48f + -0.14f;

        publicityScoreBarBase.transform.localPosition = new Vector3(newX, publicityScoreBarBase.transform.localPosition.y, publicityScoreBarBase.transform.localPosition.z);
        textMeshPro = PublicityBlock.transform.Find("PublicityScoreBase").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = (baseScore).ToString("0");
    }

    public void ChangeRecommendationSubTitleRating4(double score, int baseScore, int totalScore)
    {
        if (score >= baseScore)
        {
            ratingScoreBar.GetComponent<Renderer>().material = greenMaterial;
        }
        else
        {
            ratingScoreBar.GetComponent<Renderer>().material = redMaterial;
        }

        float length = ((float)baseScore / 100) * barLength;
        float shiftX = (-(barLength - length) / 2) * 10 + 0.103f;

        RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localScale = new Vector3(length, RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.y, RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.z);
        RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition = new Vector3(shiftX, RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.y, RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.z);

        length = barLength - length;
        shiftX = ((barLength - length) / 2) * 10 + 0.103f;

        RatingBlock.transform.Find("BarChartBackgroundRight").transform.localScale = new Vector3(length, RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.y, RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localScale.z);
        RatingBlock.transform.Find("BarChartBackgroundRight").transform.localPosition = new Vector3(shiftX, RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.y, RatingBlock.transform.Find("BarChartBackgroundLeft").transform.localPosition.z);

        length = ((float)score / 100) * barLength;
        shiftX = (-(barLength - length) / 2) * 10 + 0.103f;

        ratingScoreBar.transform.localScale = new Vector3(length, ratingScoreBar.transform.localScale.y, ratingScoreBar.transform.localScale.z);
        ratingScoreBar.transform.localPosition = new Vector3(shiftX, ratingScoreBar.transform.localPosition.y, ratingScoreBar.transform.localPosition.z);

        length = barLength - length;
        shiftX = ((barLength - length) / 2) * 10 + 0.103f;

        RatingBlock.transform.Find("BarChartBackgroundRight_Up").transform.localScale = new Vector3(length, ratingScoreBar.transform.localScale.y, ratingScoreBar.transform.localScale.z);
        RatingBlock.transform.Find("BarChartBackgroundRight_Up").transform.localPosition = new Vector3(shiftX, ratingScoreBar.transform.localPosition.y, ratingScoreBar.transform.localPosition.z);


        TMPro.TextMeshPro textMeshPro = RatingBlock.transform.Find("RatingScore").GetComponent<TMPro.TextMeshPro>();
        //textMeshPro.text = ((double)score / 10).ToString("0.0");
        textMeshPro.text = ((double)score / 20).ToString("0.0");

        float newX = ((float)baseScore / 100.0f) * 0.48f + -0.14f;

        ratingScoreBarBase.transform.localPosition = new Vector3(newX, ratingScoreBarBase.transform.localPosition.y, ratingScoreBarBase.transform.localPosition.z);
        textMeshPro = RatingBlock.transform.Find("RatingScoreBase").GetComponent<TMPro.TextMeshPro>();
        //textMeshPro.text = (baseScore / 10.0f).ToString("0.0");
        textMeshPro.text = (baseScore / 20.0f).ToString("0.0");
    }

    public void ChangeRecommendationSubTitleActor2(int score, int baseScore, int totalScore)
    {
        /*
        if (score >= baseScore)
        {
            actorScoreBar.GetComponent<Renderer>().material = greenMaterial;
        }
        else
        {
            actorScoreBar.GetComponent<Renderer>().material = redMaterial;
        }*/

        score = score > MIN_NUM ? MIN_NUM : score;
        float length = ((float)score / MIN_NUM) * barLength;
        float shiftX = (-(barLength - length) / 2) * 10 + 0.37f;

        actorScoreBar.transform.localScale = new Vector3(length, actorScoreBar.transform.localScale.y, actorScoreBar.transform.localScale.z);
        actorScoreBar.transform.localPosition = new Vector3(shiftX, actorScoreBar.transform.localPosition.y, actorScoreBar.transform.localPosition.z);
        TMPro.TextMeshPro textMeshPro = ActorBlock.transform.Find("ActorScore").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = score.ToString();

        float newX = ((float)baseScore / MIN_NUM) * 0.48f + (0.130f);

        actorScoreBarBase.transform.localPosition = new Vector3(newX, actorScoreBarBase.transform.localPosition.y, actorScoreBarBase.transform.localPosition.z);
        textMeshPro = actorScoreBarBase.transform.Find("Score").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = (baseScore).ToString("0");
    }

    public void ChangeRecommendationSubTitleGenre2(int score, int baseScore, int totalScore)
    {
        /*
        if (score >= baseScore)
        {
            genreScoreBar.GetComponent<Renderer>().material = greenMaterial;
        }
        else
        {
            genreScoreBar.GetComponent<Renderer>().material = redMaterial;
        }*/

        score = score > MIN_NUM ? MIN_NUM : score;
        float length = ((float)score / MIN_NUM) * barLength;
        float shiftX = (-(barLength - length) / 2) * 10 + 0.37f;

        genreScoreBar.transform.localScale = new Vector3(length, genreScoreBar.transform.localScale.y, genreScoreBar.transform.localScale.z);
        genreScoreBar.transform.localPosition = new Vector3(shiftX, genreScoreBar.transform.localPosition.y, genreScoreBar.transform.localPosition.z);
        TMPro.TextMeshPro textMeshPro = GenreBlock.transform.Find("GenreScore").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = score.ToString();

        float newX = ((float)baseScore / MIN_NUM) * 0.48f + (0.130f);

        genreScoreBarBase.transform.localPosition = new Vector3(newX, genreScoreBarBase.transform.localPosition.y, genreScoreBarBase.transform.localPosition.z);
        textMeshPro = genreScoreBarBase.transform.Find("Score").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = (baseScore).ToString("0");
    }

    public void ChangeRecommendationSubTitlePublicity2(int score, int baseScore, int totalScore)
    {
        /*
        if (score >= baseScore)
        {
            publicityScoreBar.GetComponent<Renderer>().material = greenMaterial;
        }
        else
        {
            publicityScoreBar.GetComponent<Renderer>().material = redMaterial;
        }*/

        score = score > MIN_NUM ? MIN_NUM : score;
        float length = ((float)score / MIN_NUM) * barLength;
        float shiftX = (-(barLength - length) / 2) * 10 + 0.37f;

        publicityScoreBar.transform.localScale = new Vector3(length, publicityScoreBar.transform.localScale.y, publicityScoreBar.transform.localScale.z);
        publicityScoreBar.transform.localPosition = new Vector3(shiftX, publicityScoreBar.transform.localPosition.y, publicityScoreBar.transform.localPosition.z);
        TMPro.TextMeshPro textMeshPro = PublicityBlock.transform.Find("PublicityScore").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = score.ToString();

        float newX = ((float)baseScore / MIN_NUM) * 0.48f + (0.130f);

        publicityScoreBarBase.transform.localPosition = new Vector3(newX, publicityScoreBarBase.transform.localPosition.y, publicityScoreBarBase.transform.localPosition.z);
        textMeshPro = publicityScoreBarBase.transform.Find("Score").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = (baseScore).ToString("0");
    }

    public void ChangeRecommendationSubTitleRating2(double score, int baseScore, int totalScore)
    {
        /*
        if (score >= baseScore)
        {
            ratingScoreBar.GetComponent<Renderer>().material = greenMaterial;
        }
        else
        {
            ratingScoreBar.GetComponent<Renderer>().material = redMaterial;
        }*/

        Debug.Log(score.ToString() + " / " + baseScore.ToString());

        float length = ((float)score / 100) * barLength;
        float shiftX = (-(barLength - length) / 2) * 10 + 0.37f;

        ratingScoreBar.transform.localScale = new Vector3(length, ratingScoreBar.transform.localScale.y, ratingScoreBar.transform.localScale.z);
        ratingScoreBar.transform.localPosition = new Vector3(shiftX, ratingScoreBar.transform.localPosition.y, ratingScoreBar.transform.localPosition.z);
        TMPro.TextMeshPro textMeshPro = RatingBlock.transform.Find("RatingScore").GetComponent<TMPro.TextMeshPro>();
        //textMeshPro.text = ((double)score / 10).ToString("0.0");
        textMeshPro.text = ((double)score / 20).ToString("0.0");

        float newX = ((float)baseScore / 100.0f) * 0.48f + (0.130f);

        ratingScoreBarBase.transform.localPosition = new Vector3(newX, ratingScoreBarBase.transform.localPosition.y, ratingScoreBarBase.transform.localPosition.z);
        textMeshPro = ratingScoreBarBase.transform.Find("Score").GetComponent<TMPro.TextMeshPro>();
        //textMeshPro.text = (baseScore / 10.0f).ToString("0.0");
        textMeshPro.text = (baseScore / 20.0f).ToString("0.0");
    }

    public void ChangeRecommendationSubTitleActor(int score, int baseScore, int totalScore)
    {
        Debug.Log("Actor Score..." + score.ToString());
        Debug.Log("Actor Base Score..." + baseScore.ToString());
        if (score >= baseScore)
        {
            actorScoreBar.GetComponent<Renderer>().material = greenMaterial;
        }
        else
        {
            actorScoreBar.GetComponent<Renderer>().material = redMaterial;
        }

        score = score > MIN_NUM ? MIN_NUM : score;
        float length = ((float)score / MIN_NUM) * barLength;
        float shiftX = (-(barLength - length) / 2) * 10 + 0.37f;

        actorScoreBar.transform.localScale = new Vector3(length, actorScoreBar.transform.localScale.y, actorScoreBar.transform.localScale.z);
        actorScoreBar.transform.localPosition = new Vector3(shiftX, actorScoreBar.transform.localPosition.y, actorScoreBar.transform.localPosition.z);
        TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("ActorScore").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = score.ToString();

        float newX = ((float)baseScore / MIN_NUM) * 0.48f + (0.130f);

        actorScoreBarBase.transform.localPosition = new Vector3(newX, actorScoreBarBase.transform.localPosition.y, actorScoreBarBase.transform.localPosition.z);
        textMeshPro = actorScoreBarBase.transform.Find("Score").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = (baseScore).ToString("0");
    }

    public void ChangeRecommendationSubTitleGenre(int score, int baseScore, int totalScore)
    {
        if (score >= baseScore)
        {
            genreScoreBar.GetComponent<Renderer>().material = greenMaterial;
        }
        else
        {
            genreScoreBar.GetComponent<Renderer>().material = redMaterial;
        }

        score = score > MIN_NUM ? MIN_NUM : score;
        float length = ((float)score / MIN_NUM) * barLength;
        float shiftX = (-(barLength - length) / 2) * 10 + 0.37f;

        genreScoreBar.transform.localScale = new Vector3(length, genreScoreBar.transform.localScale.y, genreScoreBar.transform.localScale.z);
        genreScoreBar.transform.localPosition = new Vector3(shiftX, genreScoreBar.transform.localPosition.y, genreScoreBar.transform.localPosition.z);
        TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("GenreScore").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = score.ToString();

        float newX = ((float)baseScore / MIN_NUM) * 0.48f + (0.130f);

        genreScoreBarBase.transform.localPosition = new Vector3(newX, genreScoreBarBase.transform.localPosition.y, genreScoreBarBase.transform.localPosition.z);
        textMeshPro = genreScoreBarBase.transform.Find("Score").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = (baseScore).ToString("0");
    }

    public void ChangeRecommendationSubTitlePublicity(int score, int baseScore, int totalScore)
    {
        if (score >= baseScore)
        {
            publicityScoreBar.GetComponent<Renderer>().material = greenMaterial;
        }
        else
        {
            publicityScoreBar.GetComponent<Renderer>().material = redMaterial;
        }

        score = score > MIN_NUM ? MIN_NUM : score;
        float length = ((float)score / MIN_NUM) * barLength;
        float shiftX = (-(barLength - length) / 2) * 10 + 0.37f;

        publicityScoreBar.transform.localScale = new Vector3(length, publicityScoreBar.transform.localScale.y, publicityScoreBar.transform.localScale.z);
        publicityScoreBar.transform.localPosition = new Vector3(shiftX, publicityScoreBar.transform.localPosition.y, publicityScoreBar.transform.localPosition.z);
        TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("PublicityScore").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = score.ToString();

        float newX = ((float)baseScore / MIN_NUM) * 0.48f + (0.130f);

        publicityScoreBarBase.transform.localPosition = new Vector3(newX, publicityScoreBarBase.transform.localPosition.y, publicityScoreBarBase.transform.localPosition.z);
        textMeshPro = publicityScoreBarBase.transform.Find("Score").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = (baseScore).ToString("0");
    }

    public void ChangeRecommendationSubTitleRating(double score, int baseScore, int totalScore)
    {
        if (score >= baseScore)
        {
            ratingScoreBar.GetComponent<Renderer>().material = greenMaterial;
        }
        else
        {
            ratingScoreBar.GetComponent<Renderer>().material = redMaterial;
        }

        Debug.Log(score.ToString() + " / " + baseScore.ToString());

        float length = ((float)score / 100) * barLength;
        float shiftX = (-(barLength - length) / 2) * 10 + 0.37f;

        ratingScoreBar.transform.localScale = new Vector3(length, ratingScoreBar.transform.localScale.y, ratingScoreBar.transform.localScale.z);
        ratingScoreBar.transform.localPosition = new Vector3(shiftX, ratingScoreBar.transform.localPosition.y, ratingScoreBar.transform.localPosition.z);
        TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("RatingScore").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = ((double)score / 10).ToString("0.0");

        float newX = ((float)baseScore / 100.0f) * 0.48f + (0.130f);

        ratingScoreBarBase.transform.localPosition = new Vector3(newX, ratingScoreBarBase.transform.localPosition.y, ratingScoreBarBase.transform.localPosition.z);
        textMeshPro = ratingScoreBarBase.transform.Find("Score").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = (baseScore / 10.0f).ToString("0.0");
    }

    public Texture StarOn;
    public Texture StarOff;
    public void UpdateRecommendationSubStars(List<bool> starFeatures)
    {
        if (starFeatures[0])
        {
            ActorBlock.transform.Find("StarIcon").GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Transparent"));
            ActorBlock.transform.Find("StarIcon").GetComponent<Renderer>().material.mainTexture = StarOn;
        }
        else
        {
            ActorBlock.transform.Find("StarIcon").GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Transparent"));
            ActorBlock.transform.Find("StarIcon").GetComponent<Renderer>().material.mainTexture = StarOff;
        }
        if (starFeatures[1])
        {
            GenreBlock.transform.Find("StarIcon").GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Transparent"));
            GenreBlock.transform.Find("StarIcon").GetComponent<Renderer>().material.mainTexture = StarOn;
        }
        else
        {
            GenreBlock.transform.Find("StarIcon").GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Transparent"));
            GenreBlock.transform.Find("StarIcon").GetComponent<Renderer>().material.mainTexture = StarOff;
        }
        if (starFeatures[2])
        {
            PublicityBlock.transform.Find("StarIcon").GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Transparent"));
            PublicityBlock.transform.Find("StarIcon").GetComponent<Renderer>().material.mainTexture = StarOn;
        }
        else
        {
            PublicityBlock.transform.Find("StarIcon").GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Transparent"));
            PublicityBlock.transform.Find("StarIcon").GetComponent<Renderer>().material.mainTexture = StarOff;
        }
        if (starFeatures[3])
        {
            RatingBlock.transform.Find("StarIcon").GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Transparent"));
            RatingBlock.transform.Find("StarIcon").GetComponent<Renderer>().material.mainTexture = StarOn;
        }
        else
        {
            RatingBlock.transform.Find("StarIcon").GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Transparent"));
            RatingBlock.transform.Find("StarIcon").GetComponent<Renderer>().material.mainTexture = StarOff;
        }

    }

}
