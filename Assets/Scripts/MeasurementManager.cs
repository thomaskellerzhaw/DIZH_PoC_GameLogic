using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Timer))]
[RequireComponent(typeof(RCD_Test))]
public class MeasurementManager : MonoBehaviour
{
    public Constants.MeasureMode measureMode;
    private Constants.MeasureMode measureModePrevious;
    private Timer timer;
    private RCD_Test rCD_Test;

    private void Start()
    {
        measureMode = Constants.MeasureMode.Off;
        measureModePrevious = measureMode;
        timer = GetComponent<Timer>();
        rCD_Test = GetComponent<RCD_Test>();
    }

    private void Update()
    {
        //to be replaced by rotary dial event
        if (MeasureModeChanged())
        {
            timer.clearListeners();
            switch (measureMode)
            {
                case Constants.MeasureMode.RCD:
                    timer.addListener(rCD_Test.Measure);
                    break;

                default:
                    //nothing to do
                    break;
            }
            
        }
    }

    private bool MeasureModeChanged()
    {
        if (measureMode != measureModePrevious)
        {
            measureModePrevious = measureMode;
            return true;
        }
        else
        {
            return false;
        }
    }
}
