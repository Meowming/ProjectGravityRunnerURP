using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncTrigger : MonoBehaviour, ITriggerable
{
    public GameObject targetGO;
    ITriggerable target;
    public bool isTriggerOnce = false;
    public bool hasTriggered = false;
    public LayerMask allowedLayers;
    void ITriggerable.OnTriggered()
    {
        target = targetGO.GetComponent<ITriggerable>();
        if (target != null)
        {
            target.OnTriggered();
        }
        else
        {
            Debug.LogWarning("(Func Trigger) " + name + ": target GO has no trigger interface!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((allowedLayers & (1 << other.gameObject.layer)) != 0)// must be allowed layer
        {
            return;
        }
        if (isTriggerOnce && hasTriggered)
        {
            return;
        }
        else
        {
            ((ITriggerable)this).OnTriggered();
            hasTriggered = true;
        }
    }
}
