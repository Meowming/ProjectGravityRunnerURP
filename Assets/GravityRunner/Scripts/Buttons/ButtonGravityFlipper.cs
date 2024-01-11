using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGravityFlipper : ButtonBase
{
    Prop heldingProp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnButtonPressed()
    {
        if (CanTrigger())
        {
            transform.position += buttonStroke; // Move buttin
            isPressed = true;
            hasTriggered = true;
            if (pressedSound)
            {
                AudioSource.PlayClipAtPoint(pressedSound, transform.position);
            }

            //Trigger target
            if (this.triggerTarget != null)
            {
                ITriggerable target = triggerTarget.GetComponent<ITriggerable>();
                Debug.Log(target);
                if (target != null)
                {
                    target.OnTriggered();
                }
            }
            PlayManager.FlipPlayerGravity(); // Flip player
            Cooldown(); // Cooldown
        }
    }

    public override void OnButtonReleased()
    {
        if (!isPressed)
        {
            return;
        }
        //Press actions
        transform.position -= buttonStroke;
        isPressed = false;

        //Release sound
        if (releasedSound)
        {
            AudioSource.PlayClipAtPoint(releasedSound, transform.position);
        }
        //Touching prop contrains
        if (heldingProp != null)
        {
            //remove glowing effect
            heldingProp.gameObject.layer = LayerMask.NameToLayer("Default");
            gameObject.layer = LayerMask.NameToLayer("Default");
            //remove contrains
            heldingProp.rb.constraints = RigidbodyConstraints.None;
            heldingProp.canHeld = true;
            heldingProp = null;// reset touching prop
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!CanTrigger())
        {
            return;
        }
        if (other.tag != allowedTag || isPressed)
        {
            return;
        }
        heldingProp = other.GetComponent<Prop>();
        if (heldingProp.isHeld)//only trigger when item is dropped
        {
            return;
        }
        if(heldingProp != null)
        {
            heldingProp.rb.constraints = RigidbodyConstraints.FreezeAll;
            heldingProp.canHeld = false;
            //add glowing effect
            heldingProp.gameObject.layer = LayerMask.NameToLayer("Outlined");
            gameObject.layer = LayerMask.NameToLayer("Outlined");
        }
        OnButtonPressed();
        PlayManager.ReplaceCurrentGravityFlipper(this);
    }

    public override void OnTriggered()
    {
        //PlayManager.FlipPlayerGravity(); //TODO: Change
    }

    protected override IEnumerator CooldownCoroutine()
    {
        string name = gameObject.name;
        isPressed = true;
        /*
        if (heldingProp != null)
        {
            //remove glowing effect
            heldingProp.gameObject.layer = LayerMask.NameToLayer("Default");
            gameObject.layer = LayerMask.NameToLayer("Default");

            heldingProp.rb.constraints = RigidbodyConstraints.None;
            heldingProp.canHeld = true;
            heldingProp = null;// reset touching prop
        }
        */
        yield return new WaitForSeconds(cooldownTime);
        isPressed = false;
    }
    public override void Cooldown()
    {
        StopAllCoroutines();
        StartCoroutine(CooldownCoroutine());
    }

    public void DisconnectHeldingProp()//TODO: optimize
    {
        if (heldingProp != null)
        {
            //remove glowing effect
            heldingProp.gameObject.layer = LayerMask.NameToLayer("Default");
            gameObject.layer = LayerMask.NameToLayer("Default");
            //remove contrains
            heldingProp.rb.constraints = RigidbodyConstraints.None;
            heldingProp.canHeld = true;
            heldingProp = null;// reset touching prop
        }
    }
}
