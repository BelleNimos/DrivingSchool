using System;
using UnityEngine;

public class SoundMusic : MonoBehaviour
{
    [SerializeField] private ActiveMusicButton _soundOn;
    [SerializeField] private InactiveMusicButton _soundOff;

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
