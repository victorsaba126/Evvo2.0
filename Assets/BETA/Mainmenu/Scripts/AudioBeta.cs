using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBeta : MonoBehaviour
{
    public AudioSource Button;
    public AudioClip Hover;
    public AudioClip Click;
    public void HoverSound()
    {
        Button.PlayOneShot(Hover);
    }
    public void ClickSound()
    {
        Button.PlayOneShot(Click);
    }
}
