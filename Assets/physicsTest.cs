using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class physicsTest: MonoBehaviour
{
    private Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        rigidbody.AddRelativeForce(1, 0, 0, ForceMode.Acceleration);
    }
}