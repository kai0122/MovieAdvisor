using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceController : MonoBehaviour
{
    public GameObject parentObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Target") && !hasFoundTarget)
        {
            ChangeTargetParent();
        }
    }

    public bool hasFoundTarget = false;
    private void ChangeTargetParent()
    {
        hasFoundTarget = true;
        GameObject target = GameObject.Find("Target");
        target.transform.parent = null;
        target.transform.localRotation = Quaternion.identity;
    }
}
