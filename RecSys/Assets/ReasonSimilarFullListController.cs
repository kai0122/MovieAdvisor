using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReasonSimilarFullListController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSimilarFullList(List<string> movieNames)
    {
        if (movieNames.Count < 15)
        {
            string text = "";
            foreach(string name in movieNames)
            {
                text += name + "\n";
            }
            TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("MovieNames1").GetComponent<TMPro.TextMeshPro>();
            textMeshPro.text = text;
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
            foreach (string name in movieNames.GetRange(15, 30))
            {
                text += name + "\n";
            }
            textMeshPro = gameObject.transform.Find("MovieNames2").GetComponent<TMPro.TextMeshPro>();
            textMeshPro.text = text;
        }
    }
}
