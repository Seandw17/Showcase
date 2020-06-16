using UnityEngine;
using UnityEngine.XR;
using System;

// Author: Alec

public class VRManage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (XRDevice.isPresent)
        {
            throw new NotImplementedException(
                "VR functionality has not been implemented");
        }
    }
}
