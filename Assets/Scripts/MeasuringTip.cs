using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(FixedJoint))]
public class MeasuringTip : MonoBehaviour
{
    [SerializeField] GameObject connectedTo;
    FixedJoint fixedJoint;

    private void OnTriggerEnter(Collider other)
    {
        //here comes some logic to avoid connection with wrong objects
        if(other.GetComponent<Rigidbody>() != null)
        {
            connectedTo = other.gameObject;
            fixedJoint.connectedBody = connectedTo.GetComponent<Rigidbody>();
            Debug.Log("MeasuringTip.OnTriggerEnter: FixedJoint is now connected");
            Debug.Log("Connected Object is of type " + connectedTo.GetType());
        }
        else
        {
            Debug.Log("MeasuringTip.OnTriggerEnter: not connection - missing rigidbody");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        fixedJoint.connectedBody = null;
        connectedTo = null;
    }

    public GameObject getConnectedObject()
    {
        return connectedTo;
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().useGravity = false;
        fixedJoint = GetComponent<FixedJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
