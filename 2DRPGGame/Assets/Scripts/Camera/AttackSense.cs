using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;


[RequireComponent(typeof(CinemachineVirtualCamera))]
[AddComponentMenu("Corgi Engine/Camera/Camera Controller")]
public class AttackSense : MMSingleton<AttackSense>
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private CinemachineBasicMultiChannelPerlin noiseProfile;
    private CinemachineFramingTransposer framingTransposer;
    private Player player;
    private bool isShake;

    protected override void Awake()
    {
        base.Awake();

        isShake = false;
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        noiseProfile = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    private void Start()
    {
        player= FindObjectOfType<Player>();
    }

    private void Update()
    {
        framingTransposer.m_ScreenX = 0.5f - 0.25f * player.Core.GetCoreComponent<Movement>().FacingDirection;
    }

    public void HitPause(int duration)
    {
        StartCoroutine(Pause(duration));
    }

    public void CameraShake(float duration, float strength)
    {
        if (!isShake)
            StartCoroutine(Shake(duration, strength));
    }

    IEnumerator Pause(int duration)
    {
        float pauseTime = duration / 60f;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(pauseTime);
        Time.timeScale = 1;
    }

    IEnumerator Shake(float duration, float strength)
    {
        isShake = true;

        if (noiseProfile != null)
        {
            while (duration > 0)
            {
                noiseProfile.m_AmplitudeGain = strength;
                noiseProfile.m_FrequencyGain = strength;
                duration -= Time.deltaTime;
                yield return null;
            }
        }

        if (noiseProfile != null)
        {
            noiseProfile.m_AmplitudeGain = 0f;
            noiseProfile.m_FrequencyGain = 0f;
        }

        isShake = false;
    }
}

