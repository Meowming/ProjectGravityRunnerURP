using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAudio : MonoBehaviour,ITriggerable
{
    public AudioClip audioClip;
    public void OnTriggered()
    {
        AudioSource.PlayClipAtPoint(audioClip, transform.position);
    }
}
