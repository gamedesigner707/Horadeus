using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameLimiter : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Application.targetFrameRate = 60;
    }
}
