using UnityEngine;
using UnityEngine.UI.Extensions;

[RequireComponent(typeof(TrailRenderer), typeof(ParticleSystem))]
public class Wheel : MonoBehaviour
{
    private TrailRenderer _trailRenderer;
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
        _particleSystem = GetComponent<ParticleSystem>();
        _trailRenderer.enabled = true;
        _particleSystem.Stop();
    }

    public void SetFlag(bool flag)
    {
        if (flag == true)
        {
            _trailRenderer.emitting = true;
            _particleSystem.Play();
        }
        else
        {
            _trailRenderer.emitting = false;
            _particleSystem.Stop();
        }
    }
}
