using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterNumberChanger : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        ChangeDisplayedNum();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeNum(float number)
    {
        gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = String.Format("{0:0}", (number * 100));
    }

    public void ChangeActorNum(float number)
    {
        totalNum -= ActorNum;
        ActorNum = number * 100;
        totalNum += ActorNum;
        ChangeDisplayedNum();
    }

    public void ChangeGenreNum(float number)
    {
        totalNum -= GenreNum;
        GenreNum = number * 100;
        totalNum += GenreNum;
        ChangeDisplayedNum();
    }

    public void ChangePublicityNum(float number)
    {
        totalNum -= PublicityNum;
        PublicityNum = number * 100;
        totalNum += PublicityNum;
        ChangeDisplayedNum();
    }

    public void ChangeRatingNum(float number)
    {
        totalNum -= RatingNum;
        RatingNum = number * 100;
        totalNum += RatingNum;
        ChangeDisplayedNum();
    }

    float totalNum = 200;
    float ActorNum = 50;
    float GenreNum = 50;
    float PublicityNum = 50;
    float RatingNum = 50;
    public void ChangeDisplayedNum()
    {
        gameObject.transform.Find("ActorNumber").GetComponent<TMPro.TextMeshProUGUI>().text = ((int)((ActorNum / totalNum) * 100)).ToString() + "%";
        gameObject.transform.Find("GenreNumber").GetComponent<TMPro.TextMeshProUGUI>().text = ((int)((GenreNum / totalNum) * 100)).ToString() + "%";
        gameObject.transform.Find("PublicityNumber").GetComponent<TMPro.TextMeshProUGUI>().text = ((int)((PublicityNum / totalNum) * 100)).ToString() + "%";
        gameObject.transform.Find("RatingNumber").GetComponent<TMPro.TextMeshProUGUI>().text = ((int)((RatingNum / totalNum) * 100)).ToString() + "%";
    }
}
