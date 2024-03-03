using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Prop : MonoBehaviour
{
    public bool canReversed = true;
    public bool isReversed = false; //TODO: encapsulate
    public bool canHeld = true;
    public bool isHeld = false;
    
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {

        if (isReversed)
        {
            rb.useGravity = false;
            rb.AddForce(Vector3.up * Physics.gravity.magnitude, ForceMode.Acceleration);
        }
        else
        {
            rb.useGravity = true;
        }
    }
    public void ReverseGravity()
    {
        isReversed = !isReversed;
    }
    public void OnHeld()
    {
        isHeld = true;
    }
    public void OnRelease()
    {
        isHeld = false;
    }
}
