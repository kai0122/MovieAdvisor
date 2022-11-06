using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieSelection : MonoBehaviour
{
    private int selectionNum = 5;
    private List<GameObject> selections = new List<GameObject>();
    public TMDbController tMDbController;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 1; i <= selectionNum; i++)
        {
            selections.Add(gameObject.transform.Find("MovieContainor" + i.ToString()).Find("MovieInformation").Find("Selection").gameObject);
        }

        SetSelectionDisappear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<bool> FetchSelectedMovieIndex()
    {
        List<bool> items = new List<bool>();

        foreach(GameObject selection in selections)
        {
#pragma warning disable CS0618 // 類型或成員已經過時
            items.Add(selection.active);
        }

        return items;
    }

    public void SelectMovie(int _index)
    {
        if (selections[_index].active == false)
        {
            selections[_index].SetActive(true);
        }
        else
        {
            selections[_index].active = true;

            selections[_index].SetActive(false);
        }

        tMDbController.UpdateSelectMovies();
#pragma warning restore CS0618 // 類型或成員已經過時
    }

    public void SetSelectionDisappear()
    {
        foreach(GameObject selection in selections)
        {
            selection.SetActive(false);
        }
    }

}
