using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieJustificationController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeJustification(string reason)
    {
        TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("Reason").GetComponent<TMPro.TextMeshPro>();

        if (reason == "Movie")
        {
            textMeshPro.text = "Recommendation is based on:\nSimilar Movies";
        }
        if (reason == "Actor")
        {
            textMeshPro.text = "Recommendation is based on:\nActors/Actresses";
        }
        if (reason == "Genre")
        {
            textMeshPro.text = "Recommendation is based on:\nMovie Genres";
        }
    }
}
