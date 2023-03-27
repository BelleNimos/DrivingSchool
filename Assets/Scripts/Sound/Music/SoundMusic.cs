using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMusic : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _mixerGroup;
    [SerializeField] private ActiveMusicButton _soundOn;
    [SerializeField] private InactiveMusicButton _soundOff;

    private const string MusicVolumeText = "MusicVolume";
    private const float MinVolume = -80f;
    private const float MaxVolume = 0f;

    public bool IsActive { get; private set; }

    private void Awake()
    {
        IsActive = true;

        if (PlayerPrefs.HasKey(KeysData.MusicVolumeValue) == true)
            IsActive = Convert.ToBoolean(PlayerPrefs.GetInt(KeysData.MusicVolumeValue));

        if (IsActive == true)
            Enable();
        else
            Disable();
    }

    public void Enable()
    {
        _mixerGroup.audioMixer.SetFloat(MusicVolumeText, MaxVolume);
        _soundOn.gameObject.SetActive(true);
        _soundOff.gameObject.SetActive(false);
        IsActive = true;
    }

    public void Disable()
    {
        _mixerGroup.audioMixer.SetFloat(MusicVolumeText, MinVolume);
        _soundOff.gameObject.SetActive(true);
        _soundOn.gameObject.SetActive(false);
        IsActive = false;
    }
}
