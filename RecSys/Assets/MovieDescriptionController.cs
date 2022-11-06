using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieDescriptionController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeDescriptionName(string _name)
    {
        TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("Name").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = _name;
    }

    public void ChangeDescription(string _content)
    {
        TMPro.TextMeshPro textMeshPro = gameObject.transform.Find("MovieInfo").GetComponent<TMPro.TextMeshPro>();
        textMeshPro.text = _content;
    }
}
