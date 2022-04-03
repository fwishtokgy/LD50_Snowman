using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashSnowflakes : MainPlayMonoBehaviour
{
    [SerializeField]
    protected ParticleSystem SplashParticles;

    private void Awake()
    {
        SplashParticles.Play();
    }

    protected override void OnStateStart()
    {
        base.OnStateStart();
        SplashParticles.Stop();
    }

    protected override void OnStateEnd()
    {
        base.OnStateEnd();
        SplashParticles.Play();
    }
}
