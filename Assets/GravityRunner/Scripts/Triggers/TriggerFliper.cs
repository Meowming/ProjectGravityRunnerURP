using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFliper : MonoBehaviour, ITriggerable
{
    public bool isFlipped = false;
    public List<GameObject> propGoList = new List<GameObject>();
    
    public void OnTriggered()
    {
        isFlipped = !isFlipped;
        foreach (GameObject propGO in propGoList)
        {
            Prop prop = propGO.GetComponent<Prop>();
            if (prop != null)
            {
                prop.isReversed = !prop.isReversed;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
