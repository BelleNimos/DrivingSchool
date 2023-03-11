using System;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(Animator))]
public class Sound : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _mixerGroup;
    [SerializeField] private SoundOn _soundOn;
    [SerializeField] private SoundOff _soundOff;

    private Animator _animator;

    private const string Open = "Open";
    private const string Close = "Close";
    private const string MasterVolumeText = "MasterVolume";
    private const float MinVolume = -80f;
    private const float MaxVolume = 0f;

    public bool IsActive { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        IsActive = true;

        if (PlayerPrefs.HasKey(KeysData.SoundVolumeValue) == true)
            IsActive = Convert.ToBoolean(PlayerPrefs.GetInt(KeysData.SoundVolumeValue));

        if (IsActive == true)
            Enable();
        else
            Disable();
    }

    public void PlayOpen()
    {
        _animator.SetTrigger(Open);
    }

    public void PlayClose()
    {
        _animator.SetTrigger(Close);
    }

    public void Enable()
    {
        _mixerGroup.audioMixer.SetFloat(MasterVolumeText, MaxVolume);
        _soundOn.gameObject.SetActive(true);
        _soundOff.gameObject.SetActive(false);
        IsActive = true;
    }

    public void Disable()
    {
        _mixerGroup.audioMixer.SetFloat(MasterVolumeText, MinVolume);
        _soundOff.gameObject.SetActive(true);
        _soundOn.gameObject.SetActive(false);
        IsActive = false;
    }
}
