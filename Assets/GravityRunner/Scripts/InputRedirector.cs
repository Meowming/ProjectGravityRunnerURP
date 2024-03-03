using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public delegate void InputDelegate();

public class InputRedirector : MonoBehaviour,ITriggerable
{
    
    public Action OnRecevingInput;
    public void OnTriggered()
    {
        OnRecevingInput();
    }

    public void BindInput(Action inputMethod)
    {
        OnRecevingInput += inputMethod;
    }
}
