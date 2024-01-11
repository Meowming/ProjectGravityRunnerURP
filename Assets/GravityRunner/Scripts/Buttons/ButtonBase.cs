using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ButtonBase : MonoBehaviour, ITriggerable
{
    public bool isTriggerOnce = false;
    public bool hasTriggered = false;
    public GameObject triggerTarget;
    public Vector3 buttonStroke = new Vector3(0f, 0.1f, 0f);
    public LayerMask allowedLayers;
    public string allowedTag;
    public bool isPressed = false;
    public AudioClip pressedSound;
    public AudioClip releasedSound;
    public float cooldownTime = 1f;

    public abstract void OnButtonPressed();
    public abstract void OnButtonReleased();
    public abstract void OnTriggered();
    public bool CanTrigger()
    {
        if (isTriggerOnce && hasTriggered)
        {
            return false;
        }
        if (isPressed)
        {
            return false;
        }
        return true;
    }
    protected virtual IEnumerator CooldownCoroutine()
    {
        string name = gameObject.name;
        isPressed = true; //TODO: maybe redundant
        yield return new WaitForSeconds(cooldownTime);
        OnButtonReleased();
    }
    public virtual void Cooldown()
    {
        StopAllCoroutines();
        StartCoroutine(CooldownCoroutine());
    }
}
