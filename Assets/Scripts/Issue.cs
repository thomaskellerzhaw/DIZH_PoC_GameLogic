using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Issue
{
    string issueText { get; set; }
    Constants.MeasureMode measureMode;
    Constants.MeasureState measureState;
    float time;

    public Issue(string iT, Constants.MeasureMode mM, Constants.MeasureState mS, float t)
    {
        issueText = iT;
        measureMode = mM;
        measureState = mS;
        time = t;
    }
}
