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


    // Start is called before the first frame update
    void Start()
    {
        mM = GetComponent<MeasurementManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //check for measurement mode. If a measurement consists of several steps that may be implemented with a switch statement
        if(mM.measureMode == Constants.MeasureMode.RCD)
        {
            //check if preconditions are fulfilled for measurement
            if(!fuse1PH.getState())
            {
                if(measuringTip.getConnectedObject().GetComponent<RCD>() != null)
                {
                    //now all the preconditions are fulfilled - make measurement
                    bool result = rCD.trip(tripCurrent);
                    if(result == resultingState)
                    {
                        Debug.Log("congratulations. you just successfully made a RCD test.");
                    }
                    else
                    {
                        Debug.Log("oops... try again");
                    }
                }
                else
                {
                    Debug.Log("Measuring Tip is connected to wrong appliance.");
                }
            }
            else
            {
                Debug.Log("Fuse1PH must be off before RCD Measurement.");
            }
        }
        else
        {
            //Debug.Log("not in Measurement Mode RCD");
        }
    }
}
