using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;


public class TimeOfDay : MonoBehaviour
{
    [SerializeField] Volume day;
    [SerializeField] Volume night;

    private float counterTime;
    public float transitionTime;
    public bool nightOn = false;
    private float numero = 0;

   

    // Update is called once per frame
    void FixedUpdate()
    {
        counterTime += Time.deltaTime;
        numero += 0.02f;
        Debug.Log("numero "+numero);
        Debug.Log("countertime "+counterTime);
        
        
        
       if(counterTime>= transitionTime)
        {
            counterTime = 0;
        }
        else
        {
            if (night.weight >= 1)
            {
                nightOn = true;
            }
            else if (night.weight <= 0)
            {
                nightOn = false;
            }
            if (nightOn)
            {
                night.weight = numero / transitionTime; /*(counterTime * -0.00001f);*/
            }
            else
            {
                night.weight = numero/ transitionTime;/*(counterTime * 0.00001f);*/
            }

            //day.weight = day.weight+(counterTime * - 0.01f);
            //night.weight = night.weight+(counterTime * 0.01f);
        }
       
    }
}
