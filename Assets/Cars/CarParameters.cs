using System;
using UnityEngine;

public class CarParameters : ParametersBase
{
    [Serializable]
    public class WheelParameters
    {
        [SerializeField] public Vector2 position;

        [SerializeField] public float size;
    }

    [SerializeField] public Vector2 boxSize;
    [SerializeField] public WheelParameters[] wheelParameters = new WheelParameters[2];

    [SerializeField] public Transform box;
    [SerializeField] public WheelJoint2D[] wheelJoints;
    [SerializeField] public Transform[] wheels;
    [SerializeField] public float suspensionFrequency = 4;

    private void UpdateWithNewParameters()
    {
        box.transform.localScale = boxSize;
        for (var index = 0; index < wheels.Length; index++)
        {
            WheelJoint2D wheel = wheelJoints[index];
            Transform wheelTransform = wheels[index];
            WheelParameters wheelParameter = wheelParameters[index];
            wheel.anchor = wheelParameter.position;
            JointSuspension2D suspension = wheel.suspension;
            suspension.frequency = suspensionFrequency;
            wheel.suspension = suspension;
            wheelTransform.localPosition = wheelParameter.position;
            wheelTransform.localScale = Vector3.one * wheelParameter.size;
        }
    }

    protected override void Update()
    {
    }

    public override void SetParameters(float[] newParameters)
    {
        boxSize = new Vector2(newParameters[0],newParameters[1]);

        suspensionFrequency = newParameters[2];
        for (int i = 0; i < wheelParameters.Length; i++)
        {
            int index = i * 3;
            Vector2 wheelPosition = new Vector2(newParameters[index],newParameters[index+1]);
            wheelParameters[i].position = wheelPosition;
            wheelParameters[i].size = newParameters[index + 2];
        }

        UpdateWithNewParameters();
    }
}