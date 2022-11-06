using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilterController : MonoBehaviour
{
    public GameObject Filter;
    public FilterNumberHolder filterNumberHolder;

    bool showingFilterSetting = true;

    // Start is called before the first frame update
    void Start()
    {
        FilterButtonPressed();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public double GetActorBase()
    {
        return filterNumberHolder.actorBaseRatio;
    }

    public double GetGenreBase()
    {
        return filterNumberHolder.genreBaseRatio;
    }

    public double GetPublicityBase()
    {
        return filterNumberHolder.publicityBaseRatio;
    }

    public double GetRatingBase()
    {
        return filterNumberHolder.ratingBaseRatio;
    }

    public void ApplyButtonPressed()
    {
        filterNumberHolder.actorBaseRatio = Filter.transform.Find("ScrollbarActor").GetComponent<Scrollbar>().value;
        filterNumberHolder.genreBaseRatio = Filter.transform.Find("ScrollbarGenre").GetComponent<Scrollbar>().value;
        filterNumberHolder.publicityBaseRatio = Filter.transform.Find("ScrollbarPublicity").GetComponent<Scrollbar>().value;
        filterNumberHolder.ratingBaseRatio = Filter.transform.Find("ScrollbarRating").GetComponent<Scrollbar>().value;

        showingFilterSetting = false;
        Filter.SetActive(false);
    }

    public void FilterButtonPressed()
    {
        if (showingFilterSetting)
        {
            showingFilterSetting = false;
            Filter.SetActive(false);
        }
        else
        {
            showingFilterSetting = true;
            Filter.SetActive(true);
            Filter.transform.Find("ScrollbarActor").GetComponent<Scrollbar>().value = filterNumberHolder.actorBaseRatio;
            Filter.transform.Find("ScrollbarGenre").GetComponent<Scrollbar>().value = filterNumberHolder.genreBaseRatio;
            Filter.transform.Find("ScrollbarPublicity").GetComponent<Scrollbar>().value = filterNumberHolder.publicityBaseRatio;
            Filter.transform.Find("ScrollbarRating").GetComponent<Scrollbar>().value = filterNumberHolder.ratingBaseRatio;
        }
    }
}
