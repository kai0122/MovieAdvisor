using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshCollider))]

public class GizmosController2 : MonoBehaviour
{
    public JustificationController6 justificationController6;
    private Vector3 screenPoint;
    private Vector3 offset;

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

        Vector3 newPosition = gameObject.transform.parent.InverseTransformPoint(curPosition);

        Debug.Log(transform.position.x);

        newPosition.x = newPosition.x < -0.145f ? -0.14f : newPosition.x;
        newPosition.x = newPosition.x > 0.35f ? 0.345f : newPosition.x;
        transform.localPosition = new Vector3(newPosition.x, transform.localPosition.y, transform.localPosition.z);


        if (gameObject.name.Contains("Actor"))
        {
            justificationController6.UpdateRecommendationBase_DirectControlActor((transform.localPosition.x - -0.14f) / 0.485f);
        }
        if (gameObject.name.Contains("Genre"))
        {
            justificationController6.UpdateRecommendationBase_DirectControlGenre((transform.localPosition.x - -0.14f) / 0.485f);
        }
        if (gameObject.name.Contains("Publicity"))
        {
            justificationController6.UpdateRecommendationBase_DirectControlPublicity((transform.localPosition.x - -0.14f) / 0.485f);
        }
        if (gameObject.name.Contains("Rating"))
        {
            justificationController6.UpdateRecommendationBase_DirectControlRating((transform.localPosition.x - -0.14f) / 0.485f);
        }
    }

}