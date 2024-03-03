using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonNormal : ButtonBase
{
    public bool canTouchTrigger = false;
    public override void OnButtonPressed()
    {
        if (CanTrigger())
        {
            transform.position += buttonStroke;
            if (triggerTarget != null)
            {
                triggerTarget.GetComponent<ITriggerable>().OnTriggered();
            }
            hasTriggered = true;
            if (pressedSound)
            {
                AudioSource.PlayClipAtPoint(pressedSound, transform.position);
            }
        }
    }

    public override void OnButtonReleased()
    {
        transform.position -= buttonStroke;
    }

    public override void OnTriggered()
    {
        OnButtonPressed();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canTouchTrigger)
        {
            OnButtonPressed();
        }
    }
    // Start is called before the first frame update
}
