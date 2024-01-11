using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TriggerDelayPair 
{
    public GameObject targetGO;
    public float delay;
};

public class TriggerMultiple : MonoBehaviour,ITriggerable
{
    public bool isPlayOnStart = false;
    public bool isLooped = false;
    public float loopTime;
    public List<TriggerDelayPair> triggerDelayPairs;

    public void OnTriggered()
    {
        ITriggerable target;
        //for(int i=0;i<targetGOList.Count; i++)
        foreach (TriggerDelayPair pair in triggerDelayPairs)
        {
            if (pair.targetGO == this.gameObject)
            {
                Debug.Log("(Trigger Multiple) " + name + ": self referencing not allowed!");
            }
            else
            {
                target = pair.targetGO.GetComponent<ITriggerable>();
                if (target != null)
                {
                    StartCoroutine(InvokeDelay(target,pair.delay));
                }
                else
                {
                    Debug.LogWarning("(Trigger Multiple) " + name + ": target GO has no trigger interface!");
                }
            }
        }
        if (isLooped)
        {
            StopCoroutine(Looper());
            StartCoroutine(Looper());
        }
    }
    IEnumerator Looper()
    {
        yield return new WaitForSeconds(loopTime);
        OnTriggered();
    }

    IEnumerator InvokeDelay(ITriggerable target, float delay)
    {
        yield return new WaitForSeconds(delay);
        target.OnTriggered();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isPlayOnStart)
        {
            OnTriggered();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
