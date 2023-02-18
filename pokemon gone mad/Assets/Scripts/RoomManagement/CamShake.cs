using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamShake : MonoBehaviour
{
    public static CamShake Instance { get; private set; }

    private CinemachineVirtualCamera cineCam;
    private float shakeTimer;

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        cineCam = GetComponent<CinemachineVirtualCamera>();
    }

    public void shakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin multi = cineCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        multi.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

    private void Update()
    {
        if (shakeTimer > 0f)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f )
            {
                CinemachineBasicMultiChannelPerlin multi = cineCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                multi.m_AmplitudeGain = 0f;
            }
        }
    }
}
