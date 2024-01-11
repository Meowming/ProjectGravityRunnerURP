using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityZone : MonoBehaviour
{
    Collider zone;
    public Vector3 direction = Vector3.left;
    public float strength = 1f;
    // Start is called before the first frame update
    void Start()
    {
        zone = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.AddForce(direction * strength, ForceMode.Acceleration);
        }
    }
}
