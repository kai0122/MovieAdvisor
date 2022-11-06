using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReasonScoreController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeRecommendationScore(int score)
    {
        TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("Text").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = "This movie is\n" + score.ToString() + "% fits you.";
    }
}
