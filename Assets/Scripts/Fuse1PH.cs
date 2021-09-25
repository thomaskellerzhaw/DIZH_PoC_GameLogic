using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Fuse1PH : MonoBehaviour, IUpdateState
{
    [SerializeField] BoolEvent OnStateChanged;
    [SerializeField] bool state=true;
    [SerializeField] float i_nom = 10f; //Ampere
    [SerializeField] float u_nom = 250f; //Voltage

    private void OnTriggerEnter(Collider other)
    {
        //MeasuringTip cannot trip fuse
        if(other.GetComponent<MeasuringTip>() == null)
        {
            Debug.Log("Collision: OnStateChanged called");
            state = !state;
            OnStateChanged.Invoke(state);
        }
    }

    public bool getState()
    {
        return state;
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
        OnStateChanged.Invoke(state);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateState(bool state)
    {
        throw new System.NotImplementedException();
    }
}
