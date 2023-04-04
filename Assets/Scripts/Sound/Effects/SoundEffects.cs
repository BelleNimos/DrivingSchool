using System;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    [SerializeField] private ActiveEffectsButton _soundOn;
    [SerializeField] private InactiveEffectsButton _soundOff;

    public bool IsPlay { get; private set; }

    public void SetDefaultValues()
    {
        IsPlay = true;

        if (IsPlay == true)
            Enable();
        else
            Disable();
    }

    public void SetStartValues(int value)
    {
        IsPlay = Convert.ToBoolean(value);

        if (IsPlay == true)
            Enable();
        else
            Disable();
    }

    public void Enable()
    {
        _soundOn.gameObject.SetActive(true);
        _soundOff.gameObject.SetActive(false);
        IsPlay = true;
    }

    public void Disable()
    {
        _soundOff.gameObject.SetActive(true);
        _soundOn.gameObject.SetActive(false);
        IsPlay = false;
    }
}
