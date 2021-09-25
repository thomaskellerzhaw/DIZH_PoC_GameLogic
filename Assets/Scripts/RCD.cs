using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class RCD : MonoBehaviour, IUpdateState
{
    [SerializeField] BoolEvent OnStateChanged;
    [SerializeField] bool state;
    [SerializeField] float i_nom = 0.03f; //Ampere
    [SerializeField] float u_nom = 250f; //Voltage
    [SerializeField] bool voltageIn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MeasuringTip>() == null)
        {
            state = !state;
            OnStateChanged.Invoke(state);
        }
    }

    public void UpdateState(bool st)
    {
        Debug.Log("RCD.UpdateState: st= " + st);
        OnStateChanged.Invoke(state & st);
        voltageIn = st;
        Debug.Log("RCD.voltageOut: " + getVoltageOut());
    }

    public bool getState()
    {
        return state;
    }

    public bool getVoltageOut()
    {
        return voltageIn & state;
    }

    public bool trip(float current)
    {
        if (current >= i_nom)
        {
            //RCD trips
            state = false;
            OnStateChanged.Invoke(state);
        }
        return state;
    }

    // Start is called before the first frame update
    void Start()
    {
        state = true;
        UpdateState(state);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
