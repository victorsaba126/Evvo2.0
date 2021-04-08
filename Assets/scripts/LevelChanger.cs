using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;

    public void OnFadeComplete()
    {
        animator.SetTrigger("FadeOut");
    }

  
 
}
