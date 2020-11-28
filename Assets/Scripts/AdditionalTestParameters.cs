using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalTestParameters : MonoBehaviour
{
    
    [SerializeField]
    public float timeOutWithoutMovement = 2.0f;
    
    [SerializeField]
    public float minMovementInTimeframeToPreventTimeout = 0.4f;

    public static float TimeOutWithoutMovement = 1.0f;
    public static float MinMovementInTimeframeToPreventTimeout = 0.05f;

    void OnValidate()
    {
        UpdateVariables();
    }
    
    void Start()
    {
        UpdateVariables();
    }

    private void UpdateVariables()
    {
        TimeOutWithoutMovement = timeOutWithoutMovement;
        MinMovementInTimeframeToPreventTimeout = minMovementInTimeframeToPreventTimeout;
    }
}
