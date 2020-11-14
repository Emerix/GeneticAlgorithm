using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeParameters : MonoBehaviour
{
    [System.Serializable]
    public class HingeJointParameters
    {
        [SerializeField]
        public HingeJoint2D hingeJoint;

        [SerializeField]
        public float sineSpeed = 1.0f;

        [SerializeField]
        public float sinceSize = 1.0f;

        [SerializeField]
        public float offset = 0.0f;
    }

    [SerializeField]
    public HingeJointParameters[] hingeJointParameters;

    private List<Vector3> positions = new List<Vector3>();
    private List<Quaternion> rotations = new List<Quaternion>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            positions.Add(child.position);
            rotations.Add(child.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var joint in hingeJointParameters)
        {
            JointMotor2D motor2D = joint.hingeJoint.motor;

            motor2D.motorSpeed = Mathf.Sin((Time.timeSinceLevelLoad+ joint.offset) * joint.sineSpeed) * joint.sinceSize;
            joint.hingeJoint.motor = motor2D;
        }
    }

    public void SetParameters( float[] newParameters)
    {
        for (int i = 0; i < hingeJointParameters.Length; i++)
        {
            int index = i * 3;
            hingeJointParameters[i].sinceSize = newParameters[index];
            hingeJointParameters[i].offset = newParameters[index+1];
            hingeJointParameters[i].sineSpeed = newParameters[index+2];
        }
    }

    public void Reset()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.position = positions[i];
            child.rotation = rotations[i];
        }
    }
}
