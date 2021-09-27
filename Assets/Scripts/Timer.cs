using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] int timeoutInSeconds;
    [SerializeField] UnityEvent onTimeout;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = timeoutInSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            onTimeout.Invoke();
            timer = timeoutInSeconds;
        }
    }

    public void addListener(UnityAction call)
    {
        onTimeout.AddListener(call);
    }

    public void clearListeners()
    {
        onTimeout.RemoveAllListeners();
    }
}
