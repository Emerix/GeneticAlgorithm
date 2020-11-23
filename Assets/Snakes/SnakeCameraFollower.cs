using UnityEngine;

public class SnakeCameraFollower : MonoBehaviour
{
    [Range(0, 1)] [SerializeField] private float howMuchToMoveInOneFixedUpdate;
    public static SnakeCameraFollower instance;
    private Transform target;

    private void Awake()
    {
        instance = this;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            return;
        }

        transform.position = new Vector3(
            Mathf.SmoothStep(transform.position.x, target.position.x,howMuchToMoveInOneFixedUpdate),
            Mathf.SmoothStep(transform.position.y, target.position.y, howMuchToMoveInOneFixedUpdate),
            transform.position.z);
    }
}