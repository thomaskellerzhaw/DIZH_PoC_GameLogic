using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Light))]
public class Lamp : MonoBehaviour, IUpdateState
{
    
    //find out if voltage is on
    public void UpdateState(bool state)
    {
        if (state)
        {
            //turn lamp on
            GetComponent<Light>().range = 10;
        }
        else
        {
            //turn lamp off
            GetComponent<Light>().range = 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
