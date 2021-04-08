using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


public class TutorialMove : MonoBehaviour
{
    public VideoPlayer VideoPlayer;
    public GameObject tutorial;
    public GameObject trigger;
    private PlayerController player;
    static private bool tutorial1 = true;
    static private bool tutorial2 = true;
    static private bool tutorial3 = true;
    private PlayerAnimation animator;
    private OptionsMenu options;
    static private bool endtuto = false;
    

    private OptionsMenu pause;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        animator = FindObjectOfType<PlayerAnimation>();
        pause = FindObjectOfType<OptionsMenu>();
        VideoPlayer.loopPointReached += skip;
        options = FindObjectOfType<OptionsMenu>();
    }
    private void Update()
    {
        if (endtuto)
        {
            skip2();
            animator.OpenDoor();
        }
        else if (options.goMenu || player.tuto)
        {
            Debug.Log("funsiona");
            tutorial1 = true;
            tutorial2 = true;
            tutorial3 = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if ((player.horizontalMove != 0 && other.tag == "Player" && tutorial1 == true) || (player.verticalMove != 0 && other.tag == "Player" && tutorial1 == true))
        {
            Debug.Log("1 "+tutorial1);
            tutorial1 = false;
           
            skip2();
        }

        if (Input.GetKey("space") && other.tag=="Player" && tutorial2==true && tutorial1 == false)
        {
            Debug.Log("2 "+tutorial2);
            tutorial2 = false;
            
            skip2();
        }
        
        if(Input.GetKeyDown(KeyCode.Mouse0) && other.tag == "Player" && tutorial3==true && tutorial1 == false && tutorial2 == false)
        {
            Debug.Log("3" +tutorial3);
            tutorial3 = false;
            animator.OpenDoor();
            skip2();
            endtuto = true;
        }
    }
    
    void skip(VideoPlayer vp){}
    void skip2()
    {
        tutorial.SetActive(false);
        trigger.SetActive(false);
    }
}
