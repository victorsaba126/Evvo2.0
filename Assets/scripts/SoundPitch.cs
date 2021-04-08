using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPitch : MonoBehaviour
{
    
    private AudioSource audioSource;
        

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        audioSource.pitch = (Random.Range(0.7f, 1.3f));
        
    }
    
          
}
