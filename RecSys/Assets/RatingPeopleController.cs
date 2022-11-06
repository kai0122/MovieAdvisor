using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatingPeopleController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateRatedPeople(int peopleNum)
    {
        gameObject.GetComponent<TMPro.TextMeshPro>().text = "Rated by " + peopleNum.ToString() + " people";
    }
}
