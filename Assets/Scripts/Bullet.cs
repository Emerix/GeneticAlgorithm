using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    private Rigidbody rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        DidHitSomething = false;
    }

    public void Push(float power)
    {
        rigidbody.AddRelativeForce(new Vector3(0,0,power),ForceMode.Impulse);
    }

    public bool DidHitSomething{get;set;}

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            return;
        }
        DidHitSomething = true;
        rigidbody.velocity = Vector3.zero;
    }
}