using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundEffects : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _mixerGroup;
    [SerializeField] private ActiveEffectsButton _soundOn;
    [SerializeField] private InactiveEffectsButton _soundOff;

    private const string EffectsVolumeText = "EffectsVolume";
    private const float MinVolume = -80f;
    private const float MaxVolume = 0f;

    public bool IsActive { get; private set; }

    private void Awake()
    {
        IsActive = true;

        if (PlayerPrefs.HasKey(KeysData.EffectsVolumeValue) == true)
            IsActive = Convert.ToBoolean(PlayerPrefs.GetInt(KeysData.EffectsVolumeValue));

        if (IsActive == true)
            Enable();
        else
            Disable();
    }

    public void Enable()
    {
        _mixerGroup.audioMixer.SetFloat(EffectsVolumeText, MaxVolume);
        _soundOn.gameObject.SetActive(true);
        _soundOff.gameObject.SetActive(false);
        IsActive = true;
    }

    public void Disable()
    {
        _mixerGroup.audioMixer.SetFloat(EffectsVolumeText, MinVolume);
        _soundOff.gameObject.SetActive(true);
        _soundOn.gameObject.SetActive(false);
        IsActive = false;
    }
}
