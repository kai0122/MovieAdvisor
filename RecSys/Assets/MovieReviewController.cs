using System.Collections;
using System.Collections.Generic;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using UnityEngine;

public class MovieReviewController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeReviewName(string _name)
    {
        TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("Name").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = _name;
    }

    public void ChangeReviewInfo(string review)
    {
        TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("Review").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = review;
    }
}
