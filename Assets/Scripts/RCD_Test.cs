using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeasurementManager))]
public class RCD_Test : MonoBehaviour
{
    [SerializeField] Fuse1PH fuse1PH;
    [SerializeField] RCD rCD;
    [SerializeField] MeasuringTip measuringTip;
    [SerializeField] bool resultingState;
    [SerializeField] float tripCurrent;
    private MeasurementManager mM;
    private List<Issue> issues;
    private HelpSystem helpSystem;


    // Start is called before the first frame update
    void Start()
    {
        mM = GetComponent<MeasurementManager>();
        issues = new List<Issue>();
        helpSystem = mM.GetComponent<HelpSystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Measure()
    {
        //check if preconditions are fulfilled for measurement
        if (!fuse1PH.getState())
        {
            if (measuringTip.getConnectedObject().GetComponent<RCD>() != null)
            {
                //now all the preconditions are fulfilled - make measurement
                bool result = rCD.trip(tripCurrent);
                if (result == resultingState)
                {
                    Debug.Log("congratulations. you just successfully made a RCD test.");
                    issues.Add(new Issue("congratulations. you just successfully made a RCD test.",Constants.MeasureMode.RCD, Constants.MeasureState.Success, Time.time));
                }
                else
                {
                    Debug.Log("oops... try again");
                    issues.Add(new Issue("oops... try again.", Constants.MeasureMode.RCD, Constants.MeasureState.Failure, Time.time));
                }
            }
            else
            {
                Debug.Log("Measuring Tip is connected to wrong appliance.");
                issues.Add(new Issue("Measuring Tip is connected to wrong appliance.", Constants.MeasureMode.RCD, Constants.MeasureState.MissingPrecondition, Time.time));
            }
        }
        else
        {
            Debug.Log("Fuse1PH must be off before RCD Measurement.");
            issues.Add(new Issue("Fuse1PH must be off before RCD Measurement.", Constants.MeasureMode.RCD, Constants.MeasureState.MissingPrecondition, Time.time));
        }

        if(helpSystem != null) helpSystem.ReportIssue(issues);
    }

}
