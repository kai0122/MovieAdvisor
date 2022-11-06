using UnityEngine;
using Vuforia;
//Attach to the image tracker
public class ChildObjectsActivator : MonoBehaviour, ITrackableEventHandler
{
    public string currentTrackedName = "";
    private TrackableBehaviour trackableBehaviour;
    public string myTargetName;

    void Start()
    {
        trackableBehaviour = GetComponent<TrackableBehaviour>();
        if (trackableBehaviour)
            trackableBehaviour.RegisterTrackableEventHandler(this);
    }

    public void OnTrackableStateChanged(
      TrackableBehaviour.Status previousStatus,
      TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED)
            //newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            OnTrackingFound();
        else
            onTrackingLost();
    }
    private void OnTrackingFound()
    {
        currentTrackedName = trackableBehaviour.TrackableName;
        if (transform.childCount > 0)
            SetChildrenActive(true);
    }
    private void onTrackingLost()
    {
        currentTrackedName = "";
        if (transform.childCount > 0)
            SetChildrenActive(false);
    }
    private void SetChildrenActive(bool activeState)
    {
        for (int i = 0; i <= transform.childCount; i++)
            transform.GetChild(i++).gameObject.SetActive(activeState);
    }
}