using System.Collections.Generic;
using UnityEngine;

public abstract class ParametersBase : MonoBehaviour
{
    private List<Vector3> positions = new List<Vector3>();
    private List<Quaternion> rotations = new List<Quaternion>();
    protected abstract void Update();
    public abstract void SetParameters(float[] newParameters);

    protected virtual void Start()
    {
        foreach (Transform child in transform)
        {
            positions.Add(child.position);
            rotations.Add(child.rotation);
        }
    }

    public virtual void Reset()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.position = positions[i];
            child.rotation = rotations[i];
        }
    }
}