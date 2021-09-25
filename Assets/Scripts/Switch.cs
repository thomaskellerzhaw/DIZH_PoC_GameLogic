using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Switch : MonoBehaviour, IUpdateState
{

    [SerializeField] BoolEvent OnStateChanged;
    [SerializeField] bool state;
    [SerializeField] bool voltageIn;

    private void OnTriggerEnter(Collider other)
    {
        //accepting all type of objects
        Debug.Log("Switch/OnTriggerEnter: OnStateChanged called");
        state = !state;
        OnStateChanged.Invoke(state);
    }

    public void UpdateState(bool st)
    {
        OnStateChanged.Invoke(state & st);
        voltageIn = st;
    }

    public bool getState()
    {
        return state;
    }

    public bool getVoltageOut()
    {
        return voltageIn & state;
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
