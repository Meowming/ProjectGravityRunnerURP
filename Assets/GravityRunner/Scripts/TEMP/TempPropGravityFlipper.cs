using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPropGravityFlipper : MonoBehaviour
{
    public List<Prop> props;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            foreach (Prop prop in props)
            {
                prop.ReverseGravity();
            }
        }
    }
}
