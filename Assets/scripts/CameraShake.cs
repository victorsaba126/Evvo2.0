using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Security.Cryptography;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }
    public CinemachineFreeLook freeLook;
    private float shakeTime;
    private void Awake()
    {
        Instance = this;
        freeLook = GetComponent<CinemachineFreeLook>();


    }
    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cbmbp = freeLook.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        CinemachineBasicMultiChannelPerlin cbmbp1 = freeLook.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        CinemachineBasicMultiChannelPerlin cbmbp2 = freeLook.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cbmbp.m_AmplitudeGain = intensity;
        cbmbp1.m_AmplitudeGain = intensity;
        cbmbp2.m_AmplitudeGain = intensity;
        shakeTime = time;

    }
    

    // Update is called once per frame
    void Update()
    {
        if(shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
            if (shakeTime <= 0f)
            {
                CinemachineBasicMultiChannelPerlin cbmbp = freeLook.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                CinemachineBasicMultiChannelPerlin cbmbp1 = freeLook.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                CinemachineBasicMultiChannelPerlin cbmbp2 = freeLook.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cbmbp.m_AmplitudeGain = 0f;
                cbmbp1.m_AmplitudeGain = 0f;
                cbmbp2.m_AmplitudeGain = 0f;
            }
        }
    }


}
