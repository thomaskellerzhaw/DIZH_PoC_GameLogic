using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    public enum MeasureMode
    {
        Off,
        Isolation,
        ShortCircuit,
        RCD
    }

    public enum MeasureState
    {
        Success,
        Failure,
        MissingPrecondition
    }
}
