using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR;
using Vuforia;

public class TargetLister : MonoBehaviour
{
	public string trackedTargetName = "";

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		StateManager sm = TrackerManager.Instance.GetStateManager();
		IEnumerable<TrackableBehaviour> tbs = sm.GetActiveTrackableBehaviours();

		foreach (TrackableBehaviour tb in tbs)
		{
			trackedTargetName = tb.TrackableName;
		}
	}
}
