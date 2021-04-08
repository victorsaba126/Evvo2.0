using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Security.Cryptography;

public class CameraZoom : MonoBehaviour
{
    public CinemachineFreeLook freeLook;
    private CinemachineFreeLook.Orbit[] originalOrbits;

    //https://forum.unity.com/threads/cinemachine-how-to-add-zoom-control-to-freelook-camera.505541/
    private void Awake()
    {
        freeLook = GetComponent<CinemachineFreeLook>();
        originalOrbits = new CinemachineFreeLook.Orbit[freeLook.m_Orbits.Length];

    }

    // Update is called once per frame
    public void orbitas()
    {
        originalOrbits[0].m_Radius = freeLook.m_Orbits[0].m_Radius;
        originalOrbits[1].m_Radius = freeLook.m_Orbits[1].m_Radius;
        originalOrbits[2].m_Radius = freeLook.m_Orbits[2].m_Radius;
    }
}
