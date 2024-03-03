using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour, ITriggerable
{
    public GameObject spawningObject;

    public void OnTriggered()
    {
        Instantiate(spawningObject, transform.position, transform.rotation);
    }
}
