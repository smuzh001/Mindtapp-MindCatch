using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wait
{
    private float delay;
    public float Target { get; set; }

    public Wait(float delay)
    {
        this.delay = delay;
        Target = delay;
    }

    public void ResetDelay()
    {
        delay = Target;
    }
    
    public bool UpdateDelay()
    {
        delay -= Time.deltaTime;
        if (delay <= 0f)
        {
            delay = 0f;
            return true;
        }
        return false;
    }
}
