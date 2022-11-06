using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReasonSubScoreController : MonoBehaviour
{
    public ScoreStarController actorStarController;
    public ScoreStarController similarStarController;
    public ScoreStarController recommendationStarController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeRecommendationSubScore(double actorScore, double similarScore, double recommendationScore)
    {
        actorStarController.ChangeStarScore((int) (actorScore * 5));
        similarStarController.ChangeStarScore((int) (similarScore * 5));
        recommendationStarController.ChangeStarScore((int) (recommendationScore * 5));
    }
}
