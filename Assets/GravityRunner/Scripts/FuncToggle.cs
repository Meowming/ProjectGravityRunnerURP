using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncToggle : MonoBehaviour, ITriggerable
{
    public bool isToggledOn = true;
    public bool isAutoDisable = false;
    public float autoDisableTime = 5f;

    void ITriggerable.OnTriggered()
    {
        if (!isToggledOn && isAutoDisable)
        {
            AutoDisable();
        }
        else
        {
            isToggledOn = !isToggledOn;
            gameObject.SetActive(isToggledOn);
        }
    }
    public void AutoDisable()
    {
        //Toggle on, has to be before starting coroutines
        isToggledOn = true;
        gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(AutoDisableCoroutine());
    }

    IEnumerator AutoDisableCoroutine()
    {
        yield return new WaitForSeconds(autoDisableTime);

        //Toggle off
        isToggledOn = false;
        gameObject.SetActive(false);

    }
}
